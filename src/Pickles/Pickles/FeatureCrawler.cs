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

        public FeatureCrawler(FeatureParser featureParser)
        {
            this.featureParser = featureParser;
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

            foreach (var file in directory.GetFiles("*.feature"))
            {
                tree.Add(new FeatureNode
                {
                    Location = file,
                    Url = new Uri(file.FullName),
                    RelativePathFromRoot = PathExtensions.MakeRelativePath(rootNode.Location, file),
                    Feature = this.featureParser.Parse(file.FullName)
                });
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                tree.Add(Crawl(subDirectory, rootNode));
            }

            return tree;
        }
    }
}
