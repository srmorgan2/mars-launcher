using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MARS_LauncherLib;


namespace MARS_LauncherGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataTable ParseCsvToDataTable(string csv)
        {
            var theTable = new DataTable("OutputData");

            /*----------------------------------
             * A first pass to read the header
             * and infer the data types
             * ---------------------------------*/
            var columnNames = new List<string>();
            var columnTypes = new List<Type>();
            int columnCount = 0;
            int counter = 0;
            using (StringReader reader = new StringReader(csv)) //Read line by line
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
                        for(int i = 1; i<= columnCount; ++i)
                            columnNames.Add(tokens[i]);
                    }
                    else if (counter == 1)
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


            for(int i=0; i< columnCount; ++i)
            {
                theTable.Columns.Add(columnNames[i], columnTypes[i]);
            }

            /*----------------------------------
             * A second pass to read the data
             * ---------------------------------*/
            counter = 0;
            using (StringReader reader = new StringReader(csv)) 
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

        private void btnRun_Click(object sender, EventArgs e)
        {
            var launcher = new MarsLauncher(txtRootFolder.Text, txtPythonProgram.Text);
            //string xml;
            //xml = "Dummy";

            string stdInput;
            var serializer = new JSONDataSerializer();
            serializer.StartSerialize();
            serializer.SerializeString(txtPythonScript.Text, "Python Script");
            serializer.SerializeString(txtRootFolder.Text, "Root Folder");
            //DataTable dtFromGrid = new DataTable();
            //dtFromGrid = dataGridView1.DataSource as DataTable;//?? new DataTable();
            //serializer.serialize(dataGridView1, "Curve");
            stdInput = serializer.EndSerialize();

            txtOutput.Text = "Launching " + txtPythonScript.Text;
            lblStatus.Text = "Running " + txtPythonScript.Text;
            DataSet result = launcher.Run(txtPythonScript.Text, stdInput);
            txtOutput.Text = launcher.Output;

            var deserializerFactory = DeserializerFactory.Instance();
            var deserializer = deserializerFactory.CreateDeserializer(launcher.OutputDataType);
            //var data = ParseCsvToDataTable(launcher.OutputData);
            deserializer.StartDeserialize(launcher.OutputData);
            var dataTables = deserializer.DeserializeDataTables(new string[]{ "Table 1", "Table 2", "Table 3"}.ToList(),
                new System.Type[] { typeof(DateTime), typeof(string), typeof(double), typeof(double) }.ToList());
            serializer.EndDeserialize();

            for (int i = 0; i < dataTables.Count; ++i)
            {
                TabPage tab = new TabPage();
                DataGridView dataGridView = new DataGridView();

                dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
                dataGridView.Location = new System.Drawing.Point(3, 3);
                dataGridView.Name = "dgvResult" + (i + 1).ToString();
                dataGridView.Size = new System.Drawing.Size(850, 576);
                dataGridView.TabIndex = 8;
                dataGridView.DataSource = dataTables[i];

                tab.SuspendLayout();
                // Add tabpage to tabcontrol
                tab.Controls.Add(dataGridView);
                //tab.Text = "Tab" + (i+1).ToString();
                tab.Text = dataTables[i].TableName;
                tab.Controls.Add(this.dgvResult);
                tab.Location = new System.Drawing.Point(4, 25);
                tab.Name = "tabPage" + (i + 1).ToString();
                tab.Padding = new System.Windows.Forms.Padding(3);
                tab.Size = new System.Drawing.Size(856, 582);
                tab.TabIndex = i+1;
                tab.Text = "Result: " + dataTables[i].TableName;
                tab.UseVisualStyleBackColor = true;
                tab.ResumeLayout();

                tabResult.Controls.Add(tab);
            }
            dgvResult.DataSource = dataTables[0];
            lblStatus.Text = "Done";
        }
    }
}
