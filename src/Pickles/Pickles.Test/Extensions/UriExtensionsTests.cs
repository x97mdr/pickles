using System;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.Test.Extensions
{
    [TestFixture]
    public class UriExtensionsTests : BaseFixture
    {
        [Test]
        public void ToFileUriCombined_ValidIntput_ValidOutput()
        {
            var info = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            Uri uri = info.ToFileUriCombined("test.txt", FileSystem);

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
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp\");

            Uri uri = directoryInfo.ToUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToUriDirectoryInfo_WithoutTrailingSlash_ProducesUriWithTrailingSlash()
        {
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            Uri uri = directoryInfo.ToUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToUriFileInfo_NormalFilename_ProducesUri()
        {
            var fileInfo = FileSystem.FileInfo.FromFileName(@"c:\temp\test.txt");

            Uri uri = fileInfo.ToUri();

            Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
        }

        [Test]
        public void ToUriFileSystemInfo_DirectoryWithTrailingSlash_ProducesUriWithTrailingSlash()
        {
            var fsi = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp\");

            Uri uri = fsi.ToUri();

            Assert.AreEqual("file:///c:/temp/", uri.ToString());
        }

        [Test]
        public void ToUriFileSystemInfo_FileInfo_ProducesUri()
        {
            var fsi = FileSystem.FileInfo.FromFileName(@"c:\temp\test.txt");

            Uri uri = fsi.ToUri();

            Assert.AreEqual("file:///c:/temp/test.txt", uri.ToString());
        }
    }
}