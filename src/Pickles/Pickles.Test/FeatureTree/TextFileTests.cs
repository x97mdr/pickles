using System;
using NUnit.Framework;
using Pickles.FeatureTree;
using Should;

namespace Pickles.Test.FeatureTree
{
    [TestFixture]
    public class TextFileTests
    {
        private static readonly Folder parentFolder = Helpers.CreateSimpleFolder();

        [Test]
        public void Constructor_EmptyFeature_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new TextFile("Text", parentFolder, null));

            exception.ParamName.ShouldEqual("content");
        }

        [Test]
        public void Constructor_WithFeature_SetsContentProperty()
        {
            string text = "# Text #";

            var TextFile = new TextFile("filename.ext", parentFolder, text);

            TextFile.Content.ShouldEqual(text);
        }

        [Test]
        public void TextFile_Implements_ITreeItem()
        {
            var TextFile = new TextFile("filename.ext", parentFolder, "# Text #");

            TextFile.ShouldImplement<ITreeItem>();
        }
    }
}