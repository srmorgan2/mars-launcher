using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gemini
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void amrketDateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pythonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new MARS_LauncherGUI.Form1();
            f.Show();
        }
    }
}
