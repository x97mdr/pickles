using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using Pickles.Parser;
using System.Text.RegularExpressions;

namespace Pickles.TestFrameworks
{
    public class XUnitResults : ITestResults
    {
        private readonly Configuration configuration;
        private readonly xUnitExampleSignatureBuilder exampleSignatureBuilder;
        private readonly XDocument resultsDocument;

        public XUnitResults(Configuration configuration, xUnitExampleSignatureBuilder exampleSignatureBuilder)
        {
            this.configuration = configuration;
            this.exampleSignatureBuilder = exampleSignatureBuilder;
            if (configuration.HasTestFrameworkResults)
            {
                this.resultsDocument = ReadResultsFile();
            }
        }

        private XDocument ReadResultsFile()
        {
            XDocument document;
            using (var stream = configuration.LinkedTestFrameworkResultsFile.OpenRead())
            {
                var xmlReader = XmlReader.Create(stream);
                document = XDocument.Load(xmlReader);
                stream.Close();
            }
            return document;
        }

        private XElement GetFeatureElement(Feature feature)
        {
            var featureQuery =
                  from clazz in this.resultsDocument.Root.Descendants("class")
                  from test in clazz.Descendants("test")
                  from trait in clazz.Descendants("traits").Descendants("trait")
                  where trait.Attribute("name").Value == "FeatureTitle" && trait.Attribute("value").Value == feature.Name
                  select clazz;

            return featureQuery.FirstOrDefault();
        }

        private XElement GetScenarioElement(Scenario scenario)
        {
            var featureElement = GetFeatureElement(scenario.Feature);

            var scenarioQuery =
                from test in featureElement.Descendants("test")
                from trait in test.Descendants("traits").Descendants("trait")
                where trait.Attribute("name").Value == "Description" && trait.Attribute("value").Value == scenario.Name
                select test;

            return scenarioQuery.FirstOrDefault();
        }

        private IEnumerable<XElement> GetScenarioOutlineElements(ScenarioOutline scenario)
        {
            var featureElement = GetFeatureElement(scenario.Feature);

            var scenarioQuery =
                from test in featureElement.Descendants("test")
                from trait in test.Descendants("traits").Descendants("trait")
                where trait.Attribute("name").Value == "Description" && trait.Attribute("value").Value == scenario.Name
                select test;

            return scenarioQuery;
        }

        private TestResult GetResultFromElement(XElement element)
        {
            TestResult result = new TestResult();
            var resultAttribute = element.Attribute("result");
            switch (resultAttribute.Value.ToLowerInvariant())
            {
                case "skip":
                    result.WasExecuted = false;
                    result.WasSuccessful = false;
                    break;
                case "pass":
                    result.WasExecuted = true;
                    result.WasSuccessful = true;
                    break;
                case "fail":
                    result.WasExecuted = true;
                    result.WasSuccessful = false;
                    break;
                default:
                    result.WasExecuted = false;
                    result.WasSuccessful = false;
                    break;
            }
            return result;
        }

        private static TestResult GetAggregateResult(int passedCount, int failedCount, int skippedCount)
        {
            TestResult result;
            if (skippedCount > 0)
            {
                result.WasExecuted = false;
                result.WasSuccessful = false;
            }
            else if (failedCount > 0)
            {
                result.WasExecuted = true;
                result.WasSuccessful = false;
            }
            else
            {
                result.WasExecuted = true;
                result.WasSuccessful = true;
            }

            return result;
        }

        #region ITestResults Members

        public TestResult GetExampleResult(Parser.ScenarioOutline scenarioOutline, string[] row)
        {
            var exampleElements = GetScenarioOutlineElements(scenarioOutline);

            TestResult result = new TestResult();
            foreach (var exampleElement in exampleElements)
            {
                var signature = this.exampleSignatureBuilder.Build(scenarioOutline, row);
                if (signature.IsMatch(exampleElement.Attribute("name").Value.ToLowerInvariant()))
                {
                    return GetResultFromElement(exampleElement);
                }
            }
            return result;
        }

        public TestResult GetFeatureResult(Parser.Feature feature)
        {
            var featureElement = GetFeatureElement(feature);
            int passedCount = int.Parse(featureElement.Attribute("passed").Value);
            int failedCount = int.Parse(featureElement.Attribute("failed").Value);
            int skippedCount = int.Parse(featureElement.Attribute("skipped").Value);

            return GetAggregateResult(passedCount, failedCount, skippedCount);
        }

        public TestResult GetScenarioOutlineResult(Parser.ScenarioOutline scenarioOutline)
        {
            var exampleElements = GetScenarioOutlineElements(scenarioOutline);
            int passedCount = 0;
            int failedCount = 0;
            int skippedCount = 0;

            foreach (var exampleElement in exampleElements)
            {
                var result = GetResultFromElement(exampleElement);
                if (result.WasExecuted == false) skippedCount++;
                if (result.WasExecuted && result.WasSuccessful) passedCount++;
                if (result.WasExecuted && !result.WasSuccessful) failedCount++;
            }

            return GetAggregateResult(passedCount, failedCount, skippedCount);
        }

        public TestResult GetScenarioResult(Parser.Scenario scenario)
        {
            var scenarioElement = GetScenarioElement(scenario);
            return GetResultFromElement(scenarioElement);
        }

        #endregion
    }
}
