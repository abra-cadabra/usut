using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using UnrealScriptUnitTestParser;

namespace UnitTests
{
    [TestFixture]
    public class AssertAlmostEqualTest
    {
        
        private AssertAlmostEqual _tester;

        [SetUp]
        public void SetUp()
        {
            _tester = new AssertAlmostEqual();
        }

        [Test]
        public void TestName()
        {
            _tester.Name.Should().Be("AssertAlmostEqual");
        }

        [Test]
        public void TestPassTest()
        {
            var result = _tester.Test(new [] {"1.2345611", "1.2345699", "0.0005"});
            
            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Passed);
        }

        [Test]
        public void TestFailTest()
        {
            var result = _tester.Test(new string[] { "1.2345611", "1.2345699", "0.000000005" });

            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Failed);
            result.FailureReason.Should().NotBeBlank();

            result = _tester.Test(new string[] { "1.2345611", "-1.2345699", "0.000000005" });

            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Failed);
            result.FailureReason.Should().NotBeBlank();
        }
    }
}
