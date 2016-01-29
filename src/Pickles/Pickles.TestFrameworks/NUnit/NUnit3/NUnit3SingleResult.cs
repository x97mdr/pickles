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

namespace PicklesDoc.Pickles.TestFrameworks.NUnit.NUnit3
{
    public class NUnit3SingleResult : SingleTestRunBase
    {
        private readonly XDocument resultsDocument;

        public NUnit3SingleResult(XDocument resultsDocument)
        {
            this.resultsDocument = resultsDocument;
        }

        public override bool SupportsExampleResults
        {
            get { return true; }
        }

        public override TestResult GetFeatureResult(Feature feature)
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

        public override TestResult GetScenarioResult(Scenario scenario)
        {
            var scenarioElement = this.GetScenarioElement(scenario);

            return this.GetResultFromElement(scenarioElement);
        }

        public override TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            var scenarioOutlineElement = this.GetScenarioOutlineElement(scenarioOutline);

            return this.DetermineScenarioOutlineResult(scenarioOutlineElement);
        }

        public override TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] exampleValues)
        {
            Regex exampleSignature = this.CreateSignatureRegex(scenarioOutline, exampleValues);

            var examplesElement = this.GetExamplesElement(scenarioOutline, exampleSignature);

            return this.GetResultFromElement(examplesElement);
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

        private static bool IsMatchingTestCase(XElement x, Regex exampleSignature)
        {
            var name = x.Attribute("name");
            return name != null && exampleSignature.IsMatch(name.Value.ToLowerInvariant().Replace(@"\", string.Empty));
        }

        private XElement GetScenarioElement(Scenario scenario)
        {
            XElement featureElement = this.GetFeatureElement(scenario.Feature);
            XElement scenarioElement = null;
            if (featureElement != null)
            {
                scenarioElement =
                    featureElement.Descendants("test-case")
                        .FirstOrDefault(
                            ts =>
                            ts.Elements("properties")
                                .Elements("property")
                                .Any(
                                    p =>
                                    p.Attribute("name").Value == "Description" && p.Attribute("value").Value == scenario.Name));
            }

            return scenarioElement;
        }

        private TestResult DetermineScenarioOutlineResult(XElement scenarioOutlineElement)
        {
            if (scenarioOutlineElement != null)
            {
                return scenarioOutlineElement.Descendants("test-case").Select(this.GetResultFromElement).Merge();
            }

            return TestResult.Inconclusive;
        }

        private XElement GetScenarioOutlineElement(ScenarioOutline scenarioOutline)
        {
            XElement featureElement = this.GetFeatureElement(scenarioOutline.Feature);
            XElement scenarioOutlineElement = null;
            if (featureElement != null)
            {
                scenarioOutlineElement =
                    this.GetFeatureElement(scenarioOutline.Feature)
                        .Descendants("test-suite")
                        .FirstOrDefault(
                            ts =>
                            ts.Elements("properties")
                                .Elements("property")
                                .Any(
                                    p =>
                                    p.Attribute("name").Value == "Description"
                                    && p.Attribute("value").Value == scenarioOutline.Name));
            }
            return scenarioOutlineElement;
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

        private XElement GetExamplesElement(
            ScenarioOutline scenarioOutline,
            Regex exampleSignature)
        {
            XElement featureElement = this.GetFeatureElement(scenarioOutline.Feature);
            XElement examplesElement = null;
            if (featureElement != null)
            {
                var parameterizedTestElement =
                    featureElement.Descendants("test-suite")
                        .FirstOrDefault(
                            ts =>
                            ts.Elements("properties")
                                .Elements("property")
                                .Any(
                                    p =>
                                    p.Attribute("name").Value == "Description"
                                    && p.Attribute("value").Value == scenarioOutline.Name));

                if (parameterizedTestElement != null)
                {
                    examplesElement =
                        parameterizedTestElement.Descendants("test-case")
                            .FirstOrDefault(x => IsMatchingTestCase(x, exampleSignature));
                }
            }

            return examplesElement;
        }
    }
}
