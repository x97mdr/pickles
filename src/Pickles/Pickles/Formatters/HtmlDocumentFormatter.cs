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

        public HtmlDocumentFormatter(HtmlTableOfContentsFormatter htmlTableOfContentsFormatter, HtmlFeatureFormatter htmlFeatureFormatter)
        {
            this.htmlTableOfContentsFormatter = htmlTableOfContentsFormatter;
            this.htmlFeatureFormatter = htmlFeatureFormatter;
        }

        public XDocument Format(FeatureNode featureNode)
        {
            return Format(featureNode, null);
        }

        public XDocument Format(FeatureNode featureNode, GeneralTree<FeatureNode> features)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            var head = new XElement(xmlns + "head");
            head.Add(new XElement(xmlns + "title", string.Format("{0}", featureNode.Feature.Title)));

            var body = new XElement(xmlns + "body");
            if (features != null)
            {
                body.Add(this.htmlTableOfContentsFormatter.Format(featureNode.Url, features));
            }
            body.Add(this.htmlFeatureFormatter.Format(featureNode.Feature));

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
