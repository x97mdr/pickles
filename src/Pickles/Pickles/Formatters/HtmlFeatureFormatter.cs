using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace Pickles.Formatters
{
    public class HtmlFeatureFormatter
    {
        private readonly HtmlScenarioFormatter htmlScenarioFormatter;

        public HtmlFeatureFormatter(HtmlScenarioFormatter htmlScenarioFormatter)
        {
            this.htmlScenarioFormatter = htmlScenarioFormatter;
        }

        public XDocument Format(Feature feature)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            
            var head = new XElement(xmlns + "head");
            head.Add(new XElement(xmlns + "title", string.Format("Feature: {0}", feature.Title)));
            
            var body = new XElement(xmlns + "body");
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
                scenarios.Add(this.htmlScenarioFormatter.Format(scenario, id++));
            }

            body.Add(scenarios);

            var root = new XElement(xmlns + "html",
                           new XAttribute(XNamespace.Xml + "lang", "en"),
                           head, 
                           body);

            var document = new XDocument(
                                new XDeclaration("1.0", "UTF-8", null),
                                new XDocumentType("html", "-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", string.Empty),
                                root);

            return document;
        }
    }
}
