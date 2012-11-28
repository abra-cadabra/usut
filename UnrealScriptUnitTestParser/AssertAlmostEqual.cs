using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    public class AssertAlmostEqual:IAssertion
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
            var result = new AssertionInfo();
            result.Result = AssertionResults.Error; //fo future validation

            //validate arguments length
            if(values.Length !=3)
            {   
                result.FailureReason = "AssertAlmostEqual awaited 3 arguments (lvalue, rvalue, delta) but got " + values.Length;
                return result;
            }

            //parse arguments
            double lvalue, rvalue, delta;
            double[] parsedValues = new double[3];
            for (int i = 0; i < 3; i++)
            {
                if (!Double.TryParse(values[i], NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValues[i]))
                {
                    result.FailureReason = "AssertAlmostEqual was unable to parse "+i+" argument: " + values[0];
                    return result;
                }
            }
            lvalue = parsedValues[0];
            rvalue = parsedValues[1];
            delta = parsedValues[2];

            double realDelta = Math.Abs(lvalue - rvalue);
            delta = Math.Abs(delta);
            if (Math.Abs(realDelta) <= delta)
            {
                result.Result = AssertionResults.Passed;
            }
            else
            {
                result.Result = AssertionResults.Failed;
                result.FailureReason = String.Format("Values {0} and {1} differ for {2}. It is {3} bigger than required delta {4}", lvalue, rvalue, realDelta, Math.Abs(realDelta - delta), delta);
            }
            return result;
        }

        #endregion
    }
}
