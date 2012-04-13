using System;
using NUnit.Framework;
using Pickles.FeatureTree;
using Pickles.Parser;

namespace Pickles.Test.FeatureTree
{
    [TestFixture]
    public class FeatureFileTests
    {
        private static readonly Folder parentFolder = new Folder();

        [Test]
        public void Constructor_EmptyFeature_ThrowsArgumentException()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new FeatureFile("feature", parentFolder, null));

            Assert.AreEqual("feature", exception.ParamName);
        }

        [Test]
        public void Constructor_WithFeature_SetsContentProperty()
        {
            var feature = new Feature();

            var featureFile = new FeatureFile("filename.ext", parentFolder, feature);

            Assert.AreEqual(feature, featureFile.Content);
        }

        [Test]
        public void FeatureFile_Implements_ITreeItem()
        {
            var featureFile = new FeatureFile("filename.ext", parentFolder, new Feature());

// ReSharper disable CSharpWarnings::CS0183
            Assert.IsTrue(featureFile is ITreeItem);
// ReSharper restore CSharpWarnings::CS0183
        }

        //[Test]
        //public void Constructor_NullFolder_ThrowsArgumentNullException()
        //{
        //    ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new FeatureFile(null, null, new Feature()));

        //    Assert.AreEqual("folder", exception.ParamName);
        //}
    }
}