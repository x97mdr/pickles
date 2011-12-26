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
using System.Text;
using System.Xml.Linq;
using Pickles.Parser;
using Pickles.TestFrameworks;

namespace Pickles.DocumentationBuilders.DITA
{
    public class DitaScenarioFormatter
    {
        private readonly Configuration configuration;
        private readonly NUnitResults nunitResults;
        private readonly DitaStepFormatter ditaStepFormatter;

        public DitaScenarioFormatter(Configuration configuration, NUnitResults nunitResults, DitaStepFormatter ditaStepFormatter)
        {
            this.configuration = configuration;
            this.nunitResults = nunitResults;
            this.ditaStepFormatter = ditaStepFormatter;
        }

        public void Format(XElement parentElement, Scenario scenario)
        {
            var section = new XElement("section",
                              new XElement("title", scenario.Name));

            if (this.configuration.HasTestFrameworkResults)
            {
                var testResult = this.nunitResults.GetScenarioResult(scenario);
                if (testResult.WasExecuted && testResult.WasSuccessful)
                {
                    section.Add(new XElement("note", "This scenario passed"));
                }
                else if (testResult.WasExecuted && !testResult.WasSuccessful)
                {
                    section.Add(new XElement("note", "This scenario failed"));
                }
            }

            if (!string.IsNullOrEmpty(scenario.Description))
            {
                section.Add(new XElement("p", scenario.Description));
            }

            foreach (var step in scenario.Steps)
            {
                this.ditaStepFormatter.Format(section, step);
            }

            parentElement.Add(section);
        }
    }
}
