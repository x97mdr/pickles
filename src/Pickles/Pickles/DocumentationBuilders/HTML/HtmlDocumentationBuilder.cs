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
using System.IO;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using System.Xml;
using Pickles.Formatters;
using System.Xml.Linq;

namespace Pickles
{
    public class HtmlDocumentationBuilder : IDocumentationBuilder
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly FeatureCrawler featureCrawler;
        private readonly HtmlDocumentFormatter htmlDocumentFormatter;
        private readonly HtmlResourceWriter htmlResourceWriter;

        public HtmlDocumentationBuilder(FeatureCrawler featureCrawler, HtmlDocumentFormatter htmlDocumentFormatter, HtmlResourceWriter htmlResourceWriter)
        {
            this.featureCrawler = featureCrawler;
            this.htmlDocumentFormatter = htmlDocumentFormatter;
            this.htmlResourceWriter = htmlResourceWriter;
        }

        public void Build(DirectoryInfo inputPath, DirectoryInfo outputPath)
        {
            if (log.IsInfoEnabled)
            {
                log.InfoFormat("Writing HTML to {0}", outputPath.FullName);
            }

            this.htmlResourceWriter.WriteTo(outputPath.FullName);

            var features = this.featureCrawler.Crawl(inputPath);
            var actionVisitor = new ActionVisitor<FeatureNode>(node =>
                {
                    var nodePath = Path.Combine(outputPath.FullName, node.RelativePathFromRoot);

                    if (!node.IsDirectory)
                    {
                        var htmlFilePath = nodePath.Replace(Path.GetExtension(nodePath), ".xhtml");

                        using (var writer = new StreamWriter(htmlFilePath, false, Encoding.UTF8))
                        {
                            var document = this.htmlDocumentFormatter.Format(node, features);
                            document.Save(writer);
                            writer.Close();
                        }
                    }
                    else if (!node.IsEmpty)
                    {
                        Directory.CreateDirectory(nodePath);
                    }
                });

            features.AcceptVisitor(actionVisitor);
        }
    }
}
