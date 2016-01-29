//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MsTestSingleResults.cs" company="PicklesDoc">
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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.MsTest
{
    public class MsTestSingleResults : SingleTestRunBase
    {
        private const string Failed = "failed";

        private static readonly XNamespace Ns = @"http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
        private readonly XDocument resultsDocument;

        public MsTestSingleResults(XDocument resultsDocument)
        {
            this.resultsDocument = resultsDocument;
        }

        public override bool SupportsExampleResults
        {
            get { return false; }
        }

        private Guid GetScenarioExecutionId(Scenario queriedScenario)
        {
            var idString =
                (from scenario in this.AllScenariosInResultFile()
                    let properties = PropertiesOf(scenario)
                    where properties != null
                    where FeatureNamePropertyExistsWith(queriedScenario.Feature.Name, among: properties)
                    where NameOf(scenario) == queriedScenario.Name
                    select ScenarioExecutionIdStringOf(scenario)).FirstOrDefault();

            return !string.IsNullOrEmpty(idString) ? new Guid(idString) : Guid.Empty;
        }

        private TestResult GetExecutionResult(Guid scenarioExecutionId)
        {
            var resultText =
                (from scenarioResult in this.AllScenarioExecutionResultsInResultFile()
                    let executionId = ResultExecutionIdOf(scenarioResult)
                    where scenarioExecutionId == executionId
                    let outcome = ResultOutcomeOf(scenarioResult)
                    select outcome).FirstOrDefault() ?? string.Empty;

            switch (resultText.ToLowerInvariant())
            {
                case "passed":
                    return TestResult.Passed;
                case Failed:
                    return TestResult.Failed;
                default:
                    return TestResult.Inconclusive;
            }
        }

        private static string ResultOutcomeOf(XElement scenarioResult)
        {
            var outcomeAttribute = scenarioResult.Attribute("outcome");
            return outcomeAttribute != null ? outcomeAttribute.Value : Failed;
        }

        private static Guid ResultExecutionIdOf(XElement unitTestResult)
        {
            var executionIdAttribute = unitTestResult.Attribute("executionId");
            return executionIdAttribute != null ? new Guid(executionIdAttribute.Value) : Guid.Empty;
        }

        public override TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues)
        {
            throw new NotSupportedException();
        }

        public override TestResult GetFeatureResult(Feature feature)
        {
            var featureExecutionIds =
                from scenario in this.AllScenariosInResultFile()
                let properties = PropertiesOf(scenario)
                where properties != null
                where FeatureNamePropertyExistsWith(feature.Name, among: properties)
                select ScenarioExecutionIdOf(scenario);

            TestResult result = featureExecutionIds.Select(this.GetExecutionResult).Merge();

            return result;
        }

        public override TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            var queriedFeatureName = scenarioOutline.Feature.Name;
            var queriedScenarioOutlineName = scenarioOutline.Name;

            var allScenariosForAFeature =
                from scenario in this.AllScenariosInResultFile()
                let scenarioProperties = PropertiesOf(scenario)
                where scenarioProperties != null
                where FeatureNamePropertyExistsWith(queriedFeatureName, among: scenarioProperties)
                select scenario;

            var scenarioOutlineExecutionIds = from scenario in allScenariosForAFeature
                where NameOf(scenario).StartsWith(queriedScenarioOutlineName)
                select ScenarioExecutionIdOf(scenario);

            TestResult result = scenarioOutlineExecutionIds.Select(this.GetExecutionResult).Merge();

            return result;
        }

        public override TestResult GetScenarioResult(Scenario scenario)
        {
            Guid scenarioExecutionId = this.GetScenarioExecutionId(scenario);
            return this.GetExecutionResult(scenarioExecutionId);
        }

        private static Guid ScenarioExecutionIdOf(XElement scenario)
        {
            return new Guid(ScenarioExecutionIdStringOf(scenario));
        }

        private static string ScenarioExecutionIdStringOf(XElement scenario)
        {
            return scenario.Element(Ns + "Execution").Attribute("id").Value;
        }

        private static string NameOf(XElement scenario)
        {
            return scenario.Element(Ns + "Description").Value;
        }

        private static XElement PropertiesOf(XElement scenariosReportes)
        {
            return scenariosReportes.Element(Ns + "Properties");
        }

        private static bool FeatureNamePropertyExistsWith(string featureName, XElement among)
        {
            var properties = among;
            return (from property in properties.Elements(Ns + "Property")
                let key = property.Element(Ns + "Key")
                let value = property.Element(Ns + "Value")
                where key.Value == "FeatureTitle" && value.Value == featureName
                select property).Any();
        }

        private IEnumerable<XElement> AllScenariosInResultFile()
        {
            // Feature scenarios unit-tests should have Description. It is a scenario name
            return
                this.resultsDocument.Root.Descendants(Ns + "UnitTest").Where(s => s.Element(Ns + "Description") != null);
        }

        private IEnumerable<XElement> AllScenarioExecutionResultsInResultFile()
        {
            return this.resultsDocument.Root.Descendants(Ns + "UnitTestResult");
        }
    }
}
