using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnrealScriptUnitTestParser
{
    /// <summary>
    /// 
    /// Class that parses UnrealEngine3 Logs to find unit test records and parse them
    /// </summary>
    /// <remarks>
    /// Glossary:
    /// 
    /// Example of log string:
    ///     [0024.45] ScriptLog: 
    ///     UNIT_TEST
    ///     {Location:(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay}
    ///     {Message:This is my message. Ha!}
    ///     {AssertIntEqual:5,25}
    /// 
    /// How things named in this class:
    /// 
    /// '0024.45' - time
    /// 'Location' and 'Message' have the same name
    /// 
    /// Unreal script doesn't check anything for real. It sends results of tests 
    /// and one part of information is what we assert and what the arguments to check. Like
    /// 'AssertIntEqual:5,25'
    ///                  
    ///     'AssertIntEqual' - AssertionName
    ///     '5' and '25' - AssertionValues
    ///     ',' - IntValuesSeparator
    /// 
    /// </remarks>
    public class LogParser
    {
        #region Fields
        /// <summary>
        /// Reg
        /// </summary>
        private Regex _logRecordRegex;
       

        #endregion //Fields

        #region Properties

        /// <summary>
        /// Regular expression to parse log string. 
        /// </summary>
        /// <remarks>
        /// Should include groups: 
        ///  - time
        ///  - location
        ///  - message
        ///  - assertion
        ///  - values
        /// </remarks>
        public string LogRecordRegex
        {
            get { return _logRecordRegex.ToString(); }
            set { _logRecordRegex = new Regex(value); }
        }

        /// <summary>
        /// Separator for string assert values, |=|=| by default
        /// 
        /// String separator is a sequence that separates strings. To avoid complexities with encoding and decoding string content,
        /// we just assume that, say, "|=|=|" is highly unlikely to be in a string and use it to determine string separator
        /// </summary>
        public string StringValueSeparator { get; set; }

        /// <summary>
        /// Separator for number values, ','(comma) by default
        /// 
        /// String separator is a sequence that separates strings. To avoid complexities with encoding and decoding string content,
        /// we just assume that, say, "|=|=|" is highly unlikely to be in a string and use it to determine string separator
        /// </summary>
        public string NumberValueSeparator { get; set; }

        /// <summary>
        /// Manager object that stores known assertion types
        /// </summary>
        public AssertionManger AssertionManger { get; set; }
        #endregion //Properties

        public LogParser()
        {
            LogRecordRegex = @"\[(?<time>.+)\] ScriptLog: UNIT_TEST\{Location:(?<location>.+)\}\{Message:(?<message>.*)\}\{(?<assertion>.+):(?<values>.*)\}";
            StringValueSeparator = @"|=|=|";
            NumberValueSeparator = @",";

            //Create manager and fill it with known assertions
            AssertionManger = new AssertionManger
                {
                    new AssertTrue(),
                    new AssertFalse(),
                    new AssertIntEqual(),
                    new AssertStringEqual(),
                    new AssertAlmostEqual(), 
                    new AssertVectorAlmostEqual()
                };
        }

        /// <summary>
        /// Splits entire log text to strings containing individual log records. 
        /// </summary>
        /// <param name="text">UDK log file content or something like this</param>
        /// <returns>List of  strings containing individual log records</returns>
        public static List<string> SplitLog(string text)
        {
            //requirment for string to be a new record
            var isNewRecord = new Func<string, bool>(str => str.StartsWith("[") && str.Contains("]"));

            //split text to strings
            var strings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            //1 log record might be on several lines
            var recordsStr = new List<string>();
            if (!isNewRecord(strings[0])) recordsStr.Add(""); //Add first record if needed
            foreach (string str in strings)
            {
                //It is a new record
                if (str.StartsWith("[") && str.Contains("]"))
                {
                    recordsStr.Add(str);
                }
                else
                {
                    //it is a string to the last record
                    recordsStr[recordsStr.Count - 1] += Environment.NewLine + str;
                }
            }
            return recordsStr;
        }

        /// <summary>
        /// Tests log record to be a unit test record
        /// </summary>
        /// <param name="logRecord">a string used to be a one log record</param>
        /// <returns>True if this is unit test record</returns>
        public static bool IsUnitTestRecord(string logRecord)
        {
            return logRecord.Contains("] ScriptLog: UNIT_TEST");
        }

        public List<TestInfo> ParseText(string text)
        {
            var result = new List<TestInfo>();
            var records = SplitLog(text);
            foreach (string record in records)
            {
                if (IsUnitTestRecord(record)) result.Add(ParseRecord(record));
            }
            return result;

        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UnrealScriptUnitTestParser.InvalidLogRecordException">Thrown if input string has wrong format and cannot be parsed</exception>
        /// <param name="source"></param>
        /// <returns></returns>
        public TestInfo ParseRecord(string source)
        {   
            var matches = _logRecordRegex.Matches(source);
            if(matches.Count!=1)
            {
                var message = String.Format(@"Regular expression found {0} occurances of the pattern while awaited 1. The source string '{1}' ", matches.Count, source);
                throw new InvalidLogRecordException(message);
            }
            var testInfo = new TestInfo{RecordFullText = source};
            
            var match = matches[0];

            //Parse first part
            testInfo.Time = TimeSpan.FromSeconds(Double.Parse(match.Groups["time"].Value));
            testInfo.SourceString = match.Groups["location"].Value;
            testInfo.Message = match.Groups["message"].Value;
            testInfo.AssertionName = match.Groups["assertion"].Value;

            //Obtain values array from t
            string valueStr = match.Groups["values"].Value;
            string[] values;
            if(valueStr.Contains(StringValueSeparator)) //It has several strings
            {
                values = valueStr.Split(new []{StringValueSeparator}, StringSplitOptions.RemoveEmptyEntries);
            }
            else if (valueStr.Contains(NumberValueSeparator)) //values have several ints
            {
                values = valueStr.Split(new[] { NumberValueSeparator }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                //It doesn't have any separator, it is a one value
                values = new []{valueStr};
            }
            testInfo.AssertionValues = values;

            //Run assertion!!! 
            var assertResult = AssertionManger.TestAssertion(testInfo.AssertionName, testInfo.AssertionValues);
            testInfo.Result = assertResult.Result;
            testInfo.FailureReason = assertResult.FailureReason;

            //return what we've got
            return testInfo;
        }
    }
}
