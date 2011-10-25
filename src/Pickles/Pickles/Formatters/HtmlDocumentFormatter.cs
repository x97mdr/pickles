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
        private readonly HtmlHeaderFormatter htmlHeaderFormatter;
        private readonly HtmlTableOfContentsFormatter htmlTableOfContentsFormatter;
        private readonly HtmlContentFormatter htmlContentFormatter;
        private readonly HtmlFooterFormatter htmlFooterFormatter;

        public HtmlDocumentFormatter(HtmlHeaderFormatter htmlHeaderFormatter, HtmlTableOfContentsFormatter htmlTableOfContentsFormatter, HtmlContentFormatter htmlContentFormatter, HtmlFooterFormatter htmlFooterFormatter)
        {
            this.htmlHeaderFormatter = htmlHeaderFormatter;
            this.htmlTableOfContentsFormatter = htmlTableOfContentsFormatter;
            this.htmlContentFormatter = htmlContentFormatter;
            this.htmlFooterFormatter = htmlFooterFormatter;
        }

        public XDocument Format(FeatureNode featureNode, GeneralTree<FeatureNode> features, Uri stylesheetUri)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            var container = new XElement(xmlns + "div", new XAttribute("id", "container"));
            container.Add(this.htmlHeaderFormatter.Format());
            container.Add(this.htmlTableOfContentsFormatter.Format(featureNode.Url, features));
            container.Add(this.htmlContentFormatter.Format(featureNode));
            container.Add(this.htmlFooterFormatter.Format());

            var body = new XElement(xmlns + "body");
            body.Add(container);

            var head = new XElement(xmlns + "head");
            head.Add(new XElement(xmlns + "title", string.Format("{0}", featureNode.Name)));

            if (stylesheetUri != null)
            {
                head.Add(new XElement(xmlns + "link",
                             new XAttribute("rel", "stylesheet"),
                             new XAttribute("href", stylesheetUri),
                             new XAttribute("type", "text/css")));
            }

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
