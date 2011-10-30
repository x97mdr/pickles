using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Text.RegularExpressions;

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

    public class Results
    {
        private readonly Dictionary<string, TestResult> featureResults;
        private readonly Dictionary<string, TestResult> scenarioResults;
        private readonly Dictionary<string, List<ExampleRowResult>> exampleResults;

        public Results()
        {
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
                this.featureResults.Add(featureElement.Attribute("description").Value, new TestResult
                {
                    IsSuccessful = featureElement.Attribute("success").Value.ToLowerInvariant() == "true",
                    WasExecuted = featureElement.Attribute("executed").Value.ToLowerInvariant() == "true"
                });

                var scenarioElements = featureElement.Element("results").Elements("test-case");
                foreach (var scenarioElement in scenarioElements)
                {
                    this.scenarioResults.Add(scenarioElement.Attribute("description").Value, new TestResult
                    {
                        IsSuccessful = scenarioElement.Attribute("success").Value.ToLowerInvariant() == "true",
                        WasExecuted = scenarioElement.Attribute("executed").Value.ToLowerInvariant() == "true"
                    });
                }

                var scenarioOutlineElements = featureElement.Element("results").Elements("test-suite");
                foreach (var scenarioOutlineElement in scenarioOutlineElements)
                {
                    var examples = scenarioOutlineElement.Element("results").Elements("test-case");
                    var exampleResults = new List<ExampleRowResult>();
                    foreach (var example in examples)
                    {
                        exampleResults.Add(new ExampleRowResult
                        {
                            RowValues = ExtractRowValuesFromName(example.Attribute("name").Value),
                            IsSuccessful = scenarioOutlineElement.Attribute("success").Value.ToLowerInvariant() == "true",
                            WasExecuted = scenarioOutlineElement.Attribute("executed").Value.ToLowerInvariant() == "true"
                        });
                    }
                    this.exampleResults.Add(scenarioOutlineElement.Attribute("description").Value, exampleResults);
                }

            }
        }

        public TestResult GetFeatureResult(string name)
        {
            TestResult testResult;
            if (this.featureResults.TryGetValue(name, out testResult))
            {
                return testResult;
            }

            throw new InvalidOperationException("Cannot retrieve test results for feature named " + name);
        }

        public TestResult GetScenarioResult(string name)
        {
            TestResult testResult;
            if (this.scenarioResults.TryGetValue(name, out testResult))
            {
                return testResult;
            }

            throw new InvalidOperationException("Cannot retrieve test results for scenario named " + name);
        }

        public TestResult GetExampleResult(string name, string[] row)
        {
            List<ExampleRowResult> exampleRowResults;
            if (this.exampleResults.TryGetValue(name, out exampleRowResults))
            {
                var result = exampleRowResults.Single(x => IsRowMatched(row, x.RowValues));
                return new TestResult
                {
                    IsSuccessful = result.IsSuccessful,
                    WasExecuted = result.WasExecuted
                };
            }

            throw new InvalidOperationException("Cannot retrieve test results for scenario named " + name);
        }
    }
}
