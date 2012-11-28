using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentAssertions;
using UnrealScriptUnitTestParser;

namespace UnitTests
{
    [TestFixture]
    public class AssertionManagerTest
    {
        private AssertionManger _tester;

        [SetUp]
        public void SetUp()
        {
            _tester = new AssertionManger();
        }

        public void FillTester()
        {
            _tester.Add(new AssertTrue());
            _tester.Add(new AssertFalse());
            _tester.Add(new AssertIntEqual());
            _tester.Add(new AssertStringEqual());
            _tester.Add(new AssertAlmostEqual());
        }

        [Test]
        public void CtorTest()
        {
            _tester = new AssertionManger();
            _tester.GetAssertions().Should().BeEmpty();
            _tester.Count.Should().Be(0);
        }

        [Test]
        public void BasicOperationTest()
        {
            var assert = new AssertTrue();
            _tester.Add(assert);
            _tester.Count.Should().Be(1);
            _tester["AssertTrue"].Should().NotBeNull();
            _tester.Clear();
            _tester.Count.Should().Be(0);
            FillTester();
            _tester.Count.Should().Be(5);
            _tester.Remove("AssertTrue").Should().BeTrue();
            _tester.Count.Should().Be(4);
            _tester.Add(assert);
            _tester.Remove(assert);
            _tester.Count.Should().Be(4);
        }

        [Test]
        public void RunAssertionsTest()
        {
            FillTester();

            //Pass test
            var result = _tester.TestAssertion("AssertTrue", new[] { "True" });
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Passed);

            //Fail test
            result = _tester.TestAssertion("AssertTrue", new[] { "False" });
            result.Should().NotBeNull();
            result.Result.Should().Be(AssertionResults.Failed);
            result.FailureReason.Should().NotBeBlank();
        }
    }
}
