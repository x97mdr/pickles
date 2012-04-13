using System;
using NUnit.Framework;
using Pickles.FeatureTree;

namespace Pickles.Test.FeatureTree
{
    [TestFixture]
    public class TextFileTests
    {
        private static readonly Folder parentFolder = new Folder();

        [Test]
        public void Constructor_EmptyFeature_ThrowsArgumentException()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new TextFile("Text", parentFolder, null));

            Assert.AreEqual("content", exception.ParamName);
        }

        [Test]
        public void Constructor_WithFeature_SetsContentProperty()
        {
            var text = "# Text #";

            var TextFile = new TextFile("filename.ext", parentFolder, text);

            Assert.AreEqual(text, TextFile.Content);
        }

        [Test]
        public void TextFile_Implements_ITreeItem()
        {
            var TextFile = new TextFile("filename.ext", parentFolder, "# Text #");

// ReSharper disable CSharpWarnings::CS0183
            Assert.IsTrue(TextFile is ITreeItem);
// ReSharper restore CSharpWarnings::CS0183
        }
    }
}