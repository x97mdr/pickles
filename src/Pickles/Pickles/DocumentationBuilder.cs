using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using System.Xml;
using Pickles.Formatters;

namespace Pickles
{
    public class DocumentationBuilder : IDocumentationBuilder
    {
        private readonly HtmlFeatureParser htmlFeatureParser;
        private readonly FeatureCrawler featureCrawler;
        private readonly HtmlTableOfContentsFormatter tocFormatter;

        public DocumentationBuilder(HtmlFeatureParser htmlFeatureParser, FeatureCrawler featureCrawler, HtmlTableOfContentsFormatter tocFormatter)
        {
            this.htmlFeatureParser = htmlFeatureParser;
            this.featureCrawler = featureCrawler;
            this.tocFormatter = tocFormatter;
        }

        public void Build(DirectoryInfo inputPath, DirectoryInfo outputPath)
        {
            var features = this.featureCrawler.Crawl(inputPath);
            // enumerate features, generating HTML documentation to output folder building table of contents as moving along.
            var actionVisitor = new ActionVisitor<FeatureNode>(node =>
                {
                });
        }
    }
}
