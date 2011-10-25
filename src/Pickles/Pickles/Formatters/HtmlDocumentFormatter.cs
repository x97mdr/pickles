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
<<<<<<< HEAD
        private readonly HtmlFeatureFormatter htmlFeatureFormatter;
        private readonly HtmlMarkdownFormatter htmlMarkdownFormatter;
        private readonly HtmlFooterFormatter htmlFooterFormatter;

        public HtmlDocumentFormatter(HtmlTableOfContentsFormatter htmlTableOfContentsFormatter, HtmlFeatureFormatter htmlFeatureFormatter, HtmlMarkdownFormatter htmlMarkdownFormatter, HtmlFooterFormatter htmlFooterFormatter)
=======
        private readonly HtmlContentFormatter htmlContentFormatter;
        private readonly HtmlFooterFormatter htmlFooterFormatter;

        public HtmlDocumentFormatter(HtmlHeaderFormatter htmlHeaderFormatter, HtmlTableOfContentsFormatter htmlTableOfContentsFormatter, HtmlContentFormatter htmlContentFormatter, HtmlFooterFormatter htmlFooterFormatter)
>>>>>>> 6b4884549b208cd99282c1179c9b3c0ef4d3225c
        {
            this.htmlHeaderFormatter = htmlHeaderFormatter;
            this.htmlTableOfContentsFormatter = htmlTableOfContentsFormatter;
<<<<<<< HEAD
            this.htmlFeatureFormatter = htmlFeatureFormatter;
            this.htmlMarkdownFormatter = htmlMarkdownFormatter;
=======
            this.htmlContentFormatter = htmlContentFormatter;
>>>>>>> 6b4884549b208cd99282c1179c9b3c0ef4d3225c
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

<<<<<<< HEAD
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

=======
>>>>>>> 6b4884549b208cd99282c1179c9b3c0ef4d3225c
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
