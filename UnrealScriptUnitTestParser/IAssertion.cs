using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    /// <summary>
    /// Public interface for assertion class
    /// </summary>
    public interface IAssertion
    {
        /// <summary>
        /// String like "AssertTrue" as it appears in UDK log
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Function provides test of assertion
        /// </summary>
        /// <param name="values">assertion values as they appear in UDK log</param>
        /// <returns>Never null TestInfo with test results</returns>
        AssertionInfo Test(string[] values);
    }
}
