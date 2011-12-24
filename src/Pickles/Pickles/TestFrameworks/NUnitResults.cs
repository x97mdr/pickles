using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.TestFrameworks
{
    public struct TestResult
    {
        public bool WasExecuted;
        public bool WasSuccessful;
    }

    public class NUnitResults
    {
        private readonly Configuration configuration;
        private readonly XDocument resultsDocument;

        public NUnitResults(Configuration configuration)
        {
            this.configuration = configuration;
            if (configuration.HasTestFrameworkResults)
            {
                resultsDocument = ReadResultsFile();
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

        private string[] ExtractRowValuesFromName(string exampleName)
        {
            var parts = exampleName.Split(new char[] { '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var values = new List<string>();
            values.AddRange(parts.Skip(1).Where(x => x != "System.String[]").Select(x => x.Replace("\"", "")));
            return values.ToArray();
        }

        private bool IsRowMatched(string[] a, string[] b)
        {
            if (a.Length != b.Length) return false;
            for (int index = 0; index < a.Length; index++)
            {
                if (a[index] != b[index]) return false;
            }
            return true;
        }

        private XElement GetFeatureElement(Feature feature)
        {
            return resultsDocument
                       .Descendants("test-suite")
                       .Where(x => x.Attribute("description") != null)
                       .FirstOrDefault(x => x.Attribute("description").Value == feature.Name);
        }

        private TestResult GetResultFromElement(XElement element)
        {
            if (element == null) return new TestResult { WasExecuted = false, WasSuccessful = false };
            var wasExecuted = element.Attribute("executed") != null ? element.Attribute("executed").Value.ToLowerInvariant() == "true" : false;
            var wasSuccessful = element.Attribute("success") != null ? element.Attribute("success").Value.ToLowerInvariant() == "true" : false;
            return new TestResult { WasExecuted = wasExecuted, WasSuccessful = wasSuccessful };
        }

        public TestResult GetFeatureResult(Feature feature)
        {
            var featureElement = GetFeatureElement(feature);
            return GetResultFromElement(featureElement);
        }

        public TestResult GetScenarioResult(Scenario scenario)
        {
            var featureElement = GetFeatureElement(scenario.Feature);
            XElement scenarioElement = null;
            if (featureElement != null)
            {
                scenarioElement = featureElement
                                      .Descendants("test-case")
                                      .Where(x => x.Attribute("description") != null)
                                      .FirstOrDefault(x => x.Attribute("description").Value == scenario.Name);
            }
            return GetResultFromElement(scenarioElement);
        }

        public TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            var featureElement = GetFeatureElement(scenarioOutline.Feature);
            XElement scenarioOutlineElement = null;
            if (featureElement != null)
            {
                scenarioOutlineElement = GetFeatureElement(scenarioOutline.Feature)
                                             .Descendants("test-suite")
                                             .Where(x => x.Attribute("description") != null)
                                             .FirstOrDefault(x => x.Attribute("description").Value == scenarioOutline.Name);
            }
            return GetResultFromElement(scenarioOutlineElement);
        }

        public TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] row)
        {
            var featureElement = GetFeatureElement(scenarioOutline.Feature);
            XElement examplesElement = null;
            if (featureElement != null)
            {
                examplesElement = GetFeatureElement(scenarioOutline.Feature)
                                      .Descendants("test-suite")
                                      .Where(x => x.Attribute("description") != null)
                                      .FirstOrDefault(x => x.Attribute("description").Value == scenarioOutline.Name)
                                          .Descendants("test-case")
                                          .Where(x => x.Attribute("description") != null)
                                          .FirstOrDefault(x => IsRowMatched(ExtractRowValuesFromName(x.Attribute("description").Value), row));
            }
            return GetResultFromElement(examplesElement);
        }
    }
}
