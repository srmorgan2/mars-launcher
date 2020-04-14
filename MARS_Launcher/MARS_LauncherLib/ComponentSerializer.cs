using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MARS_LauncherLib
{
    public interface ComponentSerializer
    {
        void startSerialize();
        string endSerialize();
        void startDeserialize(string str);
        void endDeserialize();

        void serialize(TextBox textBox, string componentName = null);
        void deserialize(string componentName, TextBox textBox);

        void serialize(ComboBox comboBox, string componentName = null);
        void deserialize(string componentName, ComboBox comboBox);

        void serialize(RichTextBox richTextBox, string componentName = null);
        void deserialize(string componentName, RichTextBox richTextBox);

        void serialize(DataGridView dataGridView, string componentName = null);
        void deserialize(string componentName, DataGridView dataGridView);

        void serialize(DataTable dataTable, string name);
        DataTable deserialize(string name, System.Type[] columnTypes = null, DataTable dataTable = null);
    }

    public class JSONComponentSerializer : ComponentSerializer
    {
        private JObject _json;

        public JSONComponentSerializer()
        {
            
        }

        public void startSerialize()
        {
            _json = new JObject();
        }

        public string endSerialize()
        {
            string json = _json.ToString();
            return _json.ToString();
        }

        public void startDeserialize(string str)
        {
            _json = JObject.Parse(str);
        }

        public void endDeserialize()
        {
            _json = null;
        }

        public void serialize(TextBox textBox, string componentName = null)
        {
            string name = componentName ?? textBox.Name;
            //_json.Add(new JProperty(name, textBox.Text));
            _json[name] = textBox.Text;
        }

        public void deserialize(string componentName, TextBox textBox)
        {
            textBox.Text = _json[componentName].ToString();
        }

        public void serialize(ComboBox comboBox, string componentName = null)
        {
            string name = componentName ?? comboBox.Name;
            _json[name] = comboBox.Text;
        }

        public void deserialize(string componentName, ComboBox comboBox)
        {
            comboBox.Text = _json[componentName].ToString();
        }

        public void serialize(RichTextBox richTextBox, string componentName = null)
        {
            string name = componentName ?? richTextBox.Name;
            _json[name] = richTextBox.Text;
        }

        public void deserialize(string componentName, RichTextBox richTextBox)
        {
            richTextBox.Text = _json[componentName].ToString();
        }

        public void serialize(DataGridView dataGridView, string componentName = null)
        {
            string name = componentName ?? dataGridView.Name;

            JArray columns = new JArray(), index = new JArray(), data = new JArray();

            foreach (DataGridViewColumn column in dataGridView.Columns)
                columns.Add(column.Name);

            int rowCount = 0;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                index.Add(rowCount);
                DataGridViewCellCollection cells = row.Cells;
                JArray jsonRow = new JArray();
                foreach (DataGridViewCell cell in cells)
                    jsonRow.Add(cell.Value);
                data.Add(jsonRow);
                ++rowCount;
            }

            JObject df = new JObject();
            df["columns"] = columns;
            df["index"] = index;
            df["data"] = data;

            _json[name] = df;
        }

        public void deserialize(string componentName, DataGridView dataVGridView)
        {
            string name = componentName ?? dataVGridView.Name;

            //JObject df = _json[name];
            //JObject columns = df["columns"];
            //JObject index = df["index"];
            //JObject data = df["data"];

            JArray columns = new JArray(), index = new JArray(), data = new JArray();

            foreach (DataGridViewColumn column in dataVGridView.Columns)
                columns.Add(column.Name);

            int rowCount = 0;
            foreach (DataGridViewRow row in dataVGridView.Rows)
            {
                index.Add(rowCount);
                DataGridViewCellCollection cells = row.Cells;
                JArray jsonRow = new JArray();
                foreach (DataGridViewCell cell in cells)
                    jsonRow.Add(cell.Value);
                data.Add(jsonRow);
                ++rowCount;
            }

            JObject df = new JObject();
            df["columns"] = columns;
            df["index"] = index;
            df["data"] = data;
        }

        public void serialize(DataTable dataTable, string name)
        {
            JArray columns = new JArray(), index = new JArray(), data = new JArray();

            foreach (DataColumn column in dataTable.Columns)
                columns.Add(column.ColumnName);

            for(int i=0; i<dataTable.Rows.Count; ++i)
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

            _json[name] = df;
        }

        public DataTable deserialize(string name, System.Type[] columnTypes = null, DataTable dataTable = null)
        {
            JToken df = _json[name];
            JArray columns = df["columns"] as JArray;
            JArray index = df["index"] as JArray;
            JArray data = df["data"] as JArray;

            DataTable myTable = new DataTable();

            // Get the column types, either passed in or from the original data table
            System.Type[] colTypes = columnTypes != null ? columnTypes :
                dataTable.Columns.Cast<DataColumn>().Select(col => col.DataType).ToArray();

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
            return myTable;
        }
    }
}
