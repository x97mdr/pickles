using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Pickles
{
    public class HtmlFeatureBuilder
    {
        private readonly XDocument document;
        private readonly XElement root;
        private readonly XElement head;
        private readonly XElement body;

        public HtmlFeatureBuilder()
        {
            this.head = new XElement("head");
            this.body = new XElement("body");
            this.root = new XElement("html", this.head, this.body);

            this.document = new XDocument(
                                new XDeclaration("1.0", "UTF-8", "yes"), 
                                new XDocumentType("html", "PUBLIC", "-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"),
                                this.root);
        }

        public void SetNameAndDescription(string name, string description)
        {
            this.head.Add(new XElement("title", name));
            this.body.Add(new XElement("h1", string.Format("Feature: {0}", name)));
            this.body.Add(new XElement("p", description));
        }

        public XDocument GetResult()
        {
            return document;
        }
    }
}
