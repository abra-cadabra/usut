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
    public class AssertIntEqualTest
    {

        private AssertIntEqual _tester;

        [SetUp]
        public void SetUp()
        {
            _tester = new AssertIntEqual();
        }

        [Test]
        public void TestName()
        {
            _tester.Name.Should().Be("AssertIntEqual");
        }

        [Test]
        public void TestPassTest()
        {
            var result = _tester.Test(new [] {"135", "135"});
            
            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Passed);
        }

        [Test]
        public void TestFailTest()
        {
            var result = _tester.Test(new [] {"135", "136"});

            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Failed);
            result.FailureReason.Should().NotBeBlank();
        }
    }
}
