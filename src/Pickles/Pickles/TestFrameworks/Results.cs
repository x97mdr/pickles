using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Pickles.Parser;

namespace Pickles.TestFrameworks
{
    public struct TestResult
    {
        public bool WasExecuted;
        public bool IsSuccessful;
    }

    internal struct ElementAttributes
    {
        internal string Name;
        internal string Description;
        internal bool WasExecuted;
        internal bool IsSuccessful;
    }

    public class Results
    {
        private readonly Configuration configuration;
        private readonly Lazy<XDocument> resultsDocument;

        public Results(Configuration configuration)
        {
            this.configuration = configuration;
            this.resultsDocument = new Lazy<XDocument>(() => 
                {
                    XDocument document;
                    using (var stream = this.configuration.LinkedTestFrameworkResultsFile.OpenRead())
                    {
                        document = XDocument.Load(stream);
                        stream.Close();
                    }
                    return document;
                }, true);
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
            return this.resultsDocument.Value
                       .Descendants("test-suite")
                       .Where(x => x.Attribute("description") != null)
                       .FirstOrDefault(x => x.Attribute("description").Value == feature.Name);
        }

        private TestResult GetResultFromElement(XElement element)
        {
            bool wasExecuted = element.Attribute("executed") != null ? element.Attribute("executed").Value.ToLowerInvariant() == "true" : false;
            bool wasSuccessful = element.Attribute("success") != null ? element.Attribute("success").Value.ToLowerInvariant() == "true" : false;
            return new TestResult { WasExecuted = wasExecuted, IsSuccessful = wasSuccessful };
        }

        public TestResult GetFeatureResult(Feature feature)
        {
            var featureElement = GetFeatureElement(feature);
            return GetResultFromElement(featureElement);
        }

        public TestResult GetScenarioResult(Scenario scenario)
        {
            var featureElement = GetFeatureElement(scenario.Feature);
            var scenarioElement = featureElement
                                      .Descendants("test-case")
                                      .Where(x => x.Attribute("description") != null)
                                      .FirstOrDefault(x => x.Attribute("description").Value == scenario.Name);

            return GetResultFromElement(scenarioElement);
        }

        public TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            var scenarioOutlineElement = GetFeatureElement(scenarioOutline.Feature)
                                             .Descendants("test-suite")
                                             .Where(x => x.Attribute("description") != null)
                                             .FirstOrDefault(x => x.Attribute("description").Value == scenarioOutline.Name);

            return GetResultFromElement(scenarioOutlineElement);
        }

        public TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] row)
        {
            var examplesElement = GetFeatureElement(scenarioOutline.Feature)
                                      .Descendants("test-suite")
                                      .Where(x => x.Attribute("description") != null)
                                      .FirstOrDefault(x => x.Attribute("description").Value == scenarioOutline.Name)
                                          .Descendants("test-case")
                                          .Where(x => x.Attribute("description") != null)
                                          .FirstOrDefault(x => IsRowMatched(ExtractRowValuesFromName(x.Attribute("description").Value), row));

            return GetResultFromElement(examplesElement);
        }
    }
}
