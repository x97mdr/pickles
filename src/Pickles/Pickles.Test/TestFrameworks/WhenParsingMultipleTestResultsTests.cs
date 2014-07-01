//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenParsingMultipleTestResultsTests.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//  
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO.Abstractions;

using Moq;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

using Should;

namespace PicklesDoc.Pickles.Test.TestFrameworks
{
  [TestFixture]
  public class WhenParsingMultipleTestResultsTests
  {
    [Test]
    public void GetFeatureResult_OnePassingOneInconclusive_ReturnsPassed()
    {
      var feature = new Feature();

      var testResults1 = SetupStubForGetFeatureResult(feature, TestResult.Passed);
      var testResults2 = SetupStubForGetFeatureResult(feature, TestResult.Inconclusive);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetFeatureResult(feature);

      result.ShouldEqual(TestResult.Passed);
    }

    private static MultipleTestResults CreateMultipleTestResults(ITestResults testResults1, ITestResults testResults2)
    {
      return new TestableMultipleTestResults(new[] { testResults1, testResults2 });
    }

    private class TestableMultipleTestResults : MultipleTestResults
    {
      public TestableMultipleTestResults(IEnumerable<ITestResults> testResults)
        : base(false, testResults)
      {
      }

      public override TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues)
      {
        throw new NotSupportedException();
      }

      protected override ITestResults ConstructSingleTestResult(FileInfoBase fileInfo)
      {
        // not needed since we use the other constructor
        throw new NotSupportedException();
      }
    }

    private static Mock<ITestResults> SetupStubForGetFeatureResult(Feature feature, TestResult resultOfGetFeatureResult)
    {
      var testResults1 = new Mock<ITestResults>();
      testResults1.Setup(ti => ti.GetFeatureResult(feature)).Returns(resultOfGetFeatureResult);
      return testResults1;
    }

    [Test]
    public void GetFeatureResult_OnePassingOneFailed_ReturnsPassed()
    {
      var feature = new Feature();

      var testResults1 = SetupStubForGetFeatureResult(feature, TestResult.Passed);
      var testResults2 = SetupStubForGetFeatureResult(feature, TestResult.Failed);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetFeatureResult(feature);

      result.ShouldEqual(TestResult.Failed);
    }

    [Test]
    public void GetFeatureResult_TwoInconclusive_ReturnsInconclusive()
    {
      var feature = new Feature();

      var testResults1 = SetupStubForGetFeatureResult(feature, TestResult.Inconclusive);
      var testResults2 = SetupStubForGetFeatureResult(feature, TestResult.Inconclusive);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetFeatureResult(feature);

      result.ShouldEqual(TestResult.Inconclusive);
    }

    [Test]
    public void GetScenarioOutlineResult_OnePassingOneInconclusive_ReturnsPassed()
    {
      var scenarioOutline = new ScenarioOutline();

      var testResults1 = SetupStubForGetScenarioOutlineResult(TestResult.Passed);
      var testResults2 = SetupStubForGetScenarioOutlineResult(TestResult.Inconclusive);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetScenarioOutlineResult(scenarioOutline);

      result.ShouldEqual(TestResult.Passed);
    }

    private static Mock<ITestResults> SetupStubForGetScenarioOutlineResult(TestResult resultOfGetFeatureResult)
    {
      var testResults1 = new Mock<ITestResults>();
      testResults1.Setup(ti => ti.GetScenarioOutlineResult(It.IsAny<ScenarioOutline>())).Returns(resultOfGetFeatureResult);
      return testResults1;
    }

    [Test]
    public void GetScenarioOutlineResult_OnePassingOneFailing_ReturnsFailed()
    {
      var scenarioOutline = new ScenarioOutline();

      var testResults1 = SetupStubForGetScenarioOutlineResult(TestResult.Passed);
      var testResults2 = SetupStubForGetScenarioOutlineResult(TestResult.Failed);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetScenarioOutlineResult(scenarioOutline);

      result.ShouldEqual(TestResult.Failed);
    }

    [Test]
    public void GetScenarioOutlineResult_TwoInconclusive_ReturnsInconclusive()
    {
      var scenarioOutline = new ScenarioOutline();

      var testResults1 = SetupStubForGetScenarioOutlineResult(TestResult.Inconclusive);
      var testResults2 = SetupStubForGetScenarioOutlineResult(TestResult.Inconclusive);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetScenarioOutlineResult(scenarioOutline);

      result.ShouldEqual(TestResult.Inconclusive);
    }

    [Test]
    public void GetScenarioResult_OnePassingOneInconclusive_ReturnsPassed()
    {
      var scenario = new Scenario();

      var testResults1 = SetupStubForGetScenarioResult(TestResult.Passed);
      var testResults2 = SetupStubForGetScenarioResult(TestResult.Inconclusive);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetScenarioResult(scenario);

      result.ShouldEqual(TestResult.Passed);
    }

    private static Mock<ITestResults> SetupStubForGetScenarioResult(TestResult resultOfGetFeatureResult)
    {
      var testResults1 = new Mock<ITestResults>();
      testResults1.Setup(ti => ti.GetScenarioResult(It.IsAny<Scenario>())).Returns(resultOfGetFeatureResult);
      return testResults1;
    }

    [Test]
    public void GetScenarioResult_OnePassingOneFailing_ReturnsFailed()
    {
      var scenario = new Scenario();

      var testResults1 = SetupStubForGetScenarioResult(TestResult.Passed);
      var testResults2 = SetupStubForGetScenarioResult(TestResult.Failed);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetScenarioResult(scenario);

      result.ShouldEqual(TestResult.Failed);
    }

    [Test]
    public void GetScenarioResult_TwoInconclusive_ReturnsInconclusive()
    {
      var scenario = new Scenario();

      var testResults1 = SetupStubForGetScenarioResult(TestResult.Inconclusive);
      var testResults2 = SetupStubForGetScenarioResult(TestResult.Inconclusive);

      ITestResults multipleTestResults = CreateMultipleTestResults(testResults1.Object, testResults2.Object);

      var result = multipleTestResults.GetScenarioResult(scenario);

      result.ShouldEqual(TestResult.Inconclusive);
    }
  }
}