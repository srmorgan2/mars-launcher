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
        private const string INPUT_TAB_NAME = "tabInput";
        private const string OUTPUT_TAB_NAME = "tabOutput";

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private DataGridView CreateDataGridView(DataTable dataTable)
        {
            DataGridView dataGridView = new DataGridView();
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.RowHeadersVisible = false;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.DataSource = dataTable;
            return dataGridView;
        }

        private PictureBox CreatePictureBox(Image image)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Image = image;
            return pictureBox;
        }

        private void DisplayData(DataTable data)
        {
            DataGridView dataGridView = CreateDataGridView(data);
            AddTabPage(tabResult, data.TableName, dataGridView);
        }

        private void DisplayData(Image image)
        {
            PictureBox pictureBox = CreatePictureBox(image);
            AddTabPage(tabResult, image.Tag.ToString(), pictureBox);
        }

        private Dictionary<string, object> GetDataAsDictionary(DataGridView dataGridView)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            if (dataGridView.Columns.Count != 2)
                throw new ApplicationException("Property data grid can only have two columns.");

            foreach (DataGridViewRow dgvRow in dataGridView.Rows)
            {
                if(dgvRow.Cells[0].Value != null)
                    result[dgvRow.Cells[0].Value.ToString()] = dgvRow.Cells[1].Value;
            }
            return result;
        }

        private DataTable GetData(DataGridView dataGridView)
        {
            DataTable result = new DataTable();

            foreach (DataGridViewColumn dgvColumn in dataGridView.Columns)
                result.Columns.Add(dgvColumn.HeaderText);

            foreach (DataGridViewRow dgvRow in dataGridView.Rows)
            {
                var row = result.NewRow();
                if (dgvRow.Cells.Count > 0 && dgvRow.Cells[0].Value != null)
                {
                    for (int i = 0; i < dgvRow.Cells.Count; ++i)
                        row.SetField(i, dgvRow.Cells[i].Value);
                    result.Rows.Add(row);
                }
            }
            return result;
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
            string stdInput;
            var serializer = new JSONDataSerializer();
            serializer.StartSerialize();
            serializer.SerializeScalar("Python Script", txtPythonScript.Text);
            serializer.SerializeScalar("Root Folder", txtRootFolder.Text);
            serializer.SerializeScalar("A Number", 5.7);

            Dictionary<string, object> dict = GetDataAsDictionary(dgvInputProperties);
            DataTable inputTable = GetData(dgvInputProperties);

            serializer.SerializeDictionary("InputProperties", dict);
            serializer.SerializeDataTable(inputTable, "InputTable");
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (TabPage page in tabResult.TabPages)
            {
                if (page.Name != INPUT_TAB_NAME && page.Name != OUTPUT_TAB_NAME)
                    tabResult.TabPages.Remove(page);
            }
            txtOutput.Clear();
        }
    }
}
