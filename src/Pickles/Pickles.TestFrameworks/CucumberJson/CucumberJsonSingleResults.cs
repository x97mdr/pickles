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

        public override TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues)
        {
            var cucumberScenarios = this.GetCucumberScenarios(scenario);

            var query = cucumberScenarios.Where(cs => this.ScenarioHasStepsForAllExampleValues(cs.Item1, exampleValues))
                .Select(cs => ToTestResult(cs.Item1, cs.Item2));

            return query.FirstOrDefault();
        }

        private bool ScenarioHasStepsForAllExampleValues(Element cucumberScenario, string[] exampleValues)
        {
            return exampleValues.All(exampleValue => this.ScenarioHasAStepWithThisExampleValue(cucumberScenario, exampleValue));
        }

        private bool ScenarioHasAStepWithThisExampleValue(Element cucumberScenario, string exampleValue)
        {
            return cucumberScenario.steps.Any(step => step.name.Contains(exampleValue));
        }

        public override TestResult GetFeatureResult(ObjectModel.Feature feature)
        {
            var cucumberFeature = this.GetCucumberFeature(feature);

            return this.GetResultFromFeature(cucumberFeature.Item1, cucumberFeature.Item2);
        }

        private Tuple<Feature, Element> GetCucumberFeature(ObjectModel.Feature feature)
        {
            var cucumberFeature = this.resultsDocument.FirstOrDefault(f => f.name == feature.Name);
            var background = cucumberFeature?.elements.FirstOrDefault(e => e.type == "background");
            return new Tuple<Feature, Element>(cucumberFeature, background);
        }

        private TestResult GetResultFromFeature(Feature cucumberFeature, Element background)
        {
            if (cucumberFeature?.elements == null)
            {
                return TestResult.Inconclusive;
            }

            return ToTestResult(cucumberFeature, background);
        }

        private TestResult ToTestResult(Feature feature, Element background)
        {
            return feature.elements.Select(e => ToTestResult(e, background)).Merge();
        }

        private TestResult ToTestResult(Element scenario, Element background)
        {
            var steps = (background?.steps ?? new List<Step>()).Concat(scenario.steps);

            return steps.Select(ToTestResult).Merge();
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
            var cucumberScenarios = this.GetCucumberScenarios(scenarioOutline);

            return cucumberScenarios.Select(cs => ToTestResult(cs.Item1, cs.Item2)).Merge();
        }


        public override TestResult GetScenarioResult(Scenario scenario)
        {
            var cucumberScenario = this.GetCucumberScenario(scenario);

            return this.GetResultFromScenario(cucumberScenario.Item1, cucumberScenario.Item2);
        }

        private Tuple<Element, Element> GetCucumberScenario(Scenario scenario)
        {
            Element cucumberScenario = null;
            var cucumberFeature = this.GetCucumberFeature(scenario.Feature);
            if (cucumberFeature?.Item1 != null)
            {
                cucumberScenario = cucumberFeature.Item1.elements.FirstOrDefault(x => x.name == scenario.Name);
            }

            return new Tuple<Element, Element>(cucumberScenario, cucumberFeature?.Item2);
        }

        private IEnumerable<Tuple<Element, Element>> GetCucumberScenarios(ScenarioOutline scenarioOutline)
        {
            IEnumerable<Element> cucumberScenarios = null;
            var cucumberFeature = this.GetCucumberFeature(scenarioOutline.Feature);
            if (cucumberFeature?.Item1 != null)
            {
                cucumberScenarios = cucumberFeature.Item1.elements.Where(x => x.name == scenarioOutline.Name);
            }

            return (cucumberScenarios ?? new Element[0]).Select(cs => new Tuple<Element, Element>(cs, cucumberFeature?.Item2));
        }

        private TestResult GetResultFromScenario(Element cucumberScenario, Element background)
        {
            if (cucumberScenario == null)
            {
                return TestResult.Inconclusive;
            }


            return ToTestResult(cucumberScenario, background);
        }
    }
}
