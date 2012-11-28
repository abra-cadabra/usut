using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    public class AssertVectorAlmostEqual:IAssertion
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
            result.Result = AssertionResults.Error; //for future validation

            //validate arguments length
            if(values.Length !=7)
            {
                result.FailureReason = "AssertVectorAlmostEqual awaited 7 arguments (x1, y1, z1, x2, y2, z2, delta)  but got " + values.Length;
                return result;
            }

            //parse arguments
            
            double[] parsedValues = new double[7];
            for (int i = 0; i < 7; i++)
            {
                if (!Double.TryParse(values[i], NumberStyles.Any, CultureInfo.InvariantCulture, out parsedValues[i]))
                {
                    result.FailureReason = "AssertVectorAlmostEqual was unable to parse " + i + " argument: " + values[0];
                    return result;
                }
            }

            var v1 = new double[3];
            var v2 = new double[3];
            v1[0] = parsedValues[0];
            v1[1] = parsedValues[1];
            v1[2] = parsedValues[2];
            v2[0] = parsedValues[3];
            v2[1] = parsedValues[4];
            v2[2] = parsedValues[5];
            double delta = parsedValues[6];

            for (int i = 0; i < 3; i++)
            {
                double realDelta = Math.Abs(v1[i] - v2[i]);
                delta = Math.Abs(delta);
                if (Math.Abs(realDelta) > delta)
                {   
                    result.Result = AssertionResults.Failed;
                    result.FailureReason = String.Format("Values {0} and {1} differ for {2}. It is {3} bigger than required delta {4}", v1[i], v2[i], realDelta, Math.Abs(realDelta - delta), delta);
                    result.FailureReason += String.Format("Comparision [x1={0} x2={1}] [y1={2}, y2={3}] [z1={4}, z2={5}] delta={6}", v1[0], v2[0], v1[1], v2[1], v1[2], v2[2], delta);
                    return result;
                }
                
            }

            result.Result = AssertionResults.Passed;
            return result;
        }

        #endregion
    }
}
