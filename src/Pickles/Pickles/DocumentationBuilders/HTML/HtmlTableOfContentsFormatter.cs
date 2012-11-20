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
using System.IO;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using Pickles.DirectoryCrawler;
using Pickles.Extensions;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlTableOfContentsFormatter
    {
        private readonly HtmlImageResultFormatter imageResultFormatter;

        public HtmlTableOfContentsFormatter(HtmlImageResultFormatter imageResultFormatter)
        {
          this.imageResultFormatter = imageResultFormatter;
        }

        private XElement BuildListItems(XNamespace xmlns, Uri file, GeneralTree<IDirectoryTreeNode> features)
          {
              var ul = new XElement(xmlns + "ul", new XAttribute("class", "features"));

              foreach (var childNode in features.ChildNodes)
              {
                  if (childNode.Data.IsContent)
                  {
                      if (childNode.Data.IsIndexMarkDownNode())
                      {
                          continue;
                      }

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

        private static XElement AddNodeForHome(XNamespace xmlns, Uri file, DirectoryInfo rootFolder)
        {
            var rootfile = new FileInfo(Path.Combine(rootFolder.FullName, "index.html"));

            var xElement = new XElement(xmlns + "li", new XAttribute("class", "file"), new XAttribute("id", "root"));

            string nodeText = "Home";

            bool fileIsActuallyTheRoot = DetermineWhetherFileIsTheRootFile(file, rootfile);
            if (fileIsActuallyTheRoot)
            {
                xElement.Add(new XElement(xmlns + "span", new XAttribute("class", "current"), nodeText));
            }
            else
            {
                xElement.Add(new XElement(xmlns + "a",
                                          new XAttribute("href", file.GetUriForTargetRelativeToMe(rootfile, ".html")),
                                          nodeText));
            }

            return xElement;
        }

        private static bool DetermineWhetherFileIsTheRootFile(Uri file, FileInfo rootfile)
        {
            var fileInfo = new FileInfo(file.LocalPath);

            if (rootfile.DirectoryName != fileInfo.DirectoryName)
                return false; // they're not even in the same directory

            if (rootfile.FullName == file.LocalPath) return true; // it's really the same file

            if (fileInfo.Name == "")
                return true; // the file is actually the directory, so we consider that the root file

            if (fileInfo.Name.StartsWith("index", StringComparison.InvariantCultureIgnoreCase))
                return true; // the file is an index file, so we consider that the root

            return false;
        }

        private XElement AddNodeForFile(XNamespace xmlns, Uri file, GeneralTree<IDirectoryTreeNode> childNode)
        {
            var xElement = new XElement(xmlns + "li", new XAttribute("class", "file"));

            string nodeText = childNode.Data.Name;
            if (childNode.Data.OriginalLocationUrl == file)
            {
                xElement.Add(new XElement(xmlns + "span", new XAttribute("class", "current"), nodeText));
            }
            else
            {
                xElement.Add(new XElement(xmlns + "a", new XAttribute("href", childNode.Data.GetRelativeUriTo(file)),
                                          nodeText));
            }

          var featureNode = childNode.Data as FeatureDirectoryTreeNode;
          if (featureNode != null && this.imageResultFormatter != null)
          {
            Feature feature = featureNode.Feature;

            XElement formatForToC = this.imageResultFormatter.FormatForToC(feature);

            if (formatForToC != null)
            {
              xElement.Add(formatForToC);
            }
          }

          return xElement;
        }

        private XElement BuildCollapser(XNamespace xmlns)
        {
            return new XElement(xmlns + "p",
                                new XAttribute("class", "tocCollapser"),
                                new XAttribute("title", "Collapse Table of Content"),
                                new XText("«"));
        }

        public XElement Format(Uri file, GeneralTree<IDirectoryTreeNode> features, DirectoryInfo outputFolder)
        {
            XNamespace xmlns = HtmlNamespace.Xhtml;

            XElement ul = BuildListItems(xmlns, file, features);
            ul.AddFirst(AddNodeForHome(xmlns, file, outputFolder));

            return new XElement(
                xmlns + "div",
                new XAttribute("id", "toc"),
                BuildCollapser(xmlns),
                ul);
        }
    }
}