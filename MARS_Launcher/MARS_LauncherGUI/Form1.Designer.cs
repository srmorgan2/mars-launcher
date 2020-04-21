namespace MARS_LauncherGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPythonScript = new System.Windows.Forms.ComboBox();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtPythonProgram = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabResult = new System.Windows.Forms.TabControl();
            this.tabInput = new System.Windows.Forms.TabPage();
            this.dgvInputTable = new System.Windows.Forms.DataGridView();
            this.dgvInputProperties = new System.Windows.Forms.DataGridView();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.dgvColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvColValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabResult.SuspendLayout();
            this.tabInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputProperties)).BeginInit();
            this.tabOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(897, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 26);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "MARS root folder";
            // 
            // txtPythonScript
            // 
            this.txtPythonScript.FormattingEnabled = true;
            this.txtPythonScript.Items.AddRange(new object[] {
            "MARS_Tools\\VerySimpleSample.py",
            "MARS_Tools\\MultiFrameSample.py"});
            this.txtPythonScript.Location = new System.Drawing.Point(157, 101);
            this.txtPythonScript.Margin = new System.Windows.Forms.Padding(4);
            this.txtPythonScript.Name = "txtPythonScript";
            this.txtPythonScript.Size = new System.Drawing.Size(496, 24);
            this.txtPythonScript.TabIndex = 2;
            this.txtPythonScript.Text = "MARS_Tools\\VerySimpleSample.py";
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Location = new System.Drawing.Point(157, 69);
            this.txtRootFolder.Margin = new System.Windows.Forms.Padding(4);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.Size = new System.Drawing.Size(604, 22);
            this.txtRootFolder.TabIndex = 3;
            this.txtRootFolder.Text = "C:\\Users\\steve\\Projects\\mars-launcher\\MARS";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(663, 101);
            this.btnRun.Margin = new System.Windows.Forms.Padding(4);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 28);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 107);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Python script";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 737);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(897, 25);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 20);
            this.lblStatus.Text = "Ready";
            // 
            // txtPythonProgram
            // 
            this.txtPythonProgram.Location = new System.Drawing.Point(157, 37);
            this.txtPythonProgram.Margin = new System.Windows.Forms.Padding(4);
            this.txtPythonProgram.Name = "txtPythonProgram";
            this.txtPythonProgram.Size = new System.Drawing.Size(604, 22);
            this.txtPythonProgram.TabIndex = 10;
            this.txtPythonProgram.Text = "C:/ProgramData/Anaconda3/python.exe";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 41);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Python program";
            // 
            // tabResult
            // 
            this.tabResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabResult.Controls.Add(this.tabInput);
            this.tabResult.Controls.Add(this.tabOutput);
            this.tabResult.Location = new System.Drawing.Point(12, 136);
            this.tabResult.Name = "tabResult";
            this.tabResult.SelectedIndex = 0;
            this.tabResult.Size = new System.Drawing.Size(873, 595);
            this.tabResult.TabIndex = 11;
            // 
            // tabInput
            // 
            this.tabInput.Controls.Add(this.dgvInputTable);
            this.tabInput.Controls.Add(this.dgvInputProperties);
            this.tabInput.Location = new System.Drawing.Point(4, 25);
            this.tabInput.Name = "tabInput";
            this.tabInput.Size = new System.Drawing.Size(865, 566);
            this.tabInput.TabIndex = 2;
            this.tabInput.Text = "Input";
            this.tabInput.UseVisualStyleBackColor = true;
            // 
            // dgvInputTable
            // 
            this.dgvInputTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInputTable.Location = new System.Drawing.Point(307, 0);
            this.dgvInputTable.Name = "dgvInputTable";
            this.dgvInputTable.RowTemplate.Height = 24;
            this.dgvInputTable.Size = new System.Drawing.Size(558, 566);
            this.dgvInputTable.TabIndex = 1;
            // 
            // dgvInputProperties
            // 
            this.dgvInputProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInputProperties.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColName,
            this.dgvColValue});
            this.dgvInputProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.dgvInputProperties.Location = new System.Drawing.Point(0, 0);
            this.dgvInputProperties.Name = "dgvInputProperties";
            this.dgvInputProperties.RowTemplate.Height = 24;
            this.dgvInputProperties.Size = new System.Drawing.Size(307, 566);
            this.dgvInputProperties.TabIndex = 0;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.txtOutput);
            this.tabOutput.Location = new System.Drawing.Point(4, 25);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Padding = new System.Windows.Forms.Padding(3);
            this.tabOutput.Size = new System.Drawing.Size(865, 566);
            this.tabOutput.TabIndex = 0;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.Location = new System.Drawing.Point(3, 3);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(859, 560);
            this.txtOutput.TabIndex = 8;
            this.txtOutput.Text = "";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(771, 101);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // dgvColName
            // 
            this.dgvColName.HeaderText = "Name";
            this.dgvColName.Name = "dgvColName";
            // 
            // dgvColValue
            // 
            this.dgvColValue.HeaderText = "Value";
            this.dgvColValue.Name = "dgvColValue";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 762);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.tabResult);
            this.Controls.Add(this.txtPythonProgram);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtRootFolder);
            this.Controls.Add(this.txtPythonScript);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "MARS Launcher";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabResult.ResumeLayout(false);
            this.tabInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInputProperties)).EndInit();
            this.tabOutput.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox txtPythonScript;
        private System.Windows.Forms.TextBox txtRootFolder;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.TextBox txtPythonProgram;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabResult;
        private System.Windows.Forms.TabPage tabOutput;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.TabPage tabInput;
        private System.Windows.Forms.DataGridView dgvInputProperties;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvInputTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvColValue;
    }
}

