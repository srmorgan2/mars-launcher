using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void btnRun_Click(object sender, EventArgs e)
        {
            var launcher = new MarsLauncher(txtRootFolder.Text, txtPythonProgram.Text);
            //string xml;
            //xml = "Dummy";

            string stdInput;
            var serializer = new JSONComponentSerializer();
            serializer.startSerialize();
            serializer.serialize(txtPythonScript, "Python Script");
            serializer.serialize(txtRootFolder, "Root Folder");
            //DataTable dtFromGrid = new DataTable();
            //dtFromGrid = dataGridView1.DataSource as DataTable;//?? new DataTable();
            serializer.serialize(dataGridView1, "Curve");
            stdInput = serializer.endSerialize();

            txtOutput.Text = "Launching " + txtPythonScript.Text;
            lblStatus.Text = "Running " + txtPythonScript.Text;
            DataSet result = launcher.Run(txtPythonScript.Text, stdInput);
            txtOutput.Text = launcher.Output;

            // Deserialize the DataFrame "Output Discount Factors" and display it
            System.Type[] colTypes = new System.Type[] { typeof(string), typeof(double) };
            serializer.startDeserialize(launcher.Output);
            DataTable outputTable = serializer.deserialize("Output Discount Factors", colTypes);
            dataGridView2.DataSource = outputTable;
            lblStatus.Text = "Done";
        }
    }
}
