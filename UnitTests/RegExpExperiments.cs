using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace UnitTests
{
    [TestFixture]
    public class TestRegExp
    {
        
        [Test]
        public void Test()
        {
            var rx = new Regex(@"\[(?<time>.+)\] ScriptLog: UNIT_TEST\{Location:(?<location>.+)\}\{Message:(?<message>.*)\}\{(?<assertion>.+):(?<value>.*)\}");
            var source ="[0024.45] ScriptLog: UNIT_TEST{Location:(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay}{Message:This is my message. Ha!}{AssertIntEqual:3,25}";
            var matches = rx.Matches(source);
            matches.Count.Should().Be(1);
            var match = matches[0];
            match.Groups.Count.Should().Be(6);

            match.Groups["time"].Value.Should().Be("0024.45");
            match.Groups["location"].Value.Should().Be("(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay");
            match.Groups["message"].Value.Should().Be("This is my message. Ha!");
            match.Groups["assertion"].Value.Should().Be("AssertIntEqual");
            match.Groups["value"].Value.Should().Be("3,25");
        }

    }
}
