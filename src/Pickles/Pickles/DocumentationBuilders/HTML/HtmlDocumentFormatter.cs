#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using System.IO;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlDocumentFormatter
    {
        private readonly Configuration configuration;
        private readonly HtmlHeaderFormatter htmlHeaderFormatter;
        private readonly HtmlTableOfContentsFormatter htmlTableOfContentsFormatter;
        private readonly HtmlContentFormatter htmlContentFormatter;
        private readonly HtmlFooterFormatter htmlFooterFormatter;
        private readonly HtmlResourceSet htmlResources;

        public HtmlDocumentFormatter(Configuration configuration, HtmlHeaderFormatter htmlHeaderFormatter, HtmlTableOfContentsFormatter htmlTableOfContentsFormatter, HtmlContentFormatter htmlContentFormatter, HtmlFooterFormatter htmlFooterFormatter, HtmlResourceSet htmlResources)
        {
            this.configuration = configuration;
            this.htmlHeaderFormatter = htmlHeaderFormatter;
            this.htmlTableOfContentsFormatter = htmlTableOfContentsFormatter;
            this.htmlContentFormatter = htmlContentFormatter;
            this.htmlFooterFormatter = htmlFooterFormatter;
            this.htmlResources = htmlResources;
        }

        public XDocument Format(FeatureNode featureNode, GeneralTree<FeatureNode> features)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var featureNodeOutputPath = Path.Combine(this.configuration.OutputFolder.FullName, featureNode.RelativePathFromRoot);
            var featureNodeOutputUri = new Uri(featureNodeOutputPath);

            var container = new XElement(xmlns + "div", new XAttribute("id", "container"));
            container.Add(this.htmlHeaderFormatter.Format());
            container.Add(this.htmlTableOfContentsFormatter.Format(featureNode.Url, features));
            container.Add(this.htmlContentFormatter.Format(featureNode));
            container.Add(this.htmlFooterFormatter.Format());

            var body = new XElement(xmlns + "body");
            body.Add(container);

            var head = new XElement(xmlns + "head");
            head.Add(new XElement(xmlns + "title", string.Format("{0}", featureNode.Name)));

            head.Add(new XElement(xmlns + "link",
                         new XAttribute("rel", "stylesheet"),
                         new XAttribute("href", featureNodeOutputUri.MakeRelativeUri(this.htmlResources.MasterStylesheet)),
                         new XAttribute("type", "text/css")));

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
