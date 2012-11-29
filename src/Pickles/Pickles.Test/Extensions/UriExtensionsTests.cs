using System;
using System.IO;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.Test.Extensions
{
    [TestFixture]
    public class UriExtensionsTests
    {
        [Test]
        public void ToFileUriCombined_ValidIntput_ValidOutput()
        {
            var info = new DirectoryInfo(@"c:\temp");

            Uri uri = info.ToFileUriCombined("test.txt");

            Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
        }

        [Test]
        public void ToFileUriString_WithoutTrailingSlash_ValidOutputWithTrailingSlash()
        {
            Uri uri = @"c:\temp\test.txt".ToFileUri();

            Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
        }

        [Test]
        public void ToFolderUriString_WithTrailingSlash_ValidOutput()
        {
            Uri uri = @"c:\temp\".ToFolderUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToFolderUriString_WithoutTrailingSlash_ValidOutputWithTrailingSlash()
        {
            Uri uri = @"c:\temp".ToFolderUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToUriDirectoryInfo_WithTrailingSlash_ProducesUriWithTrailingSlash()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp\");

            Uri uri = directoryInfo.ToUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToUriDirectoryInfo_WithoutTrailingSlash_ProducesUriWithTrailingSlash()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp");

            Uri uri = directoryInfo.ToUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToUriFileInfo_NormalFilename_ProducesUri()
        {
            var fileInfo = new FileInfo(@"c:\temp\test.txt");

            Uri uri = fileInfo.ToUri();

            Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
        }

        [Test]
        public void ToUriFileSystemInfo_DirectoryWithTrailingSlash_ProducesUriWithTrailingSlash()
        {
            FileSystemInfo fsi = new DirectoryInfo(@"c:\temp\");

            Uri uri = fsi.ToUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToUriFileSystemInfo_FileInfo_ProducesUri()
        {
            FileSystemInfo fsi = new FileInfo(@"c:\temp\test.txt");

            Uri uri = fsi.ToUri();

            Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
        }
    }
}