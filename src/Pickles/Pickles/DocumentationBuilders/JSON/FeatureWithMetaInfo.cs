using System;
using Pickles.DirectoryCrawler;
using Pickles.Parser;
using Pickles.TestFrameworks;

namespace Pickles.DocumentationBuilders.JSON
{
    public class FeatureWithMetaInfo
    {
        public string RelativeFolder { get; set; }
        public Feature Feature { get; set; }
        public TestResult Result { get; set; } 

        public FeatureWithMetaInfo(FeatureDirectoryTreeNode featureNodeTreeNode)
        {
            Feature = featureNodeTreeNode.Feature;
            RelativeFolder = featureNodeTreeNode.RelativePathFromRoot;
        }

        public FeatureWithMetaInfo(FeatureDirectoryTreeNode featureNodeTreeNode, TestResult result)
          : this(featureNodeTreeNode)
        {
          Result = result;
        }
    }
}