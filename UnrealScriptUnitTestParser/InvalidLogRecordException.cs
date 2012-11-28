using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnrealScriptUnitTestParser
{
    class InvalidLogRecordException:Exception
    {
        public TestInfo RecoveredTest { get; set; }

        public InvalidLogRecordException(string message):base(message)
        {
            RecoveredTest = null;
        }

        public InvalidLogRecordException(string message, Exception innerException):base(message, innerException)
        {
        }
    }
}
