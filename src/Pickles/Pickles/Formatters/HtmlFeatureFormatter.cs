using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using Pickles.Parser;
using MarkdownSharp;

namespace Pickles.Formatters
{
    public class HtmlFeatureFormatter
    {
        private readonly HtmlScenarioFormatter htmlScenarioFormatter;
        private readonly HtmlScenarioOutlineFormatter htmlScenarioOutlineFormatter;
        private readonly HtmlDescriptionFormatter htmlDescriptionFormatter;

        public HtmlFeatureFormatter(HtmlScenarioFormatter htmlScenarioFormatter, HtmlDescriptionFormatter htmlDescriptionFormatter, HtmlScenarioOutlineFormatter htmlScenarioOutlineFormatter)
        {
            this.htmlScenarioFormatter = htmlScenarioFormatter;
            this.htmlScenarioOutlineFormatter = htmlScenarioOutlineFormatter;
            this.htmlDescriptionFormatter = htmlDescriptionFormatter;
        }

        public XElement Format(Feature feature)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            var div = new XElement(xmlns + "div",
                        new XAttribute("id", "feature"),
                        new XElement(xmlns + "h1", feature.Name),
                        this.htmlDescriptionFormatter.Format(feature.Description)
                    );

            var scenarios = new XElement(xmlns + "ul", new XAttribute("id", "scenarios"));
            int id = 0;

            if (feature.Background != null)
            {
                scenarios.Add(this.htmlScenarioFormatter.Format(feature.Background, id++));
            }

            foreach (var scenario in feature.Scenarios)
            {
                scenarios.Add(this.htmlScenarioFormatter.Format(scenario, id++));
            }

            foreach (var scenarioOutline in feature.ScenarioOutlines)
            {
                scenarios.Add(this.htmlScenarioOutlineFormatter.Format(scenarioOutline, id++));
            }

            div.Add(scenarios);

            return div;
        }
    }
}
