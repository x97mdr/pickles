using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.Formatters
{
    public class HtmlScenarioFormatter
    {
        private readonly XNamespace xmlns;
        private readonly HtmlStepFormatter htmlStepFormatter;
        private readonly HtmlDescriptionFormatter htmlDescriptionFormatter;

        public HtmlScenarioFormatter(HtmlStepFormatter htmlStepFormatter, HtmlDescriptionFormatter htmlDescriptionFormatter)
        {
            this.htmlStepFormatter = htmlStepFormatter;
            this.htmlDescriptionFormatter = htmlDescriptionFormatter;
            this.xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        public XElement Format(Scenario scenario, int id)
        {
            return new XElement(xmlns + "li",
                       new XAttribute("id", id),
                       new XAttribute("class", "scenario"),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "scenario-heading"),
                           new XElement(xmlns + "h2", scenario.Name),
                           !string.IsNullOrWhiteSpace(scenario.Description) ? this.htmlDescriptionFormatter.Format(scenario.Description) : null
                       ),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "steps"),
                           new XElement(xmlns + "ul", scenario.Steps.Select(step => this.htmlStepFormatter.Format(step)))
                       )
                   );
        }
    }
}
