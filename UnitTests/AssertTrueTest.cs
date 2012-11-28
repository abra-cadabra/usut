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
    public class AssertTrueTest
    {
        
        private AssertTrue _tester;

        [SetUp]
        public void SetUp()
        {
            _tester = new AssertTrue();
        }

        [Test]
        public void TestName()
        {
            _tester.Name.Should().Be("AssertTrue");
        }

        [Test]
        public void TestPassTest()
        {
            var result = _tester.Test(new [] {"True"});
            
            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Passed);
        }

        [Test]
        public void TestFailTest()
        {
            var result = _tester.Test(new [] {"False"});

            //Pass test
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Failed);
            result.FailureReason.Should().NotBeBlank();
        }
    }
}
