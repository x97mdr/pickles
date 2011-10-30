using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using NGenerics.DataStructures.Trees;

namespace Pickles.Formatters
{
    public class HtmlTableOfContentsFormatter
    {
        private XElement BuildListItems(XNamespace xmlns, Uri file, GeneralTree<FeatureNode> features)
        {
            var ul = new XElement(xmlns + "ul", new XAttribute("class", "features"));

            foreach (var childNode in features.ChildNodes)
            {
                if (childNode.Data.IsContent)
                {
                    ul.Add(new XElement(xmlns + "li",
                                new XElement(xmlns + "a",
                                    new XAttribute("href", childNode.Data.GetRelativeUriTo(file)),
                                    childNode.Data.Name.ExpandWikiWord())));
                }
                else
                {
                    ul.Add(new XElement(xmlns + "li", new XText(childNode.Data.Name.ExpandWikiWord()), BuildListItems(xmlns, file, childNode)));
                }
            }

            return ul;
        }

        public XElement Format(FileInfo file, GeneralTree<FeatureNode> features)
        {
            return Format(new Uri(file.FullName), features);
        }

        public XElement Format(Uri file, GeneralTree<FeatureNode> features)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            return new XElement(xmlns + "div",
                       new XAttribute("id", "toc"),
                       BuildListItems(xmlns, file, features)
                   );
        }
    }
}
