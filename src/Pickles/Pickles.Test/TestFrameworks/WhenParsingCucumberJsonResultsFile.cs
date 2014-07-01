using System;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

using Should;

namespace PicklesDoc.Pickles.Test.TestFrameworks
{
    [TestFixture]
    class WhenParsingCucumberJsonResultsFile : WhenParsingTestResultFiles<CucumberJsonResults>
    {
        public WhenParsingCucumberJsonResultsFile()
            : base("results-example-json.json")
        {
        }

        [Test]
        public void ThenCanReadFeatureResultSuccesfully()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Test Feature" };
            TestResult result = results.GetFeatureResult(feature);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Test Feature" };

            var scenario1 = new Scenario { Name = "Passing", Feature = feature };
            TestResult result1 = results.GetScenarioResult(scenario1);

            result1.WasExecuted.ShouldBeTrue();
            result1.WasSuccessful.ShouldBeTrue();

            var scenario2 = new Scenario { Name = "Failing", Feature = feature };
            TestResult result2 = results.GetScenarioResult(scenario2);

            result2.WasExecuted.ShouldBeTrue();
            result2.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadFeatureWithoutScenariosSuccessfully_ShouldReturnInconclusive()
        {
            var results = ParseResultsFile();
            var feature = new Feature { Name = "Feature Without Scenarios" };

            TestResult result = results.GetFeatureResult(feature);

            result.ShouldEqual(TestResult.Inconclusive);
        }
    }
}
