using System;
using NUnit.Framework;
using PicklesDoc.Pickles.FeatureTree;
using PicklesDoc.Pickles.Parser;
using Should;

namespace PicklesDoc.Pickles.Test.FeatureTree
{
    [TestFixture]
    public class FeatureFileTests : BaseFixture
    {
        private static readonly Folder parentFolder = Helpers.CreateSimpleFolder();

        [Test]
        public void Constructor_EmptyFeature_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new FeatureFile("feature", parentFolder, null, MockFileSystem));

            exception.ParamName.ShouldEqual("feature");
        }

        [Test]
        public void Constructor_WithFeature_SetsContentProperty()
        {
            var feature = new Feature();

            var featureFile = new FeatureFile("filename.ext", parentFolder, feature, MockFileSystem);

            featureFile.Content.ShouldEqual(feature);
        }

        [Test]
        public void FeatureFile_Implements_ITreeItem()
        {
            var featureFile = new FeatureFile("filename.ext", parentFolder, new Feature(), MockFileSystem);

            featureFile.ShouldImplement<ITreeItem>();
        }
    }
}