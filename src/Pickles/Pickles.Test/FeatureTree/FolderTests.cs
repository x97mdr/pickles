using System;
using NUnit.Framework;
using Pickles.FeatureTree;
using Should;

namespace Pickles.Test.FeatureTree
{
    public class FolderTests
    {
        [Test]
        public void Constructor_NullFolderName_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Folder(null));

            exception.ParamName.ShouldEqual("folderName");
        }

        [Test]
        public void Constructor_ValidFolderName_SetsNameProperty()
        {
            var folder = new Folder("Folder Name");

            folder.Name.ShouldEqual("Folder Name");
        }

        [Test]
        public void Constructor_WhiteSpaceFolderName_ThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new Folder("     "));

            exception.ParamName.ShouldEqual("folderName");
        }

        [Test]
        public void Constructor_WithParentFolder_SetsParentFolderProperty()
        {
            var parentFolder = new Folder("Parent Folder Name");

            var folder = new Folder("Folder Name", parentFolder);

            folder.ParentFolder.ShouldEqual(parentFolder);
        }

        [Test]
        public void FindCommonAncestor_NullArgument_ThrowsArgumentNullException()
        {
            var folder = new Folder("Folder Name");

            var exception = Assert.Throws<ArgumentNullException>(() => folder.FindCommonAncestor(null));

            exception.ParamName.ShouldEqual("other");
        }

        [Test]
        public void FindCommonAncestor_ItsOwnParent_ReturnsParent()
        {
            var parentFolder = new Folder("Parent Folder Name");

            var folder = new Folder("Folder Name", parentFolder);

            ITreeItem commonAncestor = folder.FindCommonAncestor(parentFolder);

            commonAncestor.ShouldEqual(parentFolder);
        }

        [Test]
        public void FindCommonAncestor_ItsOwnGrandParent_ReturnsGrandParent()
        {
            var grandParentFolder = new Folder("Grand Parent Folder Name");

            var parentFolder = new Folder("Parent Folder Name", grandParentFolder);

            var folder = new Folder("Folder Name", parentFolder);

            ITreeItem commonAncestor = folder.FindCommonAncestor(grandParentFolder);

            commonAncestor.ShouldEqual(grandParentFolder);
        }

        [Test]
        public void FindCommonAncestor_TwoSiblings_ReturnsParent()
        {
            var parentFolder = new Folder("Parent Folder Name");

            var sibling1 = new Folder("Sibling 1", parentFolder);
            var sibling2 = new Folder("Sibling 2", parentFolder);

            ITreeItem commonAncestor = sibling1.FindCommonAncestor(sibling2);

            commonAncestor.ShouldEqual(parentFolder);
        }

        [Test]
        public void FindCommonAncestor_AFolderAndItsChild_ReturnsTheFolder()
        {
            var folder = new Folder("Folder");
            var itsChild = new Folder("Its child", folder);

            ITreeItem commonAncestor = folder.FindCommonAncestor(itsChild);

            commonAncestor.ShouldEqual(folder);
        }
    }
}