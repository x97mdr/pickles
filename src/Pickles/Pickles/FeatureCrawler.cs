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
        public GeneralTree<FeatureNode> Crawl(string directory)
        {
            return Crawl(new DirectoryInfo(directory));
        }

        public GeneralTree<FeatureNode> Crawl(DirectoryInfo directory)
        {
            var tree = new GeneralTree<FeatureNode>(new FeatureNode
            {
                Location = directory,
                Url = new Uri(directory.FullName)
            });

            foreach (var file in directory.GetFiles("*.feature"))
            {
                tree.Add(new FeatureNode
                {
                    Location = file,
                    Url = new Uri(file.FullName)
                });
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                tree.Add(Crawl(subDirectory));
            }

            return tree;
        }
    }
}
