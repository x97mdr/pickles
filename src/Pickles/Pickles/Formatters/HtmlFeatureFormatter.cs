using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechTalk.SpecFlow.Parser.SyntaxElements;
using NGenerics.DataStructures.Trees;

namespace Pickles.Formatters
{
    public class HtmlFeatureFormatter
    {
        private readonly HtmlScenarioFormatter htmlScenarioFormatter;

        public HtmlFeatureFormatter(HtmlScenarioFormatter htmlScenarioFormatter)
        {
            this.htmlScenarioFormatter = htmlScenarioFormatter;
        }

        public XElement Format(Feature feature)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            var div = new XElement(xmlns + "div",
                        new XAttribute("id", "feature"),
                        new XAttribute("class", "feature"),
                        new XElement(xmlns + "h1", feature.Title), feature.Description.Split('\n').Select(s => new XElement(xmlns + "p", s.Trim()))
                    );

            var scenarios = new XElement(xmlns + "ul", new XAttribute("class", "scenarios"));
            int id = 0;
            foreach (var scenario in feature.Scenarios)
            {
                scenarios.Add(this.htmlScenarioFormatter.Format(scenario, id++));
            }

            div.Add(scenarios);

            return div;
        }
    }
}
