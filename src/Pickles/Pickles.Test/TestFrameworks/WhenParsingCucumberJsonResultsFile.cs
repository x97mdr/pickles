using System;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

using NFluent;

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

            Check.That(result.WasExecuted).IsTrue();
            Check.That(result.WasSuccessful).IsFalse();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Test Feature" };

            var scenario1 = new Scenario { Name = "Passing", Feature = feature };
            TestResult result1 = results.GetScenarioResult(scenario1);

            Check.That(result1.WasExecuted).IsTrue();
            Check.That(result1.WasSuccessful).IsTrue();

            var scenario2 = new Scenario { Name = "Failing", Feature = feature };
            TestResult result2 = results.GetScenarioResult(scenario2);

            Check.That(result2.WasExecuted).IsTrue();
            Check.That(result2.WasSuccessful).IsFalse();
        }

        [Test]
        public void ThenCanReadFeatureWithoutScenariosSuccessfully_ShouldReturnInconclusive()
        {
            var results = ParseResultsFile();
            var feature = new Feature { Name = "Feature Without Scenarios" };

            TestResult result = results.GetFeatureResult(feature);

            Check.That(result).IsEqualTo(TestResult.Inconclusive);
        }
    }
}
