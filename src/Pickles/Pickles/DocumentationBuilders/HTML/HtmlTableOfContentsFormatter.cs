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
using System.IO;
using NGenerics.DataStructures.Trees;
using Pickles.Extensions;
using Pickles.DirectoryCrawler;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlTableOfContentsFormatter
    {
        private XElement BuildListItems(XNamespace xmlns, Uri file, GeneralTree<IDirectoryTreeNode> features)
        {
            var ul = new XElement(xmlns + "ul", new XAttribute("class", "features"));

            foreach (var childNode in features.ChildNodes)
            {
                if (childNode.Data.IsContent)
                {
                    ul.Add(
                        new XElement(
                            xmlns + "li",
                            new XAttribute("class", "file"),
                            new XElement(xmlns + "a", new XAttribute("href", childNode.Data.GetRelativeUriTo(file)), childNode.Data.Name)));
                }
                else
                {
                    ul.Add(new XElement(xmlns + "li", 
                               new XElement(xmlns + "div",
                                   new XAttribute("class", "directory"),
                                   new XText(childNode.Data.Name)
                               ), BuildListItems(xmlns, file, childNode)));
                }
            }

            return ul;
        }

        public XElement Format(FileInfo file, GeneralTree<IDirectoryTreeNode> features)
        {
            return Format(new Uri(file.FullName), features);
        }

        public XElement Format(Uri file, GeneralTree<IDirectoryTreeNode> features)
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");

            return new XElement(xmlns + "div",
                       new XAttribute("id", "toc"),
                       BuildListItems(xmlns, file, features)
                   );
        }
    }
}
