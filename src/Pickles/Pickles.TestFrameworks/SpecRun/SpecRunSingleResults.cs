//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SpecRunSingleResults.cs" company="PicklesDoc">
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

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.SpecRun
{
    public class SpecRunSingleResults : SingleTestRunBase
    {
        private readonly List<SpecRunFeature> specRunFeatures;

        public SpecRunSingleResults(IEnumerable<SpecRunFeature> specRunFeatures)
        {
            this.specRunFeatures = specRunFeatures.ToList();
        }

        public override bool SupportsExampleResults
        {
            get { return true; }
        }

        public override TestResult GetFeatureResult(Feature feature)
        {
            var specRunFeature = this.FindSpecRunFeature(feature);

            if (specRunFeature == null)
            {
                return TestResult.Inconclusive;
            }

            TestResult result =
                specRunFeature.Scenarios.Select(specRunScenario => StringToTestResult(specRunScenario.Result)).Merge();

            return result;
        }

        public override TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            var specRunFeature = this.FindSpecRunFeature(scenarioOutline.Feature);

            if (specRunFeature == null)
            {
                return TestResult.Inconclusive;
            }

            SpecRunScenario[] specRunScenarios = FindSpecRunScenarios(scenarioOutline, specRunFeature);

            if (specRunScenarios.Length == 0)
            {
                return TestResult.Inconclusive;
            }

            TestResult result = StringsToTestResult(specRunScenarios.Select(srs => srs.Result));

            return result;
        }

        public override TestResult GetScenarioResult(Scenario scenario)
        {
            var specRunFeature = this.FindSpecRunFeature(scenario.Feature);

            if (specRunFeature == null)
            {
                return TestResult.Inconclusive;
            }

            var specRunScenario = FindSpecRunScenario(scenario, specRunFeature);

            if (specRunScenario == null)
            {
                return TestResult.Inconclusive;
            }

            return StringToTestResult(specRunScenario.Result);
        }

        public override TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues)
        {
            throw new NotSupportedException();
        }

        private static TestResult StringsToTestResult(IEnumerable<string> results)
        {
            if (results == null)
            {
                return TestResult.Inconclusive;
            }

            return results.Select(StringToTestResult).Merge();
        }

        private static TestResult StringToTestResult(string result)
        {
            if (result == null)
            {
                return TestResult.Inconclusive;
            }

            switch (result.ToLowerInvariant())
            {
                case "passed":
                {
                    return TestResult.Passed;
                }

                case "failed":
                {
                    return TestResult.Failed;
                }

                default:
                {
                    return TestResult.Inconclusive;
                }
            }
        }

        private static SpecRunScenario[] FindSpecRunScenarios(ScenarioOutline scenarioOutline, SpecRunFeature specRunFeature)
        {
            return specRunFeature.Scenarios.Where(d => d.Title.StartsWith(scenarioOutline.Name + ", ")).ToArray();
        }

        private static SpecRunScenario FindSpecRunScenario(Scenario scenario, SpecRunFeature specRunFeature)
        {
            SpecRunScenario result = specRunFeature.Scenarios.FirstOrDefault(d => d.Title.Equals(scenario.Name));

            return result;
        }

        private SpecRunFeature FindSpecRunFeature(Feature feature)
        {
            return this.specRunFeatures.FirstOrDefault(specRunFeature => specRunFeature.Title == feature.Name);
        }
    }
}
