#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.TestFrameworks
{
    public class NUnitResults : ITestResults
    {
        private readonly Configuration configuration;
        private readonly NUnitExampleSignatureBuilder exampleSignatureBuilder;
        private readonly XDocument resultsDocument;

        public NUnitResults(Configuration configuration, NUnitExampleSignatureBuilder exampleSignatureBuilder)
        {
            this.configuration = configuration;
            this.exampleSignatureBuilder = exampleSignatureBuilder;
            if (configuration.HasTestResults)
            {
                this.resultsDocument = ReadResultsFile();
            }
        }

        private XDocument ReadResultsFile()
        {
            XDocument document;
            using (var stream = configuration.TestResultsFile.OpenRead())
            {
                var xmlReader = XmlReader.Create(stream);
                document = XDocument.Load(xmlReader);
                stream.Close();
            }
            return document;
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
                var exampleSignature = this.exampleSignatureBuilder.Build(scenarioOutline, row);
                examplesElement = featureElement
                                      .Descendants("test-suite")
                                      .Where(x => x.Attribute("description") != null)
                                      .FirstOrDefault(x => x.Attribute("description").Value == scenarioOutline.Name)
                                          .Descendants("test-case")
                                          .Where(x => x.Attribute("name") != null)
                                          .FirstOrDefault(x => exampleSignature.IsMatch(x.Attribute("name").Value.ToLowerInvariant()));
            }
            return GetResultFromElement(examplesElement);
        }
    }
}
