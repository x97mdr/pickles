using System;
using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.TestFrameworks;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenParsingSpecRunTestResultsFile : WhenParsingTestResultFiles<SpecRunResults>
    {
        public WhenParsingSpecRunTestResultsFile()
            : base("results-example-specrun.html")
        {
        }

        [Test]
        public void ThenCanReadBackgroundResultSuccessfully()
        {
            var background = new Scenario { Name = "Background", Feature = this.AdditionFeature() };
            var feature = this.AdditionFeature();
            feature.AddBackground(background);
            var results = ParseResultsFile();

            TestResult result = results.GetScenarioResult(background);

            result.ShouldEqual(TestResult.Inconclusive);
        }

        [Test]
        public void ThenCanReadInconclusiveFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.InconclusiveFeature());

            result.ShouldEqual(TestResult.Inconclusive);
        }

        [Test]
        public void ThenCanReadFailedFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.FailingFeature());

            result.ShouldEqual(TestResult.Failed);
        }

        [Test]
        public void ThenCanReadPassedFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.PassingFeature());

            result.ShouldEqual(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            var results = ParseResultsFile();
            var scenarioOutline = new ScenarioOutline { Name = "Adding several numbers", Feature = this.AdditionFeature() };
            
            TestResult result = results.GetScenarioOutlineResult(scenarioOutline);

            result.ShouldEqual(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadSuccessfulScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var passedScenario = new Scenario { Name = "Add two numbers", Feature = this.AdditionFeature() };

            TestResult result = results.GetScenarioResult(passedScenario);

            result.ShouldEqual(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadFailedScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var scenario = new Scenario { Name = "Fail to add two numbers", Feature = this.AdditionFeature() };
            TestResult result = results.GetScenarioResult(scenario);

            result.ShouldEqual(TestResult.Failed);
        }

        [Test]
        public void ThenCanReadIgnoredScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var ignoredScenario = new Scenario { Name = "Ignored adding two numbers", Feature = this.AdditionFeature() };
            
            var result = results.GetScenarioResult(ignoredScenario);

            result.ShouldEqual(TestResult.Inconclusive);
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

            result.ShouldEqual(TestResult.Inconclusive);
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