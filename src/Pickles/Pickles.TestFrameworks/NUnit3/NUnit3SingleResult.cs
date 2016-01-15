//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NUnit3SingleResult.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.NUnit3
{
    public class NUnit3SingleResult : ITestResults
    {
        private readonly XDocument resultsDocument;

        public NUnit3SingleResult(XDocument resultsDocument)
        {
            this.resultsDocument = resultsDocument;
        }

        public bool SupportsExampleResults
        {
            get { return true; }
        }

        internal NUnit3ExampleSignatureBuilder ExampleSignatureBuilder { get; set; }

        public TestResult GetFeatureResult(Feature feature)
        {
            var featureElement = this.GetFeatureElement(feature);

            if (featureElement == null)
            {
                return TestResult.Inconclusive;
            }

            var results = featureElement.Descendants("test-case")
                .Select(this.GetResultFromElement);

            return results.Merge();
        }

        public TestResult GetScenarioResult(Scenario scenario)
        {
            XElement featureElement = this.GetFeatureElement(scenario.Feature);
            XElement scenarioElement = null;
            if (featureElement != null)
            {
                scenarioElement = featureElement
                    .Descendants("test-case")
                    .FirstOrDefault(
                        ts =>
                        ts.Elements("properties").Elements("property")
                        .Any(p => p.Attribute("name").Value == "Description" && p.Attribute("value").Value == scenario.Name));
            }

            return this.GetResultFromElement(scenarioElement);
        }

        public TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            XElement featureElement = this.GetFeatureElement(scenarioOutline.Feature);
            XElement scenarioOutlineElement = null;
            if (featureElement != null)
            {
                scenarioOutlineElement = this.GetFeatureElement(scenarioOutline.Feature)
                    .Descendants("test-suite")
                    .FirstOrDefault(
                        ts =>
                        ts.Elements("properties").Elements("property")
                        .Any(p => p.Attribute("name").Value == "Description" && p.Attribute("value").Value == scenarioOutline.Name));
            }

            if (scenarioOutlineElement != null)
            {
                return scenarioOutlineElement.Descendants("test-case").Select(this.GetResultFromElement).Merge();
            }

            return this.GetResultFromElement(scenarioOutlineElement);
        }

        private XElement GetFeatureElement(Feature feature)
        {
            return
                this.resultsDocument
                    .Descendants("test-suite")
                    .FirstOrDefault(
                        ts =>
                        ts.Elements("properties").Elements("property")
                        .Any(p => p.Attribute("name").Value == "Description" && p.Attribute("value").Value == feature.Name));
        }

        private TestResult GetResultFromElement(XElement element)
        {
            if (element == null)
            {
                return TestResult.Inconclusive;
            }
            else if (IsAttributeSetToValue(element, "result", "Skipped"))
            {
                return TestResult.Inconclusive;
            }
            else if (IsAttributeSetToValue(element, "result", "Inconclusive"))
            {
                return TestResult.Inconclusive;
            }
            else if (IsAttributeSetToValue(element, "result", "Failed"))
            {
                return TestResult.Failed;
            }
            else if (IsAttributeSetToValue(element, "result", "Passed"))
            {
                return TestResult.Passed;
            }
            else
            {
                bool wasExecuted = IsAttributeSetToValue(element, "executed", "true");

                if (!wasExecuted)
                {
                    return TestResult.Inconclusive;
                }

                bool wasSuccessful = IsAttributeSetToValue(element, "success", "true");

                return wasSuccessful ? TestResult.Passed : TestResult.Failed;
            }
        }

        private static bool IsAttributeSetToValue(XElement element, string attributeName, string expectedValue)
        {
            return element.Attribute(attributeName) != null
                ? string.Equals(
                    element.Attribute(attributeName).Value,
                    expectedValue,
                    StringComparison.InvariantCultureIgnoreCase)
                : false;
        }

        public TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] exampleValues)
        {
            XElement featureElement = this.GetFeatureElement(scenarioOutline.Feature);
            XElement examplesElement = null;
            if (featureElement != null)
            {
                var signatureBuilder = this.ExampleSignatureBuilder;

                if (signatureBuilder == null)
                {
                    throw new InvalidOperationException(
                        "You need to set the ExampleSignatureBuilder before using GetExampleResult.");
                }

                Regex exampleSignature = signatureBuilder.Build(scenarioOutline, exampleValues);

                var parameterizedTestElement = featureElement
                    .Descendants("test-suite")
                    .FirstOrDefault(
                        ts =>
                        ts.Elements("properties").Elements("property")
                        .Any(p => p.Attribute("name").Value == "Description" && p.Attribute("value").Value == scenarioOutline.Name));

                if (parameterizedTestElement != null)
                {
                    examplesElement = parameterizedTestElement.Descendants("test-case")
                        .FirstOrDefault(x => IsMatchingTestCase(x, exampleSignature));
                }
            }

            return this.GetResultFromElement(examplesElement);
        }

        private static bool IsMatchingTestCase(XElement x, Regex exampleSignature)
        {
            var name = x.Attribute("name");
            return name != null && exampleSignature.IsMatch(name.Value.ToLowerInvariant().Replace(@"\", string.Empty));
        }

        private static bool IsMatchingParameterizedTestElement(XElement element, ScenarioOutline scenarioOutline)
        {
            var description = element.Attribute("description");

            return description != null &&
                   description.Value.Equals(scenarioOutline.Name, StringComparison.OrdinalIgnoreCase) &&
                   element.Descendants("test-case").Any();
        }
    }
}
