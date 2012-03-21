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
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.TestFrameworks
{
    public class MsTestResults : ITestResults
    {
        private static readonly XNamespace ns = @"http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
        private readonly Configuration configuration;
        private readonly XDocument resultsDocument;

        public MsTestResults(Configuration configuration)
        {
            this.configuration = configuration;
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

        private Guid GetScenarioExecutionId(Scenario scenario)
        {
            var idString =
                (from unitTest in this.resultsDocument.Root.Descendants(ns + "UnitTest")
                 let properties = unitTest.Element(ns + "Properties")
                 let property = properties.Element(ns + "Property")
                 let key = property.Element(ns + "Key")
                 let value = property.Element(ns + "Value")
                 where key.Value == "FeatureTitle" && value.Value == scenario.Feature.Name
                 let description = unitTest.Element(ns + "Description")
                 where description.Value == scenario.Name
                 let id = unitTest.Element(ns + "Execution").Attribute("id").Value
                 select id).FirstOrDefault();

            return !string.IsNullOrEmpty(idString) ? new Guid(idString) : Guid.Empty;
        }

        private TestResult GetExecutionResult(Guid scenarioExecutionId)
        {
            var resultText =
                (from unitTestResult in this.resultsDocument.Root.Descendants(ns + "UnitTestResult")
                 let executionId = new Guid(unitTestResult.Attribute("executionId").Value)
                 where scenarioExecutionId == executionId
                 let outcome = unitTestResult.Attribute("outcome").Value
                 select outcome).FirstOrDefault() ?? string.Empty;

            switch (resultText.ToLowerInvariant())
            {
                case "passed":
                    return new TestResult { WasExecuted = true, WasSuccessful = true };
                case "failed":
                    return new TestResult { WasExecuted = true, WasSuccessful = false };
                default:
                    return new TestResult { WasExecuted = false, WasSuccessful = false };
            }
        }

        #region ITestResults Members

        public TestResult GetFeatureResult(Parser.Feature feature)
        {
            var featureExecutionIds =
                from unitTest in this.resultsDocument.Root.Descendants(ns + "UnitTest")
                let property = unitTest.Element(ns + "Properties").Element(ns + "Property")
                let key = property.Element(ns + "Key")
                let value = property.Element(ns + "Value")
                where key.Value == "FeatureTitle" && value.Value == "Addition"
                select new Guid(unitTest.Element(ns + "Execution").Attribute("id").Value);

            bool wasExecuted = true;
            bool wasSuccessful = true;
            foreach (var featureExecutionId in featureExecutionIds)
            {
                var result = GetExecutionResult(featureExecutionId);
                if (!result.WasExecuted) wasExecuted = false;
                if (!result.WasSuccessful) wasSuccessful = false;
            }

            return new TestResult { WasExecuted = wasExecuted, WasSuccessful = wasSuccessful };
        }

        public TestResult GetScenarioOutlineResult(Parser.ScenarioOutline scenarioOutline)
        {
            var scenarioOutlineExecutionIds =
                from unitTest in this.resultsDocument.Root.Descendants(ns + "UnitTest")
                let property = unitTest.Element(ns + "Properties").Element(ns + "Property")
                let key = property.Element(ns + "Key")
                let value = property.Element(ns + "Value")
                where key.Value == "FeatureTitle" && value.Value == "Addition"
                let description = unitTest.Element(ns + "Description")
                where description.Value == scenarioOutline.Name
                select new Guid(unitTest.Element(ns + "Execution").Attribute("id").Value);

            bool wasExecuted = true;
            bool wasSuccessful = true;
            foreach (var scenarioOutlineExecutionId in scenarioOutlineExecutionIds)
            {
                var result = GetExecutionResult(scenarioOutlineExecutionId);
                if (!result.WasExecuted) wasExecuted = false;
                if (!result.WasSuccessful) wasSuccessful = false;
            }

            return new TestResult { WasExecuted = wasExecuted, WasSuccessful = wasSuccessful };
        }

        public TestResult GetScenarioResult(Parser.Scenario scenario)
        {
            Guid scenarioExecutionId = GetScenarioExecutionId(scenario);
            return GetExecutionResult(scenarioExecutionId);
        }

        #endregion
    }
}
