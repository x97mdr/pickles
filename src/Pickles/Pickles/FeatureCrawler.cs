using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;

namespace Pickles
{
    public class FeatureCrawler
    {
        private readonly FeatureParser featureParser;
        private readonly RelevantFileDetector relevantFileDetector;

        public FeatureCrawler(FeatureParser featureParser, RelevantFileDetector relevantFileDetector)
        {
            this.featureParser = featureParser;
            this.relevantFileDetector = relevantFileDetector;
        }

        public GeneralTree<FeatureNode> Crawl(string directory)
        {
            return Crawl(new DirectoryInfo(directory), null);
        }

        public GeneralTree<FeatureNode> Crawl(DirectoryInfo directory)
        {
            return Crawl(directory, null);
        }

        private GeneralTree<FeatureNode> Crawl(DirectoryInfo directory, FeatureNode rootNode)
        {
            var currentNode = new FeatureNode
            {
                Location = directory,
                Url = new Uri(directory.FullName),
                RelativePathFromRoot = rootNode == null ? @".\" : PathExtensions.MakeRelativePath(rootNode.Location, directory)
            };

            if (rootNode == null)
            {
                rootNode = currentNode;
            }

            var tree = new GeneralTree<FeatureNode>(currentNode);

            bool isRelevantFileFound = false;
            foreach (var file in directory.GetFiles().Where(file => this.relevantFileDetector.IsRelevant(file)))
            {
                isRelevantFileFound = true;
                var node = new FeatureNode
                {
                    Location = file,
                    Url = new Uri(file.FullName),
                    RelativePathFromRoot = PathExtensions.MakeRelativePath(rootNode.Location, file)
                };

                if (node.Type == FeatureNodeType.Feature) node.Feature = this.featureParser.Parse(file.FullName);
                tree.Add(node);
            }

            bool isRelevantDirectoryFound = false;
            foreach (var subDirectory in directory.GetDirectories())
            {
                var subTree = Crawl(subDirectory, rootNode);
                if (subTree != null)
                {
                    isRelevantDirectoryFound = true;
                    tree.Add(subTree);
                }
            }

            if (!isRelevantFileFound && !isRelevantDirectoryFound) return null;

            return tree;
        }
    }
}
