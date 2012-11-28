using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    public class AssertTrue:IAssertion
    {
        #region Implementation of IAssertion

        /// <summary>
        /// String like "AssertTrue" as it appears in UDK log
        /// </summary>
        public string Name { get { return GetType().Name; } }

        /// <summary>
        /// Function provides test of assertion
        /// </summary>
        /// <param name="values">assertion values as they appear in UDK log</param>
        /// <returns>Never null TestInfo with test results</returns>
        public AssertionInfo Test(string[] values)
        {
            var result = new AssertionInfo { Result = AssertionResults.Error };

            //validate arguments length
            if (values.Length != 1)
            {
                result.FailureReason = Name + " awaited 1 argument but got " + values.Length;
                return result;
            }

            if (values[0].ToLower() == "true")
            {
                result.Result = AssertionResults.Passed;
            }
            else
            {
                result.Result = AssertionResults.Failed;
                result.FailureReason = String.Format("Value is not true it is " + values[0]);
            }
            return result;
        }

        #endregion
    }
}
