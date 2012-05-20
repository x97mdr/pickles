using System;
using System.IO;
using NUnit.Framework;
using Pickles.DirectoryCrawler;

namespace Pickles.Test.DirectoryCrawlers
{
    [TestFixture]
    public class FolderDirectoryTreeNodeTests
    {
        [Test]
        public void Constructor_ValidFileSystemInfo_SetsOriginalLocation()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp");

            var node = new FolderDirectoryTreeNode(directoryInfo, "");

            Assert.AreEqual(@"c:\temp", node.OriginalLocation.FullName);
        }

        [Test]
        public void Constructor_ValidFileSystemInfo_SetsOriginalLocationUrl()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp");

            var node = new FolderDirectoryTreeNode(directoryInfo, "");

            Assert.AreEqual(@"file:///c:/temp/", node.OriginalLocationUrl.ToString());
        }

        [Test]
        public void Constructor_ValidRelativePath()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp");

            var node = new FolderDirectoryTreeNode(directoryInfo, "../");

            Assert.AreEqual(@"../", node.RelativePathFromRoot);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToChildDirectory_ReturnsRelativePath()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp");

            var node = new FolderDirectoryTreeNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/child/"));

            Assert.AreEqual("../", relative);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToFileBelow_ReturnsEmpty()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp");

            var node = new FolderDirectoryTreeNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/test2.html"));

            Assert.AreEqual("", relative);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToFileOutside_ReturnsRelativePath()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp");

            var node = new FolderDirectoryTreeNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp2/test2.html"));

            Assert.AreEqual("../temp/", relative);
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToParentDirectory_ReturnsRelativePath()
        {
            var directoryInfo = new DirectoryInfo(@"c:\temp\child");

            var node = new FolderDirectoryTreeNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/"));

            Assert.AreEqual("child/", relative);
        }

        [Test]
        public void GetRelativeUriTo_FileToDirectory_ReturnsNodesFileName()
        {
            var fileInfo = new FileInfo(@"c:\temp\test1.html");

            var node = new FolderDirectoryTreeNode(fileInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/"));

            Assert.AreEqual("test1.html", relative);
        }

        [Test]
        public void GetRelativeUriTo_FileToFile_ReturnsNodesFileName()
        {
            var fileInfo = new FileInfo(@"c:\temp\test1.html");

            var node = new FolderDirectoryTreeNode(fileInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/test2.html"));

            Assert.AreEqual("test1.html", relative);
        }

        [Test]
        public void RealData()
        {
            var originalLocation =
                new DirectoryInfo(
                    @"C:\tfs\Dev.CAX\src\CAX_Main\src\net\Projects\Aim.Gain.GoldenCopy.FunctionalTesting\CAX\DistributionOfRights");

            var node = new FolderDirectoryTreeNode(originalLocation, "");

            var other =
                new Uri(
                    "file:///C:/tfs/Dev.CAX/src/CAX_Main/src/net/Projects/Aim.Gain.GoldenCopy.FunctionalTesting/CAX/");

            string relative = node.GetRelativeUriTo(other);

            Assert.AreEqual("DistributionOfRights/", relative);
        }
    }
}