using System;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.DocumentationBuilders.JSON
{
    public class FeatureWithMetaInfo
    {
        public FeatureWithMetaInfo(FeatureDirectoryTreeNode featureNodeTreeNode)
        {
            this.Feature = featureNodeTreeNode.Feature;
            this.RelativeFolder = featureNodeTreeNode.RelativePathFromRoot;
        }

        public FeatureWithMetaInfo(FeatureDirectoryTreeNode featureNodeTreeNode, TestResult result)
            : this(featureNodeTreeNode)
        {
            this.Result = result;
        }

        public string RelativeFolder { get; set; }
        public Feature Feature { get; set; }
        public TestResult Result { get; set; }
    }
}