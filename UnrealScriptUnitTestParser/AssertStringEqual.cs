using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    public class AssertStringEqual:IAssertion
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
            if (values.Length != 2)
            {
                result.FailureReason = Name + " awaited 2 arguments (lvalue, rvalue) but got " + values.Length;
                return result;
            }
            string lvalue = values[0];
            string rvalue = values[1];
            if (lvalue == rvalue)
            {
                result.Result = AssertionResults.Passed;
            }
            else
            {
                result.Result = AssertionResults.Failed;
                //var diffResult = Diff.DiffText(values[0], values[1], false, false, false);
                //TODO print string diffs
                result.FailureReason = String.Format("Strings are not equal", lvalue, rvalue);
            }
            return result;
        }

        #endregion
    }
}
