using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.TestFrameworks;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenParsingMsTestResultsFile : BaseFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this._feature = new Feature {Name = "Addition"};

            // Write out the embedded test results file
            using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFile = new FileInfo(RESULTS_FILE_NAME);

            this._results = Container.Resolve<MsTestResults>();
        }

        #endregion

        private const string RESULTS_FILE_NAME = "results-example-mstest.trx";
        private Feature _feature;
        private MsTestResults _results;

        [Test]
        public void ThenCanReadBackgroundResultSuccessfully()
        {
            var background = new Scenario {Name = "Background", Feature = this._feature};
            this._feature.AddBackground(background);

            TestResult result = this._results.GetScenarioResult(background);

            result.WasExecuted.ShouldBeFalse();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadFeatureResultSuccessfully()
        {
            TestResult result = this._results.GetFeatureResult(this._feature);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            var scenarioOutline = new ScenarioOutline {Name = "Adding several numbers", Feature = this._feature};
            TestResult result = this._results.GetScenarioOutlineResult(scenarioOutline);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeTrue();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            var scenario1 = new Scenario {Name = "Add two numbers", Feature = this._feature};
            TestResult result1 = this._results.GetScenarioResult(scenario1);

            result1.WasExecuted.ShouldBeTrue();
            result1.WasSuccessful.ShouldBeTrue();

            var scenario2 = new Scenario {Name = "Fail to add two numbers", Feature = this._feature};
            TestResult result2 = this._results.GetScenarioResult(scenario2);

            result2.WasExecuted.ShouldBeTrue();
            result2.WasSuccessful.ShouldBeFalse();
        }
    }
}