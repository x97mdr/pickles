using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;

namespace Pickles.Formatters
{
    public class HtmlDocumentFormatter
    {
        private readonly HtmlTableOfContentsFormatter htmlTableOfContentsFormatter;
        private readonly HtmlFeatureFormatter htmlFeatureFormatter;
        private readonly HtmlFooterFormatter htmlFooterFormatter;

        public HtmlDocumentFormatter(HtmlTableOfContentsFormatter htmlTableOfContentsFormatter, HtmlFeatureFormatter htmlFeatureFormatter, HtmlFooterFormatter htmlFooterFormatter)
        {
            this.htmlTableOfContentsFormatter = htmlTableOfContentsFormatter;
            this.htmlFeatureFormatter = htmlFeatureFormatter;
            this.htmlFooterFormatter = htmlFooterFormatter;
        }

        public XDocument Format(FeatureNode featureNode, Uri stylesheet)
        {
            return Format(featureNode, null, stylesheet);
        }

        public XDocument Format(FeatureNode featureNode, GeneralTree<FeatureNode> features, Uri stylesheet)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            var head = new XElement(xmlns + "head");
            head.Add(new XElement(xmlns + "title", string.Format("{0}", featureNode.Feature.Title)));
            head.Add(new XElement(xmlns + "link",
                         new XAttribute("rel", "stylesheet"),
                         new XAttribute("href", featureNode.Url.MakeRelativeUri(stylesheet)),
                         new XAttribute("type", "text/css")));

            var body = new XElement(xmlns + "body");
            var container = new XElement(xmlns + "div", new XAttribute("id", "container"));
            body.Add(container);
            container.Add(new XElement(xmlns + "div", new XAttribute("id", "top")));
            if (features != null) container.Add(this.htmlTableOfContentsFormatter.Format(featureNode.Url, features));
            container.Add(this.htmlFeatureFormatter.Format(featureNode.Feature));
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
