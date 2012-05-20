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

using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Extensions;
using log4net;

namespace Pickles
{
    public class HtmlDocumentationBuilder : IDocumentationBuilder
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Configuration configuration;
        private readonly DirectoryTreeCrawler featureCrawler;
        private readonly HtmlDocumentFormatter htmlDocumentFormatter;
        private readonly HtmlResourceWriter htmlResourceWriter;

        public HtmlDocumentationBuilder(Configuration configuration, DirectoryTreeCrawler featureCrawler,
                                        HtmlDocumentFormatter htmlDocumentFormatter,
                                        HtmlResourceWriter htmlResourceWriter)
        {
            this.configuration = configuration;
            this.featureCrawler = featureCrawler;
            this.htmlDocumentFormatter = htmlDocumentFormatter;
            this.htmlResourceWriter = htmlResourceWriter;
        }

        #region IDocumentationBuilder Members

        public void Build(GeneralTree<IDirectoryTreeNode> features)
        {
            if (log.IsInfoEnabled)
            {
                log.InfoFormat("Writing HTML to {0}", configuration.OutputFolder.FullName);
            }

            htmlResourceWriter.WriteTo(configuration.OutputFolder.FullName);

            var actionVisitor = new ActionVisitor<IDirectoryTreeNode>(node =>
                                                                          {
                                                                              if (node.IsIndexMarkDownNode())
                                                                              {
                                                                                  return;
                                                                              }

                                                                              string nodePath =
                                                                                  Path.Combine(
                                                                                      configuration.OutputFolder.
                                                                                          FullName,
                                                                                      node.RelativePathFromRoot);
                                                                              string htmlFilePath;

                                                                              if (node.IsContent)
                                                                              {
                                                                                  htmlFilePath =
                                                                                      nodePath.Replace(
                                                                                          Path.GetExtension(nodePath),
                                                                                          ".html");
                                                                              }
                                                                              else
                                                                              {
                                                                                  Directory.CreateDirectory(nodePath);

                                                                                  htmlFilePath = Path.Combine(nodePath,
                                                                                                              "index.html");
                                                                              }

                                                                              using (
                                                                                  var writer =
                                                                                      new StreamWriter(htmlFilePath,
                                                                                                       false,
                                                                                                       Encoding.UTF8))
                                                                              {
                                                                                  XDocument document =
                                                                                      htmlDocumentFormatter.Format(
                                                                                          node, features,
                                                                                          configuration.FeatureFolder);
                                                                                  document.Save(writer);
                                                                                  writer.Close();
                                                                              }
                                                                          });

            features.AcceptVisitor(actionVisitor);
        }

        #endregion
    }
}