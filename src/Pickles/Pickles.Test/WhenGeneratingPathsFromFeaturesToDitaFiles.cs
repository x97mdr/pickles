using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;
using Pickles.DocumentationBuilders.DITA;
using Pickles.DirectoryCrawler;
using System.IO;
using Pickles.Parser;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenGeneratingPathsFromFeaturesToDitaFiles : BaseFixture
    {
        [Test]
        public void ThenCanGeneratePathToTopLevelFeatureFileSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.FeatureFolder = new System.IO.DirectoryInfo(@"c:\features");
            var featureNode = new FeatureDirectoryTreeNode(new FileInfo(@"c:\features\the_feature.feature"), @"features\the_feature.feature", new Feature { Name = "The Feature" });

            var ditaMapPathGenerator = Kernel.Get<DitaMapPathGenerator>();

            var existingUri = ditaMapPathGenerator.GeneratePathToFeature(featureNode);
            Assert.AreEqual(@"the_feature.dita", existingUri.OriginalString);
        }

        [Test]
        public void ThenCanGeneratePathToDeepLevelFeatureFileSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.FeatureFolder = new System.IO.DirectoryInfo(@"c:\features");
            var featureNode = new FeatureDirectoryTreeNode(new FileInfo(@"c:\features\path\to\the_feature.feature"), @"features\path\to\the_feature.feature", new Feature { Name = "The Feature" });

            var ditaMapPathGenerator = Kernel.Get<DitaMapPathGenerator>();

            var existingUri = ditaMapPathGenerator.GeneratePathToFeature(featureNode);
            Assert.AreEqual(@"path/to/the_feature.dita", existingUri.OriginalString);
        }
    }
}
