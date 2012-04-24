using System;
using NUnit.Framework;
using Pickles.FeatureTree;

namespace Pickles.Test.FeatureTree
{
  public class FolderTests
  {
    [Test]
    public void Constructor_NullFolderName_ThrowsArgumentNullException()
    {
      ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Folder(null));

      Assert.AreEqual("folderName", exception.ParamName);
    }

    [Test]
    public void Constructor_ValidFolderName_SetsNameProperty()
    {
      var folder = new Folder("Folder Name");

      Assert.AreEqual("Folder Name", folder.Name);
    }

    [Test]
    public void Constructor_WhiteSpaceFolderName_ThrowsArgumentNullException()
    {
      ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new Folder("     "));

      Assert.AreEqual("folderName", exception.ParamName);
    }

    [Test]
    public void Constructor_WithParentFolder_SetsParentFolderProperty()
    {
      var parentFolder = new Folder("Parent Folder Name");

      var folder = new Folder("Folder Name", parentFolder);

      Assert.AreEqual(parentFolder, folder.ParentFolder);
    }

    [Test]
    public void FindCommonAncestor_NullArgument_ThrowsArgumentNullException()
    {
      var folder = new Folder("Folder Name");

      var exception = Assert.Throws<ArgumentNullException>(() => folder.FindCommonAncestor(null));

      Assert.AreEqual("other", exception.ParamName);
    }

    [Test]
    public void FindCommonAncestor_ItsOwnParent_ReturnsParent()
    {
      var parentFolder = new Folder("Parent Folder Name");

      var folder = new Folder("Folder Name", parentFolder);

      ITreeItem commonAncestor = folder.FindCommonAncestor(parentFolder);

      Assert.AreEqual(parentFolder, commonAncestor);
    }

    [Test]
    public void FindCommonAncestor_ItsOwnGrandParent_ReturnsGrandParent()
    {
      var grandParentFolder = new Folder("Grand Parent Folder Name");

      var parentFolder = new Folder("Parent Folder Name", grandParentFolder);

      var folder = new Folder("Folder Name", parentFolder);

      ITreeItem commonAncestor = folder.FindCommonAncestor(grandParentFolder);

      Assert.AreEqual(grandParentFolder, commonAncestor);
    }

    [Test]
    public void FindCommonAncestor_TwoSiblings_ReturnsParent()
    {
      var parentFolder = new Folder("Parent Folder Name");

      var sibling1 = new Folder("Sibling 1", parentFolder);
      var sibling2 = new Folder("Sibling 2", parentFolder);

      ITreeItem commonAncestor = sibling1.FindCommonAncestor(sibling2);

      Assert.AreEqual(parentFolder, commonAncestor);
    }
  }
}