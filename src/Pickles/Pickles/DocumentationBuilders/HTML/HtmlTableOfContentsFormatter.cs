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
                  ul.Add(AddNodeForFile(xmlns, file, childNode));
                }
                else
                {
                  ul.Add(AddNodeForDirectory(xmlns, file, childNode));
                }
            }

            return ul;
        }

      private XElement AddNodeForDirectory(XNamespace xmlns, Uri file, GeneralTree<IDirectoryTreeNode> childNode)
      {
        var xElement = new XElement(
          xmlns + "li",
          new XElement(xmlns + "div",
            new XAttribute("class", "directory"),
            new XElement(xmlns + "a",
              new XAttribute("href", childNode.Data.GetRelativeUriTo(file) + "index.html"),
              new XText(childNode.Data.Name))),
          BuildListItems(xmlns, file, childNode));

        return xElement;
      }

      private static XElement AddNodeForFile(XNamespace xmlns, Uri file, GeneralTree<IDirectoryTreeNode> childNode)
      {
        var xElement = new XElement(xmlns + "li", new XAttribute("class", "file"));

        if (childNode.Data.OriginalLocationUrl == file)
        {
          xElement.Add(new XElement(xmlns + "span", new XAttribute("class", "current"), childNode.Data.Name));
        }
        else
        {
          xElement.Add(new XElement(xmlns + "a", new XAttribute("href", childNode.Data.GetRelativeUriTo(file)), childNode.Data.Name));
        }

        return xElement;
      }

      private XElement BuildCollapser(XNamespace xmlns)
        {
            // <p class="tocCollapser" title="Collapse Table of Content">«</p>

            return new XElement(xmlns + "p",
                new XAttribute("class", "tocCollapser"),
                new XAttribute("title", "Collapse Table of Content"),
                new XText("«"));
        }

      public XElement Format(Uri file, GeneralTree<IDirectoryTreeNode> features)
      {
        var xmlns = HtmlNamespace.Xhtml;

        return new XElement(xmlns + "div",
                            new XAttribute("id", "toc"),
                            BuildCollapser(xmlns),
                            BuildListItems(xmlns, file, features)
          );
      }
    }
}
