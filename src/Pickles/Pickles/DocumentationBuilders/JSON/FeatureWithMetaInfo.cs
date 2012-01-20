using System;
using Pickles.DirectoryCrawler;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.JSON
{
    public class FeatureWithMetaInfo
    {
        public string RelativeFolder { get; set; }
        public Feature Feature { get; set; }

        public FeatureWithMetaInfo(FeatureDirectoryTreeNode featureNodeTreeNode)
        {
            Feature = featureNodeTreeNode.Feature;
            RelativeFolder = featureNodeTreeNode.RelativePathFromRoot;
        }
    }
}