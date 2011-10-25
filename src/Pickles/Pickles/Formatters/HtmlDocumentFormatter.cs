using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using System.IO;

namespace Pickles.Formatters
{
    public class HtmlDocumentFormatter
    {
        private readonly HtmlTableOfContentsFormatter htmlTableOfContentsFormatter;
        private readonly HtmlFeatureFormatter htmlFeatureFormatter;
        private readonly HtmlMarkdownFormatter htmlMarkdownFormatter;
        private readonly HtmlFooterFormatter htmlFooterFormatter;

        public HtmlDocumentFormatter(HtmlTableOfContentsFormatter htmlTableOfContentsFormatter, HtmlFeatureFormatter htmlFeatureFormatter, HtmlMarkdownFormatter htmlMarkdownFormatter, HtmlFooterFormatter htmlFooterFormatter)
        {
            this.htmlTableOfContentsFormatter = htmlTableOfContentsFormatter;
            this.htmlFeatureFormatter = htmlFeatureFormatter;
            this.htmlMarkdownFormatter = htmlMarkdownFormatter;
            this.htmlFooterFormatter = htmlFooterFormatter;
        }

        public XDocument Format(FeatureNode featureNode)
        {
            return Format(featureNode, null, null);
        }

        public XDocument Format(FeatureNode featureNode, GeneralTree<FeatureNode> features, Uri stylesheetUri)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            var head = new XElement(xmlns + "head");
            head.Add(new XElement(xmlns + "title", string.Format("{0}", featureNode.Feature.Name)));

            if (stylesheetUri != null)
            {
                head.Add(new XElement(xmlns + "link",
                             new XAttribute("rel", "stylesheet"),
                             new XAttribute("href", stylesheetUri),
                             new XAttribute("type", "text/css")));
            }

            var body = new XElement(xmlns + "body");
            var container = new XElement(xmlns + "div", new XAttribute("id", "container"));
            body.Add(container);
            container.Add(new XElement(xmlns + "div", new XAttribute("id", "top")));
            if (features != null) container.Add(this.htmlTableOfContentsFormatter.Format(featureNode.Url, features));

            if (featureNode.Type == FeatureNodeType.Feature)
            {
                container.Add(this.htmlFeatureFormatter.Format(featureNode.Feature));
            }
            else if (featureNode.Type == FeatureNodeType.Markdown)
            {
                container.Add(this.htmlMarkdownFormatter.Format(File.ReadAllText(featureNode.Location.FullName)));
            }
            
            container.Add(new XElement(xmlns + "div", new XAttribute("id", "footer"), this.htmlFooterFormatter.Format()));

            var html = new XElement(xmlns + "html",
                           new XAttribute(XNamespace.Xml + "lang", "en"),
                           head,
                           body);

            var document = new XDocument(
                                new XDeclaration("1.0", "UTF-8", null),
                                new XDocumentType("html", "-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", string.Empty),
                                html);

            return document;
        }
    }
}
