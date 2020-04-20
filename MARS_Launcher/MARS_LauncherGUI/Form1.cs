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

        private DataGridView CreateDataGridView(string name, DataTable dataTable)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            //dataGridView.Name = name;
            dataGridView.DataSource = dataTable;
            return dataGridView;
        }

        private PictureBox CreatePictureBox(string name, Image image)
        {
            PictureBox pictureBox = new PictureBox();
            //pictureBox.Name = name;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = image;
            return pictureBox;
        }

        private void DisplayData(DataTable data)
        {
            int j = 0;
            DataGridView dataGridView = CreateDataGridView("dgvResult" + (j + 1).ToString(), data);
            AddTabPage(tabResult, data.TableName, dataGridView);
        }

        private void DisplayData(Image image)
        {
            int j = 0;
            PictureBox pictureBox = CreatePictureBox(String.Format("pbxResult{0}", j + 1), image);
            AddTabPage(tabResult, image.Tag.ToString(), pictureBox);
        }

        private TabPage AddTabPage(TabControl tabControl, string name, Control controlToAdd)
        {
            string key = name;
            int counter = 1;
            while(tabResult.TabPages.ContainsKey(key))
            {
                counter++;
                key = string.Format("{0}-{1}", name, counter);
            }
            tabControl.TabPages.Add(key, key);
            var tab = tabControl.TabPages[key];
            tab.SuspendLayout();
            tab.Controls.Add(controlToAdd);
            tab.Padding = new System.Windows.Forms.Padding(3);
            tab.ResumeLayout();
            return tab;
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

            for (int i = 0; i < launcher.Count; ++i)
            {
                var deserializer = deserializerFactory.CreateDeserializer(launcher.OutputDataType[i]);
                deserializer.StartDeserialize(launcher.OutputData[i].ToString());
                var dataTables = deserializer.DeserializeDataTables(columnTypes: new System.Type[] { typeof(DateTime), typeof(string), typeof(double), typeof(double) }.ToList());

                foreach(var data in dataTables)
                    DisplayData(data);

                if (deserializer.GetType() == typeof(JSONDataSerializer))
                {
                    var images = ((JSONDataSerializer)deserializer).DeserializeImages();
                    foreach(var image in images)
                        DisplayData(image);
                }
            }

            serializer.EndDeserialize();
            lblStatus.Text = "Done";
        }
    }
}
