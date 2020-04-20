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
        string[] ALLOWED_TYPES = { "CSV", "JSON" };
        private string _pythonExeFile;
        private string _marsFolder;

        private List<string> _errors;
        private StringBuilder _outputMessages;
        private List<StringBuilder> _outputData;
        private StringBuilder _currentOutputData;
        private List<string> _outputDataType;
        private OutputMode _outputMode;

        public String Output { get => _outputMessages.ToString(); }
        public List<StringBuilder> OutputData { get => _outputData; }
        public List<String> OutputDataType { get => _outputDataType; }
        public String Errors { get => _errors.ToString(); }
        public int Count { get => _outputData.Count; }

        public MarsLauncher(string marsFolder, string pythonProgram = @"c:/ProgramData/Anaconda3/python.exe" /*@"C:\Anaconda3\python.exe"*/)
        {
            if (!Directory.Exists(marsFolder))
                throw new ApplicationException(string.Format("MARS folder not found: {0}", marsFolder));

            _marsFolder = marsFolder;

            if (!File.Exists(pythonProgram))
                throw new ApplicationException(string.Format("Python program not found: {0}", pythonProgram));

            _pythonExeFile = pythonProgram;

            _outputMessages = new StringBuilder();
            _outputData = new List<StringBuilder>();
            _outputDataType = new List<string>();
            _errors = new List<string>();
        }


        private void p_OutputDataReceived(Object sender, DataReceivedEventArgs e)
        {
            if (e.Data == BEGIN_DATA)
            {
                _outputMode = OutputMode.Type;
            }
            else if (e.Data == END_DATA)
                _outputMode = OutputMode.Messages;

            else if (_outputMode == OutputMode.Type)
            {
                if (ALLOWED_TYPES.Contains(e.Data))
                {
                    _outputDataType.Add(e.Data);
                    _currentOutputData = new StringBuilder();
                    _outputData.Add(_currentOutputData);
                    _outputMode = OutputMode.Data;
                }
                else
                    throw new ApplicationException("Unexpected output type: " + e.Data);
            }

            else if (_outputMode == OutputMode.Messages)
                _outputMessages.AppendLine(e.Data);

            else if (_outputMode == OutputMode.Data)
                _currentOutputData.AppendLine(e.Data);

            else
                throw new ApplicationException("Unexpected output mode: " + _outputMode.ToString());
        }

        private void p_ErrorDataReceived(Object sender, DataReceivedEventArgs e)
        {
            _errors.Add(e.Data);
        }

        public DataSet Run(string pythonFile, string stdInput)
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
            process.StartInfo.Arguments = pythonFile + " " + _marsFolder;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            process.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            process.Start();
            StreamWriter myStreamWriter = process.StandardInput;
            myStreamWriter.Write(stdInput);
            myStreamWriter.Close();

            process.BeginOutputReadLine();
            process.WaitForExit();

            return result;
        }
    }

    public enum OutputMode
    {
        Messages,
        Type,
        Data
    }
}
