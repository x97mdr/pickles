using System;
using System.IO;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.Test.DirectoryCrawlers
{
    [TestFixture]
    public class FolderDirectoryTreeNodeTests : BaseFixture
    {
        [Test]
        public void Constructor_ValidFileSystemInfo_SetsOriginalLocation()
        {
            var directoryInfo = RealFileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "");

            Assert.AreEqual(@"c:\temp", node.OriginalLocation.FullName);
        }

        [Test]
        public void Constructor_ValidFileSystemInfo_SetsOriginalLocationUrl()
        {
            var directoryInfo = RealFileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "");

            Assert.AreEqual(@"file:///c:/temp/", node.OriginalLocationUrl.ToString());
        }

        [Test]
        public void Constructor_ValidRelativePath()
        {
            var directoryInfo = RealFileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            Assert.AreEqual(@"../", node.RelativePathFromRoot);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToChildDirectory_ReturnsRelativePath()
        {
            var directoryInfo = RealFileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/child/"));

            Assert.AreEqual("../", relative);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToFileBelow_ReturnsEmpty()
        {
            var directoryInfo = RealFileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/test2.html"));

            Assert.AreEqual("", relative);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToFileOutside_ReturnsRelativePath()
        {
            var directoryInfo = RealFileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp2/test2.html"));

            Assert.AreEqual("../temp/", relative);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToParentDirectory_ReturnsRelativePath()
        {
            var directoryInfo = RealFileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp\child");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/"));

            Assert.AreEqual("child/", relative);
        }

        [Test]
        public void GetRelativeUriTo_FileToDirectory_ReturnsNodesFileName()
        {
            var fileInfo = RealFileSystem.FileInfo.FromFileName(@"c:\temp\test1.html");

            var node = new FolderNode(fileInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/"));

            Assert.AreEqual("test1.html", relative);
        }

        [Test]
        public void GetRelativeUriTo_FileToFile_ReturnsNodesFileName()
        {
            var fileInfo = RealFileSystem.FileInfo.FromFileName(@"c:\temp\test1.html");

            var node = new FolderNode(fileInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/test2.html"));

            Assert.AreEqual("test1.html", relative);
        }

        [Test]
        public void RealData()
        {
            var originalLocation =
                RealFileSystem.DirectoryInfo.FromDirectoryName(
                    @"C:\tfs\Dev.CAX\src\CAX_Main\src\net\Projects\Aim.Gain.GoldenCopy.FunctionalTesting\CAX\DistributionOfRights");

            var node = new FolderNode(originalLocation, "");

            var other =
                new Uri(
                    "file:///C:/tfs/Dev.CAX/src/CAX_Main/src/net/Projects/Aim.Gain.GoldenCopy.FunctionalTesting/CAX/");

            string relative = node.GetRelativeUriTo(other);

            Assert.AreEqual("DistributionOfRights/", relative);
        }
    }
}