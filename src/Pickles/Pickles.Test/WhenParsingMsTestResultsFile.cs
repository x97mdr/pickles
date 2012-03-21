using System.IO;
using Ninject;
using NUnit.Framework;
using Pickles.Parser;
using Pickles.TestFrameworks;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenParsingMSTestResultsFile : BaseFixture
    {
        private const string RESULTS_FILE_NAME = "results-example-mstest.trx";
        private Feature _feature;
        private MsTestResults _results;

        [SetUp]
        public void Setup()
        {
            _feature = new Feature { Name = "Addition" };

            // Write out the embedded test results file
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.TestResultsFile = new FileInfo(RESULTS_FILE_NAME);

            _results = Kernel.Get<MsTestResults>();
        }

        [Test]
        public void ThenCanReadFeatureResultSuccessfully()
        {
            var result = _results.GetFeatureResult(_feature);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            var scenario1 = new Scenario { Name = "Add two numbers", Feature = _feature };
            var result1 = _results.GetScenarioResult(scenario1);

            result1.WasExecuted.ShouldBeTrue();
            result1.WasSuccessful.ShouldBeTrue();

            var scenario2 = new Scenario { Name = "Fail to add two numbers", Feature = _feature };
            var result2 = _results.GetScenarioResult(scenario2);

            result2.WasExecuted.ShouldBeTrue();
            result2.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            var scenarioOutline = new ScenarioOutline { Name = "Adding several numbers", Feature = _feature };
            var result = _results.GetScenarioOutlineResult(scenarioOutline);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeTrue();
        }

        [Test]
        public void ThenCanReadBackgroundResultSuccessfully()
        {
            var background = new Scenario { Name = "Background", Feature = _feature };
            _feature.AddBackground(background);

            var result = _results.GetScenarioResult(background);

            result.WasExecuted.ShouldBeFalse();
            result.WasSuccessful.ShouldBeFalse();
        }
    }
}
