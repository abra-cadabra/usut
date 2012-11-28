using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    public class AssertIntEqual:IAssertion
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
            var result = new AssertionInfo {Result = AssertionResults.Error};

            //validate arguments length
            if (values.Length != 2)
            {
                result.FailureReason = "AssertIntEqual awaited 2 arguments (lvalue, rvalue) but got " + values.Length;
                return result;
            }

            //parse arguments
            int lvalue, rvalue;
            if (!int.TryParse(values[0], out lvalue))
            {
                result.FailureReason = "AssertIntEqual was unable to parse 1st argument: " + values[0];
                return result;
            }

            if (!int.TryParse(values[1], out rvalue))
            {
                result.FailureReason = "AssertIntEqual was unable to parse 2nd argument: " + values[1];
                return result;
            }


            if (lvalue == rvalue)
            {
                result.Result = AssertionResults.Passed;
            }
            else
            {
                result.Result = AssertionResults.Failed;
                result.FailureReason = String.Format("{0} is not equal to {1}", lvalue, rvalue);
            }
            return result;
        }

        #endregion
    }
}
