using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MARS_LauncherLib
{
    /*----------------------------------
     * A class the laucn Python scripts
     * from the MARS library
     * 
     * Johary 6/4/2020
     * ---------------------------------*/
    public class MarsLauncher
    {
        const string BEGIN_DATA = "<<<<Data";
        const string END_DATA = "Data>>>";
        private string _pythonExeFile;
        private string _marsFolder;

        private List<string> _errors;
        private StringBuilder _outputMessages;
        private StringBuilder _outputData;
        private OutputMode _outputMode;

        public String Output { get => _outputMessages.ToString(); }
        public String OutputData { get => _outputData.ToString(); }
        public String Errors { get => _errors.ToString(); }

        public MarsLauncher(string marsFolder, string pythonProgram = @"C:\Anaconda3\python.exe")
        {
            if (!Directory.Exists(marsFolder))
                throw new ApplicationException(string.Format("MARS folder not found: {0}", marsFolder));

            _marsFolder = marsFolder;

            if (!File.Exists(pythonProgram))
                throw new ApplicationException(string.Format("Python program not found: {0}", pythonProgram));

            _pythonExeFile = pythonProgram;

            _outputMessages = new StringBuilder();
            _outputData = new StringBuilder();
            _errors = new List<string>();
        }


        private void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            if (e.Data == BEGIN_DATA)
                _outputMode = OutputMode.Data;
            
            else if (e.Data == END_DATA)
                _outputMode = OutputMode.Messages;

            else if (_outputMode == OutputMode.Messages)
                _outputMessages.AppendLine(e.Data);

            else if (_outputMode == OutputMode.Data)
                _outputData.AppendLine(e.Data);

            else
                throw new ApplicationException("Unexpected output mode: " + _outputMode.ToString());
        }

        public DataSet Run(string pythonFile, string inputXml)
        {
            _errors.Clear();
            _outputMessages.Clear();
            _outputData.Clear();
            _outputMode = OutputMode.Messages;

            Directory.SetCurrentDirectory(_marsFolder);

            if (!File.Exists(pythonFile))
                throw new ApplicationException(string.Format("File not found: {0} in folder {1}",
                    pythonFile, _marsFolder));

            DataSet result = new DataSet();

            var process = new Process();
            process.StartInfo.WorkingDirectory = _marsFolder;
            process.StartInfo.FileName = _pythonExeFile;
            process.StartInfo.Arguments = pythonFile +" " + _marsFolder;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);

            process.Start();
            process.BeginOutputReadLine();
            process.WaitForExit();

            return result;
        }
    }

    public enum OutputMode
    {
        Messages,
        Data
    }
}
