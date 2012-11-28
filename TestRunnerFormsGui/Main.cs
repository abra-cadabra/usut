using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnrealScriptUnitTestParser;

namespace TestRunnerFormsGui
{
    public partial class Main : Form
    {
        public const string OutputMessagePre = "==>  ";
        #region Fields
        private string _currentFileName;
        /// <summary>
        /// Cell style when test is failed
        /// </summary>
        private DataGridViewCellStyle _failedStyle;
        /// <summary>
        /// Cell style when there is error in test
        /// </summary>
        private DataGridViewCellStyle _errorStyle;
        /// <summary>
        /// Cell style when test is succeded
        /// </summary>
        private DataGridViewCellStyle _passedStyle;

        #endregion

        #region Properties

        #endregion

        #region Functions


        public Main()
        {
            InitializeComponent();

            _currentFileName = "";
            _failedStyle = new DataGridViewCellStyle(Grid.DefaultCellStyle);
            _failedStyle.ForeColor = _failedStyle.SelectionForeColor =  Color.Red;
            _passedStyle = new DataGridViewCellStyle(Grid.DefaultCellStyle);
            _passedStyle.ForeColor = _passedStyle.SelectionForeColor = Color.Green;
            _errorStyle = new DataGridViewCellStyle(Grid.DefaultCellStyle);
            _errorStyle.ForeColor = _errorStyle.SelectionForeColor = Color.Goldenrod;
        }

        private void AddLogLine(string text)
        {
            OutputTextBox.Text += OutputMessagePre + text + Environment.NewLine;
        }

        private void Process()
        {
            var parser = new LogParser();

            //Check file exists
            if (!File.Exists(_currentFileName))
            {
                AddLogLine(String.Format("ERROR processing file '{0}', file does not exist", _currentFileName));
                return;
            }

            //Get file content
            string fileText;
            try
            {
                AddLogLine(String.Format("Opening file '{0}'",_currentFileName));
                fileText = File.ReadAllText(_currentFileName);
            }
            catch (Exception ex)
            {
                AddLogLine(String.Format("ERROR Opening file '{0}'. Reason: {1}", _currentFileName, ex));
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
            Process();
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            Process();
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
