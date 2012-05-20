using System;
using NUnit.Framework;
using Pickles.FeatureTree;

namespace Pickles.Test.FeatureTree
{
    [TestFixture]
    public class FileBaseTests
    {
        private static readonly Folder parentFolder = Helpers.CreateSimpleFolder();

        private class TestableFileBase : FileBase
        {
            public TestableFileBase(string fileName, Folder folder)
                : base(fileName, folder)
            {
            }
        }

        [Test]
        public void Constructor_EmptyFileNameAndExtension_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TestableFileBase("    .ext", parentFolder));

            Assert.AreEqual("fileName", exception.ParamName);
        }

        [Test]
        public void Constructor_EmptyFileName_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new TestableFileBase(" ", parentFolder));

            Assert.AreEqual("fileName", exception.ParamName);
        }

        [Test]
        public void Constructor_EmptyFolder_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new TestableFileBase("feature", null));

            Assert.AreEqual("folder", exception.ParamName);
        }

        [Test]
        public void Constructor_ExtensionOnly_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new TestableFileBase(".ext", parentFolder));

            Assert.AreEqual("fileName", exception.ParamName);
        }

        [Test]
        public void Constructor_FileNameWithExtension_RemovesExtension()
        {
            var featureFile = new TestableFileBase("Feature.ext", parentFolder);

            Assert.AreEqual("Feature", featureFile.Name);
        }

        [Test]
        public void Constructor_NullFileName_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new TestableFileBase(null, parentFolder));

            Assert.AreEqual("fileName", exception.ParamName);
        }

        [Test]
        public void Constructor_ValidFileName_SetsNameProperty()
        {
            var featureFile = new TestableFileBase("Feature", parentFolder);

            Assert.AreEqual("Feature", featureFile.Name);
        }

        [Test]
        public void Constructor_WithFeature_SetsFolderProperty()
        {
            var featureFile = new TestableFileBase("filename.ext", parentFolder);

            Assert.AreEqual(parentFolder, featureFile.Folder);
        }

        [Test]
        public void FindCommonAncestor_ItsOwnParent_ReturnsParent()
        {
            var file = new TestableFileBase("filename.ext", parentFolder);

            ITreeItem commonAncestor = file.FindCommonAncestor(parentFolder);

            Assert.AreEqual(parentFolder, commonAncestor);
        }

        [Test]
        public void FindCommonAncestor_NullArgument_ThrowsArgumentNullException()
        {
            var file = new TestableFileBase("filename.ext", parentFolder);

            Assert.Throws<ArgumentNullException>(() => file.FindCommonAncestor(null));
        }
    }
}