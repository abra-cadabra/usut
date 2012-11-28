using System;

namespace UnrealScriptUnitTestParser
{
    /// <summary>
    /// Represents one unit test record
    /// </summary>
    public class TestInfo
    {
        /// <summary>
        /// Full text of a record
        /// </summary>
        public string RecordFullText { get; set; }

        /// <summary>
        /// Test message (or description)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Time when log record with this unit test is occurred
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Full string of a source
        /// </summary>
        public string SourceString { get; set; }
        
        /// <summary>
        /// Assertion name is like "AssertTrue" as it appears in UDK log
        /// </summary>
        public string AssertionName { get; set; }

        /// <summary>
        /// The values to check for assertion
        /// </summary>
        public string[] AssertionValues { get; set; }
        
        /// <summary>
        /// Test result of the assertion
        /// </summary>
        public AssertionResults Result { get; set; }

        /// <summary>
        /// If test is failed it is a reason
        /// </summary>
        public string FailureReason { get; set; }

        public TestInfo()
        {
            AssertionValues = new string[0];
        }
    }
}
