using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UnrealScriptUnitTestParser;

namespace TestRunnerFormsGui
{
    public partial class Main : Form
    {
        #region Constants
        
        /// <summary>
        /// This thing is added to the beginning of each output log line
        /// </summary>
        private const string OutputMessagePre = "==>  ";

        #endregion //Constants

        #region Fields
        /// <summary>
        /// File name with UDK log
        /// </summary>
        private string _currentFileName;

        /// <summary>
        /// Cell style when test is failed
        /// </summary>
        private readonly DataGridViewCellStyle _failedStyle;

        /// <summary>
        /// Cell style when there is error in test
        /// </summary>
        private readonly DataGridViewCellStyle _errorStyle;

        /// <summary>
        /// Cell style when test is succeeded
        /// </summary>
        private readonly DataGridViewCellStyle _passedStyle;
        #endregion

        #region Properties

        #endregion

        #region Functions
        
        public Main()
        {
            InitializeComponent();

            //Create cell style colors
            _failedStyle = new DataGridViewCellStyle(Grid.DefaultCellStyle);
            _failedStyle.ForeColor = _failedStyle.SelectionForeColor =  Color.Red;
            _passedStyle = new DataGridViewCellStyle(Grid.DefaultCellStyle);
            _passedStyle.ForeColor = _passedStyle.SelectionForeColor = Color.Green;
            _errorStyle = new DataGridViewCellStyle(Grid.DefaultCellStyle);
            _errorStyle.ForeColor = _errorStyle.SelectionForeColor = Color.Goldenrod;

            //Now what is a file name?
            _currentFileName = "";
            FileLabel.Text = "";
            try
            {
                // Open file from command line?
                var args = Environment.GetCommandLineArgs();
                if (args.Length != 0)
                {
                    _currentFileName = args[0];
                    ProcessFile(_currentFileName);
                }
            }
            catch (NotSupportedException)
            {
                //Command line arguments are not supported by system. Eat it
            }
        }


        /// <summary>
        /// Parse file 
        /// </summary>
        /// <param name="fileName"></param>
        public void ProcessFile(string fileName)
        {
            var parser = new LogParser();

            //Adjust current file name
            if (_currentFileName != fileName)
            {
                _currentFileName = fileName;
                FileLabel.Text = fileName;
            }

            //Check file exists
            if (!File.Exists(fileName))
            {
                AddLogLine(String.Format("ERROR processing file '{0}', file does not exist", fileName));
                return;
            }

            //Get file content
            string fileText;
            try
            {
                AddLogLine(String.Format("Opening file '{0}'",fileName));
                fileText = File.ReadAllText(fileName);
            }
            catch (Exception ex)
            {
                AddLogLine(String.Format("ERROR Opening file '{0}'. Reason: {1}", fileName, ex));
                return;
            }

            //Parse file 
            try
            {
                AddLogLine("Parsing...");
                var testResults = parser.ParseText(fileText);
                AddLogLine(String.Format("Found '{0}' records", testResults.Count));
                ShowResults(testResults);
                
                //calculate
                AddLogLine(String.Format("{0} passed, {1} failed, {2} errors",
                                         testResults.Count(p=>p.Result == AssertionResults.Passed),
                                         testResults.Count(p=>p.Result == AssertionResults.Failed),
                                         testResults.Count(p=>p.Result == AssertionResults.Error)));

            }
            catch (Exception ex)
            {
                AddLogLine(String.Format("ERROR parsing tests. Reason: {0}", ex));
            }
        }

        /// <summary>
        /// Adds a line to a bottom output window
        /// </summary>
        /// <param name="text"></param>
        private void AddLogLine(string text)
        {
            OutputTextBox.Text += OutputMessagePre + text + Environment.NewLine;
        }

        private void ShowTestInfo(TestInfo test)
        {
            string text = String.Format(
                "Time:     {7}{0}{0}" + //TODO right order
                "Result :  {1}{0}{0}" +
                "Type:     {2}{0}{0}" +
                "Failure:  {3}{0}{0}" +
                "Values:   {4}{0}{0}" +
                "Message:  {6}{0}{0}" +
                "Source:   {5}{0}{0}",
               
                Environment.NewLine,
                test.Result,
                test.AssertionName,
                test.FailureReason??"",
                test.AssertionValues.Aggregate((a, b) => a + ", " + b),
                test.SourceString,
                test.Message,
                test.Time
                );

            TestInfoTextBox.Text = text;

        }

        /// <summary>
        /// Puts test results to grid vies
        /// </summary>
        /// <param name="testInfos">List of testInfo - information of test passing</param>
        private void ShowResults(List<TestInfo> testInfos)
        {
            Grid.Rows.Clear();
            if (testInfos.Count == 0) return;
            
            foreach (TestInfo test in testInfos)
            {
                //test.Result 
                var row = Grid.Rows[Grid.Rows.Add()];
                row.Cells[ColumnResult.Index].Value = test.Result.ToString();
                row.Cells[GridColumnSource.Index].Value = test.SourceString;
                row.Cells[ColumnMessage.Index].Value = test.Message;
                row.Cells[ColumnTime.Index].Value = test.Time;

                //Colorize 
                switch (test.Result)
                {
                    case AssertionResults.Passed:
                        row.Cells[ColumnResult.Index].Style = _passedStyle;
                        break;
                    case AssertionResults.Failed:
                        row.Cells[ColumnResult.Index].Style = _failedStyle;
                        break;
                    case AssertionResults.Error:
                        row.Cells[ColumnResult.Index].Style = _errorStyle;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                //Add data to row.tag
                row.Tag = test;
            }
            ShowTestInfo(testInfos[0]);
            Grid.ClearSelection();
        }

        #region Event handlers
        // ReSharper disable InconsistentNaming

        private void FileButton_Click(object sender, EventArgs e)
        {
            FileDialog.FileName = _currentFileName;
            var result = FileDialog.ShowDialog(this);
            if(result != DialogResult.OK) return;

            _currentFileName = FileDialog.FileName;
            FileLabel.Text = _currentFileName;
            ReloadButton.Enabled = true;
            ProcessFile(_currentFileName);
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            ProcessFile(_currentFileName);
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if(Grid.SelectedRows.Count<=0) return;
            var row = Grid.SelectedRows[0];
            if (row.Tag == null) return;
            ShowTestInfo((TestInfo) row.Tag);
        }

        // ReSharper restore InconsistentNaming
        #endregion //Event handlers

        
        #endregion //Functions
    }
}
