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
        private readonly StylesheetWriter stylesheetWriter;

        public HtmlDocumentationBuilder(FeatureCrawler featureCrawler, HtmlDocumentFormatter htmlDocumentFormatter, StylesheetWriter stylesheetWriter)
        {
            this.featureCrawler = featureCrawler;
            this.htmlDocumentFormatter = htmlDocumentFormatter;
            this.stylesheetWriter = stylesheetWriter;
        }

        public void Build(DirectoryInfo inputPath, DirectoryInfo outputPath)
        {
            if (log.IsInfoEnabled)
            {
                log.InfoFormat("Writing HTML to {0}", outputPath.FullName);
            }

            var masterCssPath = this.stylesheetWriter.WriteTo(outputPath.FullName);
            var masterCssUri = new Uri(masterCssPath);

            var features = this.featureCrawler.Crawl(inputPath);
            var actionVisitor = new ActionVisitor<FeatureNode>(node =>
                {
                    var nodePath = Path.Combine(outputPath.FullName, node.RelativePathFromRoot);

                    if (!node.IsDirectory)
                    {
                        var nodeUri = new Uri(nodePath);
                        var relativeMasterCssUri = nodeUri.MakeRelativeUri(masterCssUri);

                        var htmlFilePath = nodePath.Replace(".feature", ".xhtml");
                        using (var writer = new StreamWriter(htmlFilePath, false, Encoding.UTF8))
                        {
                            var document = this.htmlDocumentFormatter.Format(node, features, relativeMasterCssUri);
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
