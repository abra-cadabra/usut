using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using UnrealScriptUnitTestParser;

namespace UnitTests
{   
    public class LogParserTest:TestOf<LogParser>
    {

        public const string PieceOfText = @"[0014.55] ScriptLog: UNIT_TEST{Location:(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay}{Message:IsLand 37.08941820666, -76.4730834960 (Newport News, US)}{AssertTrue:True}
[0014.55] ScriptLog: <<< 2DMap_Manager IsLand 37.08941820666, -66.4730834960 (Ocean western to Newport News, US) = False
[0014.55] ScriptLog: UNIT_TEST{Location:(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay}{Message:IsLand 37.08941820666, -66.4730834960 (Ocean western to Newport News, US)}{AssertFalse:False}
[0014.55] ScriptLog: xcGEO_GameInfo::InitGame:Geo2DMap = xcGEO_2DMap_Manager_0
                     This is multiline record
[0014.56] ScriptLog: UNIT_TEST{Location:(xcGEO_GameInfo_0) xcGEO_GameInfo::xcGEO_GameInfo:RunTests}{Message:This is sample true/false assertion}{AssertTrue:False}

";
        #region Overrides of TestBase

        /// <summary>
        /// Initialization of object under test
        /// </summary>
        public override void InitializeSystemUnderTest()
        {
            Tester = new LogParser();
        }

        /// <summary>
        /// Test setup. Called prior each test
        /// </summary>
        public override void Setup()
        {
            
        }

        /// <summary>
        /// Test cleanup. Called after each test
        /// </summary>
        public override void CleenUp()
        {

        }

        #endregion //Overrides of TestBase

        [Test]
        public void CtorTest()
        {
            Tester.AssertionManger.Should().NotBeNull();
            Tester.AssertionManger.GetAssertions().Should().NotBeEmpty();
        }

        [Test]
        public void ParseRecordTest()
        {
            var source ="[0024.45] ScriptLog: UNIT_TEST{Location:(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay}{Message:This is my message. Ha!}{AssertIntEqual:3,25}";
            var record = Tester.ParseRecord(source);
            record.AssertionName.Should().Be("AssertIntEqual");
            record.AssertionValues.Should().Contain("3");
            record.AssertionValues.Should().Contain("25");
            record.Result.Should().Be(AssertionResults.Failed);
            record.FailureReason.Should().NotBeBlank();
            record.Time.TotalSeconds.Should().BeApproximately(24.45, FloatPrec);
            record.SourceString.Should().Be("(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay");
            record.Message.Should().Be("This is my message. Ha!");
        }

        [Test]
        public void ParseTextTest()
        {
            var results = Tester.ParseText(PieceOfText);
            results.Count.Should().Be(3);
            results[0].Result.Should().Be(AssertionResults.Passed);
            results[1].Result.Should().Be(AssertionResults.Passed);
            results[2].Result.Should().Be(AssertionResults.Failed);
        }

        [Test]
        public void SplitLogTest()
        {
            var result = LogParser.SplitLog(PieceOfText);
            result.Count.Should().Be(5);

            result = LogParser.SplitLog("absdf" + PieceOfText);
            result.Count.Should().Be(5);
        }

        [Test]
        public void IsUnitTestRecordTest()
        {
            LogParser.IsUnitTestRecord("[0014.55] ScriptLog: UNIT_TEST{Location:(xcGEO_2DMap_Manager_0) xcGEO_2DMap_Manager::xcGEO_2DMap_Manager:PostBeginPlay}{Message:IsLand 37.08941820666, -76.4730834960 (Newport News, US)}{AssertTrue:True}").Should().BeTrue();
            LogParser.IsUnitTestRecord("[0014.55] ScriptLog: ha ha ha").Should().BeFalse();
        }

    }
}
