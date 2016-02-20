//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="CucumberJsonSingleResults.cs" company="PicklesDoc">
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
using System.Diagnostics;
using System.Linq;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.CucumberJson
{
    public class CucumberJsonSingleResults : SingleTestRunBase
    {
        private readonly List<Feature> resultsDocument;

        public CucumberJsonSingleResults(IEnumerable<Feature> cucumberFeatures)
        {
            this.resultsDocument = cucumberFeatures.ToList();
        }

        public override bool SupportsExampleResults
        {
            get
            {
                return false;
            }
        }

        public override TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues)
        {
            throw new NotSupportedException();
        }

        public override TestResult GetFeatureResult(ObjectModel.Feature feature)
        {
            var cucumberFeature = this.GetCucumberFeature(feature);

            return this.GetResultFromFeature(cucumberFeature);
        }

        private Feature GetCucumberFeature(ObjectModel.Feature feature)
        {
            return this.resultsDocument.FirstOrDefault(f => f.name == feature.Name);
        }

        private TestResult GetResultFromFeature(Feature cucumberFeature)
        {
            if (cucumberFeature?.elements == null)
            {
                return TestResult.Inconclusive;
            }

            return ToTestResult(cucumberFeature);
        }

        private TestResult ToTestResult(Feature feature)
        {
            return feature.elements.Select(ToTestResult).Merge();
        }

        private TestResult ToTestResult(Element scenario)
        {
            return scenario.steps.Select(ToTestResult).Merge();
        }

        private TestResult ToTestResult(Step step)
        {
            return ToTestResult(step.result.status);
        }

        private TestResult ToTestResult(string cucumberResult)
        {
            switch (cucumberResult)
            {
                default:
                case "skipped":
                case "undefined":
                case "pending":
                    {
                        return TestResult.Inconclusive;
                    }

                case "failed":
                    {
                        return TestResult.Failed;
                    }

                case "passed":
                    {
                        return TestResult.Passed;
                    }
            }
        }

        public override TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            // Not applicable
            return TestResult.Inconclusive;
        }

        public override TestResult GetScenarioResult(Scenario scenario)
        {
            var cucumberScenario = this.GetCucumberScenario(scenario);

            return this.GetResultFromScenario(cucumberScenario);
        }

        private Element GetCucumberScenario(Scenario scenario)
        {
            Element cucumberScenario = null;
            var cucumberFeature = this.GetCucumberFeature(scenario.Feature);
            if (cucumberFeature != null)
            {
                cucumberScenario = cucumberFeature.elements.FirstOrDefault(x => x.name == scenario.Name);
            }

            return cucumberScenario;
        }

        private TestResult GetResultFromScenario(Element cucumberScenario)
        {
            if (cucumberScenario == null)
            {
                return TestResult.Inconclusive;
            }


            return ToTestResult(cucumberScenario);
        }
    }
}
