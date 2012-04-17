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
  }
}