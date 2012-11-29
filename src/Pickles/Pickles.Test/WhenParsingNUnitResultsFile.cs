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
    public class WhenParsingNUnitResultsFile : BaseFixture
    {
        private const string RESULTS_FILE_NAME = "results-example-nunit.xml";

        [Test]
        public void ThenCanReadFeatureResultSuccessfully()
        {
            // Write out the embedded test results file
          using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFile = new FileInfo(RESULTS_FILE_NAME);

            var results = Container.Resolve<NUnitResults>();

            var feature = new Feature {Name = "Addition"};
            TestResult result = results.GetFeatureResult(feature);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            // Write out the embedded test results file
          using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFile = new FileInfo(RESULTS_FILE_NAME);

            var results = Container.Resolve<NUnitResults>();

            var feature = new Feature {Name = "Addition"};

            var scenarioOutline = new ScenarioOutline {Name = "Adding several numbers", Feature = feature};
            TestResult result = results.GetScenarioOutlineResult(scenarioOutline);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeTrue();

            TestResult exampleResult1 = results.GetExampleResult(scenarioOutline, new[] {"40", "50", "90"});
            exampleResult1.WasExecuted.ShouldBeTrue();
            exampleResult1.WasSuccessful.ShouldBeTrue();

            TestResult exampleResult2 = results.GetExampleResult(scenarioOutline, new[] {"60", "70", "130"});
            exampleResult2.WasExecuted.ShouldBeTrue();
            exampleResult2.WasSuccessful.ShouldBeTrue();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            // Write out the embedded test results file
          using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFile = new FileInfo(RESULTS_FILE_NAME);

            var results = Container.Resolve<NUnitResults>();

            var feature = new Feature {Name = "Addition"};

            var scenario1 = new Scenario {Name = "Add two numbers", Feature = feature};
            TestResult result1 = results.GetScenarioResult(scenario1);

            result1.WasExecuted.ShouldBeTrue();
            result1.WasSuccessful.ShouldBeTrue();

            var scenario2 = new Scenario {Name = "Fail to add two numbers", Feature = feature};
            TestResult result2 = results.GetScenarioResult(scenario2);

            result2.WasExecuted.ShouldBeTrue();
            result2.WasSuccessful.ShouldBeFalse();
        }
    }
}