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
             * and infere the data types
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
            string xml;
            xml = "Dummy";
            txtOutput.Text = "Launching " + txtPythonScript.Text;
            lblStatus.Text = "Running " + txtPythonScript.Text;
            launcher.Run(txtPythonScript.Text, xml);
            txtOutput.Text = launcher.Output;
            var data = ParseCsvToDataTable(launcher.OutputData);
            dgvResult.DataSource = data;
            lblStatus.Text = "Done";
        }
    }
}
