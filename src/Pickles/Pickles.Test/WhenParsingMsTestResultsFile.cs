using System.IO;
using System.Reflection;
using NUnit.Framework;
using Ninject;
using Pickles.Parser;
using Pickles.TestFrameworks;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenParsingMSTestResultsFile : BaseFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            _feature = new Feature {Name = "Addition"};

            // Write out the embedded test results file
            using (
                var input =
                    new StreamReader(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.TestResultsFile = new FileInfo(RESULTS_FILE_NAME);

            _results = Kernel.Get<MsTestResults>();
        }

        #endregion

        private const string RESULTS_FILE_NAME = "results-example-mstest.trx";
        private Feature _feature;
        private MsTestResults _results;

        [Test]
        public void ThenCanReadBackgroundResultSuccessfully()
        {
            var background = new Scenario {Name = "Background", Feature = _feature};
            _feature.AddBackground(background);

            TestResult result = _results.GetScenarioResult(background);

            result.WasExecuted.ShouldBeFalse();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadFeatureResultSuccessfully()
        {
            TestResult result = _results.GetFeatureResult(_feature);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            var scenarioOutline = new ScenarioOutline {Name = "Adding several numbers", Feature = _feature};
            TestResult result = _results.GetScenarioOutlineResult(scenarioOutline);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeTrue();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            var scenario1 = new Scenario {Name = "Add two numbers", Feature = _feature};
            TestResult result1 = _results.GetScenarioResult(scenario1);

            result1.WasExecuted.ShouldBeTrue();
            result1.WasSuccessful.ShouldBeTrue();

            var scenario2 = new Scenario {Name = "Fail to add two numbers", Feature = _feature};
            TestResult result2 = _results.GetScenarioResult(scenario2);

            result2.WasExecuted.ShouldBeTrue();
            result2.WasSuccessful.ShouldBeFalse();
        }
    }
}