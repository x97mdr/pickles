using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace Pickles
{
    public class HtmlFeatureFormatter
    {
        private XElement BuildStep(XNamespace xmlns, ScenarioStep step)
        {
            return new XElement(xmlns + "li",
                       new XAttribute("class", "step"),
                       new XElement(xmlns + "span", new XAttribute("class", "keyword"), step.Keyword),
                       step.Text
                   );   
        }

        private XElement[] BuildSteps(XNamespace xmlns, ScenarioSteps steps)
        {
            return steps.Select(step => BuildStep(xmlns, step)).ToArray();
        }

        public XElement BuildScenario(XNamespace xmlns, Scenario scenario, int id)
        {
            return new XElement(xmlns + "li",
                       new XAttribute("id", id),
                       new XAttribute("class", "scenario"),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "scenario-heading"),
                           new XElement(xmlns + "h2", "Scenario: " + scenario.Title),
                           scenario.Description.Split('\n').Select(s => new XElement(xmlns + "p", s.Trim()))
                       ),
                       new XElement(xmlns + "div",
                           new XAttribute("class", "steps"),
                           new XElement(xmlns + "ul", BuildSteps(xmlns, scenario.Steps))
                       )
                   );
        }

        public XDocument BuildFrom(Feature feature)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var head = new XElement(xmlns + "head");
            var body = new XElement(xmlns + "body");
            var root = new XElement(xmlns + "html",
                           new XAttribute(XNamespace.Xml + "lang", "en"),
                           head, 
                           body);

            var document = new XDocument(
                                new XDeclaration("1.0", "UTF-8", null),
                                new XDocumentType("html", "-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", string.Empty),
                                root);

            head.Add(new XElement(xmlns + "title", string.Format("Feature: {0}", feature.Title)));
            body.Add(new XElement(xmlns + "div",
                         new XAttribute("id", "feature"),
                         new XAttribute("class", "feature"),
                         new XElement(xmlns + "h1", string.Format("Feature: {0}", feature.Title)),
                         feature.Description.Split('\n').Select(s => new XElement(xmlns + "p", s.Trim()))
                    ));

            var scenarios = new XElement(xmlns + "ul", new XAttribute("class", "scenarios"));
            int id = 0;
            foreach (var scenario in feature.Scenarios)
            {
                scenarios.Add(BuildScenario(xmlns, scenario, id++));
            }

            body.Add(scenarios);

            return document;
        }
    }
}
