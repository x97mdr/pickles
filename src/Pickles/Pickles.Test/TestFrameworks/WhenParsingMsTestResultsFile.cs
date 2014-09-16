using System;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

using NFluent;

namespace PicklesDoc.Pickles.Test.TestFrameworks
{
    [TestFixture]
    public class WhenParsingMsTestResultsFile : WhenParsingTestResultFiles<MsTestResults>
    {
        public WhenParsingMsTestResultsFile()
            : base("results-example-mstest.trx")
        {
        }

        [Test]
        public void ThenCanReadBackgroundResultSuccessfully()
        {
            var background = new Scenario {Name = "Background", Feature = this.AdditionFeature()};
            var feature = this.AdditionFeature();
            feature.AddBackground(background);
            var results = ParseResultsFile();

            TestResult result = results.GetScenarioResult(background);

            Check.That(result.WasExecuted).IsFalse();
            Check.That(result.WasSuccessful).IsFalse();
        }

        [Test]
        public void ThenCanReadInconclusiveFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();
            
            TestResult result = results.GetFeatureResult(this.InconclusiveFeature());

            Assert.AreEqual(TestResult.Inconclusive, result);
        }

        [Test]
        public void ThenCanReadFailedFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.FailingFeature());

            Assert.AreEqual(TestResult.Failed, result);
        }


        [Test]
        public void ThenCanReadPassedFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.PassingFeature());

            Assert.AreEqual(TestResult.Passed, result);
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            var results = ParseResultsFile();
            var scenarioOutline = new ScenarioOutline {Name = "Adding several numbers", Feature = this.AdditionFeature()};
            
            TestResult result = results.GetScenarioOutlineResult(scenarioOutline);

            Check.That(result.WasExecuted).IsTrue();
            Check.That(result.WasSuccessful).IsTrue();
        }

        [Test]
        public void ThenCanReadSuccessfulScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var passedScenario = new Scenario { Name = "Add two numbers", Feature = this.AdditionFeature() };

            TestResult result = results.GetScenarioResult(passedScenario);

            Check.That(result.WasExecuted).IsTrue();
            Check.That(result.WasSuccessful).IsTrue();
        }

        [Test]
        public void ThenCanReadFailedScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var scenario = new Scenario { Name = "Fail to add two numbers", Feature = this.AdditionFeature() };
            TestResult result = results.GetScenarioResult(scenario);

            Check.That(result.WasExecuted).IsTrue();
            Check.That(result.WasSuccessful).IsFalse();
        }

        [Test]
        public void ThenCanReadIgnoredScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var ignoredScenario = new Scenario { Name = "Ignored adding two numbers", Feature = this.AdditionFeature() };
            
            var result = results.GetScenarioResult(ignoredScenario);

            Check.That(result.WasExecuted).IsFalse();
            Check.That(result.WasSuccessful).IsFalse();
        }

        [Test]
        public void ThenCanReadInconclusiveScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            
            var inconclusiveScenario = new Scenario
            {
                Name = "Not automated adding two numbers",
                Feature = this.AdditionFeature()
            };

            var result = results.GetScenarioResult(inconclusiveScenario);

            Check.That(result.WasExecuted).IsFalse();
            Check.That(result.WasSuccessful).IsFalse();
        }

        private Feature AdditionFeature()
        {
            return new Feature { Name = "Addition" };
        }

        private Feature InconclusiveFeature()
        {
            return new Feature { Name = "Inconclusive" };
        }
        private Feature FailingFeature()
        {
            return new Feature { Name = "Failing" };
        }

        private Feature PassingFeature()
        {
            return new Feature { Name = "Passing" };
        }
    }
}