using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;
using Pickles.Parser;
using Pickles.TestFrameworks;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenParsingxUnitResultsFile : BaseFixture
    {
        private const string RESULTS_FILE_NAME = "results-example-xunit.xml";

        [Test]
        public void ThenCanReadFeatureResultSuccessfully()
        {
            // Write out the embedded test results file
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.LinkedTestFrameworkResultsFile = new FileInfo(RESULTS_FILE_NAME);

            var results = Kernel.Get<XUnitResults>();

            var feature = new Feature { Name = "Addition" };
            var result = results.GetFeatureResult(feature);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioResultSuccessfully()
        {
            // Write out the embedded test results file
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.LinkedTestFrameworkResultsFile = new FileInfo(RESULTS_FILE_NAME);

            var results = Kernel.Get<XUnitResults>();

            var feature = new Feature { Name = "Addition" };

            var scenario1 = new Scenario { Name = "Add two numbers", Feature = feature };
            var result1 = results.GetScenarioResult(scenario1);

            result1.WasExecuted.ShouldBeTrue();
            result1.WasSuccessful.ShouldBeTrue();

            var scenario2 = new Scenario { Name = "Fail to add two numbers", Feature = feature };
            var result2 = results.GetScenarioResult(scenario2);

            result2.WasExecuted.ShouldBeTrue();
            result2.WasSuccessful.ShouldBeFalse();
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            // Write out the embedded test results file
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + RESULTS_FILE_NAME)))
            using (var output = new StreamWriter(RESULTS_FILE_NAME))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.LinkedTestFrameworkResultsFile = new FileInfo(RESULTS_FILE_NAME);

            var results = Kernel.Get<XUnitResults>();

            var feature = new Feature { Name = "Addition" };

            var scenarioOutline = new ScenarioOutline { Name = "Adding several numbers", Feature = feature };
            var result = results.GetScenarioOutlineResult(scenarioOutline);

            result.WasExecuted.ShouldBeTrue();
            result.WasSuccessful.ShouldBeTrue();

            var exampleResult1 = results.GetExampleResult(scenarioOutline, new string[] { "40", "50", "90" });
            exampleResult1.WasExecuted.ShouldBeTrue();
            exampleResult1.WasSuccessful.ShouldBeTrue();

            var exampleResult2 = results.GetExampleResult(scenarioOutline, new string[] { "60", "70", "130" });
            exampleResult2.WasExecuted.ShouldBeTrue();
            exampleResult2.WasSuccessful.ShouldBeTrue();
        }
    }
}
