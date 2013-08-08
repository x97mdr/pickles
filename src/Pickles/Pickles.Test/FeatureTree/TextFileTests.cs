using System;
using NUnit.Framework;
using PicklesDoc.Pickles.FeatureTree;
using Should;

namespace PicklesDoc.Pickles.Test.FeatureTree
{
    [TestFixture]
    public class TextFileTests : BaseFixture
    {
        private static readonly Folder parentFolder = Helpers.CreateSimpleFolder();

        [Test]
        public void Constructor_EmptyFeature_ThrowsArgumentException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new TextFile("Text", parentFolder, null, MockFileSystem));

            exception.ParamName.ShouldEqual("content");
        }

        [Test]
        public void Constructor_WithFeature_SetsContentProperty()
        {
            string text = "# Text #";

            var TextFile = new TextFile("filename.ext", parentFolder, text, MockFileSystem);

            TextFile.Content.ShouldEqual(text);
        }

        [Test]
        public void TextFile_Implements_ITreeItem()
        {
            var TextFile = new TextFile("filename.ext", parentFolder, "# Text #", MockFileSystem);

            TextFile.ShouldImplement<ITreeItem>();
        }
    }
}