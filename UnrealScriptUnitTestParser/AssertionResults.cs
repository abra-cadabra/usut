using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    /// <summary>
    /// This is a result of any assertion check
    /// </summary>
    public enum AssertionResults
    {
        /// <summary>
        /// Test is passed
        /// </summary>
        Passed,
        /// <summary>
        /// Test not passed
        /// </summary>
        Failed,
        /// <summary>
        /// Some error occurred during test result check
        /// </summary>
        Error
    }
}
