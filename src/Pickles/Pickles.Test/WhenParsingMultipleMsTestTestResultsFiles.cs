using System;
using NUnit.Framework;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.TestFrameworks;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenParsingMultipleMsTestTestResultsFiles : WhenParsingTestResultFiles<MsTestResults>
    {
        public WhenParsingMultipleMsTestTestResultsFiles()
            : base("results-example-mstest - Run 1 (failing).trx;results-example-mstest - Run 2 (passing).trx")
        {
        }

        [Test]
        public void ThenCanReadFailedFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(new Feature { Name = "Failing" });

            Assert.AreEqual(TestResult.Failed, result);
        }

        [Test]
        public void ThenCanReadPassedScenarioResultSuccessfully()
        {
          var results = ParseResultsFile();

          var scenario = new Scenario
          {
            Name = "Failing Feature Passing Scenario",
            Feature = new Feature { Name = "Failing" }
          };

          var result = results.GetScenarioResult(scenario);

          result.WasExecuted.ShouldBeTrue();
          result.WasSuccessful.ShouldBeTrue();
        }

        [Test]
        public void ThenCanReadFailedScenarioResultSuccessfully()
        {
          var results = ParseResultsFile();

          var scenario = new Scenario
          {
            Name = "Failing Feature Failing Scenario",
            Feature = new Feature { Name = "Failing" }
          };

          var result = results.GetScenarioResult(scenario);

          result.WasExecuted.ShouldBeTrue();
          result.WasSuccessful.ShouldBeFalse();
        }
    }
}