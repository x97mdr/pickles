using System;
using NUnit.Framework;
using Pickles.FeatureTree;
using Pickles.Parser;
using Should;

namespace Pickles.Test.FeatureTree
{
    [TestFixture]
    public class FeatureFileTests
    {
        private static readonly Folder parentFolder = Helpers.CreateSimpleFolder();

        [Test]
        public void Constructor_EmptyFeature_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new FeatureFile("feature", parentFolder, null));

            exception.ParamName.ShouldEqual("feature");
        }

        [Test]
        public void Constructor_WithFeature_SetsContentProperty()
        {
            var feature = new Feature();

            var featureFile = new FeatureFile("filename.ext", parentFolder, feature);

            featureFile.Content.ShouldEqual(feature);
        }

        [Test]
        public void FeatureFile_Implements_ITreeItem()
        {
            var featureFile = new FeatureFile("filename.ext", parentFolder, new Feature());

            featureFile.ShouldImplement<ITreeItem>();
        }
    }
}