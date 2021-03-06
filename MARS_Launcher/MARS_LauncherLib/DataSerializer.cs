﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using Newtonsoft.Json.Linq;
using Convert = System.Convert;
using MemoryStream = System.IO.MemoryStream;
using Image = System.Drawing.Image;

namespace MARS_LauncherLib
{
    public interface DataSerializer
    {
        void StartSerialize();
        string EndSerialize();

        void SerializeDataTable(DataTable dataTable, string name = null);
        void SerializeDataTables(List<DataTable> dataTables, List<string> names = null);
    }

    public interface StructuredDataSerializer : DataSerializer
    {
        void SerializeScalar(string name, object value);
        void SerializeDictionary(string name, Dictionary<string, object> dict);
    }

    public interface DataDeserializer
    {
        void StartDeserialize(string str);
        void EndDeserialize();

        DataTable DeserializeDataTable(string name, List<System.Type> columnTypes = null);
        List<DataTable> DeserializeDataTables(List<string> names = null, List<System.Type> columnTypes = null);
    }

    public interface StructuredDataDeserializer : DataDeserializer
    {
        object DeserializeScalar(string name);

        Image DeserializeImage(string name);
        List<Image> DeserializeImages(List<string> names = null);
    }

    public class CSVSerializer : DataDeserializer
    {
        private string _csv;

        public void StartDeserialize(string csv)
        {
            _csv = csv;
        }

        public void EndDeserialize()
        {
            _csv = null;
        }

        public DataTable DeserializeDataTable(string name, List<System.Type> colTypes = null)
        {
            var theTable = new DataTable(name);

            /*----------------------------------
             * A first pass to read the header
             * and infer the data types
             * ---------------------------------*/
            var columnNames = new List<string>();
            var columnTypes = new List<Type>();
            int columnCount = 0;
            int counter = 0;
            using (StringReader reader = new StringReader(_csv)) //Read line by line
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                        continue;

                    if (counter == 0)
                    {
                        var tokens = line.Split(',');
                        columnCount = tokens.Length - 1;
                        for (int i = 1; i <= columnCount; ++i)
                            columnNames.Add(tokens[i]);
                    }
                    else if (colTypes != null && counter == 1)
                    {
                        var tokens = line.Split(',');
                        columnCount = tokens.Length - 1;
                        for (int i = 1; i <= columnCount; ++i)
                            columnTypes.Add(this.GuessType(tokens[i]));
                    }
                    else
                        break;

                    counter++;
                }
            }

            if (colTypes != null)
                columnTypes = colTypes;
            for (int i = 0; i < columnCount; ++i)
                theTable.Columns.Add(columnNames[i], columnTypes[i]);

            /*----------------------------------
             * A second pass to read the data
             * ---------------------------------*/
            counter = 0;
            using (StringReader reader = new StringReader(_csv))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line == "")
                        continue;

                    if (counter == 0)
                    {
                        counter++;
                        continue;
                    }

                    DataRow row = theTable.NewRow();

                    var tokens = line.Split(',');
                    for (int i = 0; i < columnCount; ++i)
                    {
                        if (columnTypes[i] == typeof(DateTime))
                            row[i] = Convert.ToDateTime(tokens[i + 1]); // +1 because first column is not used.
                        else if (columnTypes[i] == typeof(double))
                            row[i] = Convert.ToDouble(tokens[i + 1]);
                        else if (columnTypes[i] == typeof(int))
                            row[i] = Convert.ToInt32(tokens[i + 1]);
                        else
                            row[i] = tokens[i + 1];
                    }

                    theTable.Rows.Add(row);
                    counter++;
                }
            }
            return theTable;
        }
        public List<DataTable> DeserializeDataTables(List<string> names = null, List<System.Type> columnTypes = null)
        {
            var result = new List<DataTable>();
            string name = names != null ? names[0] : "Table 1";
            result.Add(DeserializeDataTable(name, columnTypes));
            return result;
        }

        private System.Type GuessType(string strValue)
        {
            DateTime dateValue;
            double doubleValue;
            int intValue;

            if (DateTime.TryParse(strValue, out dateValue))
                return typeof(DateTime);

            if (int.TryParse(strValue, out intValue))
                return typeof(int);

            if (double.TryParse(strValue, out doubleValue))
                return typeof(double);

            return typeof(string);

        }
    }

    public class JSONDataSerializer : StructuredDataSerializer, StructuredDataDeserializer
    {
        private JObject _json;

        public JSONDataSerializer()
        {
        }

        public void StartSerialize()
        {
            _json = new JObject();
        }

        public string EndSerialize()
        {
            string json = _json.ToString();
            string str =_json.ToString();
            _json = null;
            return str;
        }

        public void StartDeserialize(string str)
        {
            _json = JObject.Parse(str);
        }

        public void EndDeserialize()
        {
            _json = null;
        }

        public void SerializeScalar(string name, object value)
        {
            if (!_json.ContainsKey("Scalars"))
                _json.Add("Scalars", new JObject());
            _json["Scalars"][name] = JToken.FromObject(value);
        }

        public object DeserializeScalar(string name)
        {
            return _json["Scalars"][name];
        }

        public void SerializeDictionary(string name, Dictionary<string, object> dict)
        {
            _json["Dictionaries"] = new JObject();
            JObject jsDict = new JObject();
            foreach (var key in dict.Keys)
            {
                JToken token = JToken.FromObject(dict[key]);
                jsDict[key] = token;
            }
            if (!_json.ContainsKey("Dictionaries"))
                _json.Add("Dictionaries", new JObject());
            _json["Dictionaries"][name] = jsDict;
        }

        public void SerializeDataTable(DataTable dataTable, string name = null)
        {
            JArray columns = new JArray(), index = new JArray(), data = new JArray();

            foreach (DataColumn column in dataTable.Columns)
                columns.Add(column.ColumnName);

            for (int i = 0; i < dataTable.Rows.Count; ++i)
            {
                DataRow row = dataTable.Rows[i];
                index.Add(i);
                JArray jsonRow = new JArray();
                for (int j = 0; j < columns.Count; ++j)
                    jsonRow.Add(row[j]);
                data.Add(jsonRow);
            }

            JObject df = new JObject();
            df["columns"] = columns;
            df["index"] = index;
            df["data"] = data;

            string tableName = name != null ? name : dataTable.TableName;
            if(!_json.ContainsKey("DataFrames"))
                _json.Add("DataFrames", new JObject());
            _json["DataFrames"][tableName] = df;
        }

        public DataTable DeserializeDataTable(string name, List<System.Type> columnTypes = null)
        {
            JToken df = _json["DataFrames"][name];
            JArray columns = df["columns"] as JArray;
            JArray index = df["index"] as JArray;
            JArray data = df["data"] as JArray;

            DataTable myTable = new DataTable();

            List<System.Type> colTypes = null;
            if (columnTypes != null)
                colTypes = columnTypes;

            myTable.Columns.AddRange(columns.Select(col => new DataColumn(col.ToString())).ToArray());
            foreach (var row in data)
            {
                int i = 0;
                var myRow = myTable.NewRow();
                foreach (var item in row)
                {
                    var dataValue = item.ToObject((colTypes[i]));
                    myRow[myTable.Columns[i]] = dataValue;
                    ++i;
                }
                myTable.Rows.Add(myRow);
            }
            myTable.TableName = name;
            return myTable;
        }

        public void SerializeDataTables(List<DataTable> dataTables, List<string> names = null)
        {
            if(names != null && dataTables.Count != names.Count)
                throw new ApplicationException("List of Data Table names does not have the same number of elements as the list of Data Tables");

            for (int i=0; i < dataTables.Count; ++i)
            {
                string name = names != null ? names[i] : dataTables[i].TableName;
                SerializeDataTable(dataTables[i], name);
            }
        }

        public List<DataTable> DeserializeDataTables(List<string> names = null, List<System.Type> columnTypes = null)
        {
            var result = new List<DataTable>();
            JToken dataFrames = _json["DataFrames"];
            IList<string> tableNames = names != null ? names :
                dataFrames.Children<JProperty>().Select(p => p.Name).ToList();

            foreach (var name in tableNames)
            {
                result.Add(DeserializeDataTable(name, columnTypes));
            }
            return result;
        }

        public Image DeserializeImage(string name)
        {
            JToken imageToken = _json["Images"][name];

            var base64String = imageToken.ToString();
            byte[] data = Convert.FromBase64String(base64String);
            using (var stream = new MemoryStream(data, 0, data.Length))
            {
                Image image = Image.FromStream(stream);
                image.Tag = name;
                return image;
            }
        }

        public List<Image> DeserializeImages(List<string> names = null)
        {
            var result = new List<Image>();
            JToken dataFrames = _json["Images"];
            IList<string> imageNames = names != null ? names :
                dataFrames.Children<JProperty>().Select(p => p.Name).ToList();

            foreach (var name in imageNames)
            {
                result.Add(DeserializeImage(name));
            }
            return result;
        }
    }

    public class DeserializerFactory
    {
        private static DeserializerFactory _factory = new DeserializerFactory();

        public static DeserializerFactory Instance()
        {
            return _factory;
        }

        public DataDeserializer CreateDeserializer(string type)
        {
            switch (type)
            {
                case "CSV":
                    return new CSVSerializer();
                case "JSON":
                    return new JSONDataSerializer();
                default:
                    throw new ApplicationException(String.Format("Unknown Serializer type: {0}", type));
            }
        }
    }
}
