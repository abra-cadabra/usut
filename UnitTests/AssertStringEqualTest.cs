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
    public class AssertStringEqualTest
    {
        
        private AssertStringEqual _tester;

        [SetUp]
        public void SetUp()
        {
            _tester = new AssertStringEqual();
        }

        [Test]
        public void TestName()
        {
            _tester.Name.Should().Be("AssertStringEqual");
        }

        [Test]
        public void TestPassTest()
        {
            var result = _tester.Test(new [] { "This is a string", "This is a string"});
            
            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Passed);
        }

        [Test]
        public void TestFailTest()
        {
            var result = _tester.Test(new [] { "This is a string", "Aaaaa ha ha ha" });

            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Failed);
            result.FailureReason.Should().NotBeBlank();
        }
    }
}
