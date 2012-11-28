using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    /// <summary>
    /// Result of each test
    /// </summary>
    public class AssertionInfo
    {
        /// <summary>
        /// Test result of the assertion
        /// </summary>
        public AssertionResults Result { get; set; }

        /// <summary>
        /// If something is failed it is a reason
        /// </summary>
        public string FailureReason { get; set; }
    }
}
