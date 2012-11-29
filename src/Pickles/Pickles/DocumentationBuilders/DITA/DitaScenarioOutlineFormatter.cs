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
using System.Xml.Linq;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.DocumentationBuilders.DITA
{
    public class DitaScenarioOutlineFormatter
    {
        private readonly Configuration configuration;
        private readonly DitaStepFormatter ditaStepFormatter;
        private readonly DitaTableFormatter ditaTableFormatter;
        private readonly ITestResults nunitResults;

        public DitaScenarioOutlineFormatter(Configuration configuration, ITestResults nunitResults,
                                            DitaStepFormatter ditaStepFormatter, DitaTableFormatter ditaTableFormatter)
        {
            this.configuration = configuration;
            this.nunitResults = nunitResults;
            this.ditaStepFormatter = ditaStepFormatter;
            this.ditaTableFormatter = ditaTableFormatter;
        }

        public void Format(XElement parentElement, ScenarioOutline scenario)
        {
            var section = new XElement("section",
                                       new XElement("title", scenario.Name));

            if (!string.IsNullOrEmpty(scenario.Description))
            {
                section.Add(new XElement("p", scenario.Description));
            }

            if (this.configuration.HasTestResults)
            {
                TestResult testResult = this.nunitResults.GetScenarioOutlineResult(scenario);
                if (testResult.WasExecuted && testResult.WasSuccessful)
                {
                    section.Add(new XElement("note", "This scenario passed"));
                }
                else if (testResult.WasExecuted && !testResult.WasSuccessful)
                {
                    section.Add(new XElement("note", "This scenario failed"));
                }
            }

            foreach (Step step in scenario.Steps)
            {
                this.ditaStepFormatter.Format(section, step);
            }

            parentElement.Add(section);
        }
    }
}