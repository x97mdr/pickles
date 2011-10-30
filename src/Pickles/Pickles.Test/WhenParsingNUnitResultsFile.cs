using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Pickles.TestFrameworks;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenParsingNUnitResultsFile : BaseFixture
    {
        [Test]
        public void Then_can_parse_simple_result_file_successfully()
        {
            var resultsFile = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test.results-example.xml");
            var results = new Results();
            results.Load(resultsFile);

            Assert.AreEqual(true, results.GetFeatureResult("This Is A Feature").WasExecuted);
            Assert.AreEqual(true, results.GetFeatureResult("This Is A Feature").IsSuccessful);

            Assert.AreEqual(true, results.GetScenarioResult("The First Scenario").WasExecuted);
            Assert.AreEqual(true, results.GetScenarioResult("The First Scenario").IsSuccessful);

            Assert.AreEqual(true, results.GetScenarioResult("The Second Scenario").WasExecuted);
            Assert.AreEqual(true, results.GetScenarioResult("The Second Scenario").IsSuccessful);

            Assert.AreEqual(true, results.GetScenarioResult("The Third Scenario").WasExecuted);
            Assert.AreEqual(true, results.GetScenarioResult("The Third Scenario").IsSuccessful);

            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "THIS", "THAT" }).WasExecuted);
            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "THIS", "THAT" }).IsSuccessful);

            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "THE", "OTHER" }).WasExecuted);
            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "THE", "OTHER" }).IsSuccessful);

            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "THING", "LORUS" }).WasExecuted);
            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "THING", "LORUS" }).IsSuccessful);

            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "IPSUM", "DECOR" }).WasExecuted);
            Assert.AreEqual(true, results.GetExampleResult("A Scenario Outline", new string[] { "IPSUM", "DECOR" }).IsSuccessful);
        }
    }
}
