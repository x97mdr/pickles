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
        public bool IsSuccessful;
        public bool WasExecuted;
    }

    public struct ExampleRowResult
    {
        public string[] RowValues;
        public bool IsSuccessful;
        public bool WasExecuted;
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
        private readonly Dictionary<string, TestResult> featureResults;
        private readonly Dictionary<string, TestResult> scenarioResults;
        private readonly Dictionary<string, List<ExampleRowResult>> exampleResults;
        private readonly ResultsKeyGenerator resultsKeyGenerator;

        public Results(ResultsKeyGenerator resultsKeyGenerator)
        {
            this.resultsKeyGenerator = resultsKeyGenerator;
            this.featureResults = new Dictionary<string, TestResult>();
            this.scenarioResults = new Dictionary<string, TestResult>();
            this.exampleResults = new Dictionary<string, List<ExampleRowResult>>();
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

        private ElementAttributes CaptureElementAttributes(XElement element)
        {
            return new ElementAttributes
            {
                Name = (element.Attribute("name") != null) ? element.Attribute("name").Value : string.Empty,
                Description = (element.Attribute("description") != null) ? element.Attribute("description").Value : string.Empty,
                WasExecuted = (element.Attribute("executed") != null) ? element.Attribute("executed").Value.ToLowerInvariant() == "true" : false,
                IsSuccessful = (element.Attribute("success") != null) ? element.Attribute("executed").Value.ToLowerInvariant() == "true" : false
            };
        }

        public void Load(string filePath)
        {
            Load(new FileInfo(filePath));
        }

        public void Load(FileInfo file)
        {
            using (var stream = file.OpenRead())
            {
                Load(stream);
            }
        }

        public void Load(Stream stream)
        {
            var resultsDocument = XDocument.Load(stream);

            var featureElements = from element in resultsDocument.Root.Descendants()
                                  where element.Name == ("test-suite")
                                     && element.Attribute("description") != null
                                     && !element.Element("results").Elements().All(resultChild => resultChild.Name.LocalName == "test-case")
                                  select element;

            foreach (var featureElement in featureElements)
            {
                ElementAttributes featureAttributes = CaptureElementAttributes(featureElement);
                this.featureResults.Add(this.resultsKeyGenerator.GetFeatureKey(featureAttributes.Description), new TestResult
                {
                    WasExecuted = featureAttributes.WasExecuted,
                    IsSuccessful = featureAttributes.IsSuccessful
                });

                var scenarioElements = featureElement.Element("results").Elements("test-case");
                foreach (var scenarioElement in scenarioElements)
                {
                    ElementAttributes scenarioAttributes = CaptureElementAttributes(scenarioElement);
                    this.scenarioResults.Add(this.resultsKeyGenerator.GetScenarioKey(featureAttributes.Description, scenarioAttributes.Description), new TestResult
                    {
                        WasExecuted = scenarioAttributes.WasExecuted,
                        IsSuccessful = scenarioAttributes.IsSuccessful
                    });
                }

                var scenarioOutlineElements = featureElement.Element("results").Elements("test-suite");
                foreach (var scenarioOutlineElement in scenarioOutlineElements)
                {
                    ElementAttributes scenarioOutlineAttributes = CaptureElementAttributes(scenarioOutlineElement);

                    var exampleElements = scenarioOutlineElement.Element("results").Elements("test-case");
                    var exampleResults = new List<ExampleRowResult>();
                    foreach (var exampleElement in exampleElements)
                    {
                        ElementAttributes exampleAttributes = CaptureElementAttributes(exampleElement);
                        exampleResults.Add(new ExampleRowResult
                        {
                            WasExecuted = exampleAttributes.WasExecuted,
                            IsSuccessful = exampleAttributes.IsSuccessful,
                            RowValues = ExtractRowValuesFromName(exampleAttributes.Name)
                        });
                    }
                    this.exampleResults.Add(this.resultsKeyGenerator.GetScenarioOutlineKey(featureAttributes.Description, scenarioOutlineAttributes.Description), exampleResults);
                }
            }
        }

        public TestResult GetFeatureResult(string name)
        {
            TestResult testResult;
            testResult.WasExecuted = false;
            if (this.featureResults.TryGetValue(name, out testResult))
            {
                return testResult;
            }

            return testResult;
        }

        public TestResult GetScenarioResult(Scenario scenario)
        {
            TestResult testResult;
            testResult.WasExecuted = false;

            if (this.scenarioResults.TryGetValue(this.resultsKeyGenerator.GetScenarioKey(scenario), out testResult))
            {
                return testResult;
            }

            return testResult;
        }

        public TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] row)
        {
            List<ExampleRowResult> exampleRowResults;
            if (this.exampleResults.TryGetValue(this.resultsKeyGenerator.GetScenarioOutlineKey(scenarioOutline), out exampleRowResults))
            {
                var result = exampleRowResults.Single(x => IsRowMatched(row, x.RowValues));
                return new TestResult
                {
                    IsSuccessful = result.IsSuccessful,
                    WasExecuted = result.WasExecuted
                };
            }

            return new TestResult { WasExecuted = false };
        }
    }
}
