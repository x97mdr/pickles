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

namespace Pickles.Formatters
{
    public class HtmlScenarioFormatter
    {
        private readonly XNamespace xmlns;
        private readonly HtmlStepFormatter htmlStepFormatter;
        private readonly HtmlDescriptionFormatter htmlDescriptionFormatter;
        private readonly Configuration configuration;
        private readonly Results results;
        private readonly HtmlResourceSet htmlResourceSet;

        public HtmlScenarioFormatter(
            HtmlStepFormatter htmlStepFormatter, 
            HtmlDescriptionFormatter htmlDescriptionFormatter,
            Configuration configuration,
            Results results,
            HtmlResourceSet htmlResourceSet)
        {
            this.htmlStepFormatter = htmlStepFormatter;
            this.htmlDescriptionFormatter = htmlDescriptionFormatter;
            this.configuration = configuration;
            this.results = results;
            this.htmlResourceSet = htmlResourceSet;
            this.xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        private XElement BuildResultImage(Scenario scenario)
        {
            if (configuration.ShouldLinkResults)
            {
                TestResult scenarioResult = this.results.GetScenarioResult(scenario);
                if (!scenarioResult.WasExecuted || !scenarioResult.IsSuccessful) return null;

                return new XElement(this.xmlns + "div",
                           new XAttribute("class", "float-right"),
                           new XElement(this.xmlns + "img",
                               new XAttribute("src", scenarioResult.IsSuccessful ? this.htmlResourceSet.SuccessImage : this.htmlResourceSet.FailureImage),
                               new XAttribute("title", scenarioResult.IsSuccessful ? "Successful" : "Failed"),
                               new XAttribute("alt", scenarioResult.IsSuccessful ? "Successful" : "Failed")
                            )
                        );
            }

            return null;
        }

        public XElement Format(Scenario scenario, int id)
        {
            return new XElement(xmlns + "li",
                       new XAttribute("id", id),
                       new XAttribute("class", "scenario"),
                       BuildResultImage(scenario),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "scenario-heading"),
                           new XElement(xmlns + "h2", scenario.Name),
                           this.htmlDescriptionFormatter.Format(scenario.Description)
                       ),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "steps"),
                           new XElement(xmlns + "ul", scenario.Steps.Select(step => this.htmlStepFormatter.Format(step)))
                       )
                   );
        }
    }
}
