using System;
using System.IO;
using NUnit.Framework;
using Pickles.Extensions;

namespace Pickles.Test.Extensions
{
  [TestFixture]
  public class UriExtensionsTests
  {
    [Test]
    public void DirectoryInfo_WithTrailingSlash_ProducesUriWithTrailingSlash()
    {
      var directoryInfo = new DirectoryInfo(@"c:\temp\");

      Uri uri = directoryInfo.ToUri();

      Assert.AreEqual("file:///c:/temp/", uri.ToString());
    }

    [Test]
    public void DirectoryInfo_WithoutTrailingSlash_ProducesUriWithTrailingSlash()
    {
      var directoryInfo = new DirectoryInfo(@"c:\temp");

      Uri uri = directoryInfo.ToUri();

      Assert.AreEqual("file:///c:/temp/", uri.ToString());
    }

    [Test]
    public void FileSystemInfo_DirectoryWithTrailingSlash_ProducesUriWithTrailingSlash()
    {
      FileSystemInfo fsi = new DirectoryInfo(@"c:\temp\");

      Uri uri = fsi.ToUri();

      Assert.AreEqual("file:///c:/temp/", uri.ToString());
    }

    [Test]
    public void FileInfo_NormalFilename_ProducesUri()
    {
      FileInfo fileInfo = new FileInfo(@"c:\temp\test.txt");

      Uri uri = fileInfo.ToUri();

      Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
    }

    [Test]
    public void FileSystemInfo_FileInfo_ProducesUri()
    {
      FileSystemInfo fsi = new FileInfo(@"c:\temp\test.txt");

      Uri uri = fsi.ToUri();

      Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
    }
  }
}