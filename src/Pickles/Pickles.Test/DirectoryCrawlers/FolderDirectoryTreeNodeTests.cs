using System;
using NFluent;
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
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp\");

            var node = new FolderNode(directoryInfo, "");

            Check.That(node.OriginalLocation.FullName).IsEqualTo(@"c:\temp");
        }

        [Test]
        public void Constructor_ValidFileSystemInfo_SetsOriginalLocationUrl()
        {
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "");

            Check.That(node.OriginalLocationUrl.ToString()).IsEqualTo(@"file:///c:/temp/");
        }

        [Test]
        public void Constructor_ValidRelativePath()
        {
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            Check.That(node.RelativePathFromRoot).IsEqualTo(@"../");
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToChildDirectory_ReturnsRelativePath()
        {
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/child/"));

            Check.That(relative).IsEqualTo("../");
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToFileBelow_ReturnsCurrentDirectory()
        {
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/test2.html"));

            Check.That(relative).IsEqualTo("./");
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToFileOutside_ReturnsRelativePath()
        {
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp2/test2.html"));

            Check.That(relative).IsEqualTo("../temp/");
        }

        [Test]
        public void GetRelativeUriTo_DirectoryToParentDirectory_ReturnsRelativePath()
        {
            var directoryInfo = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\temp\child");

            var node = new FolderNode(directoryInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/"));

            Check.That(relative).IsEqualTo("child/");
        }

        [Test]
        public void GetRelativeUriTo_FileToDirectory_ReturnsNodesFileName()
        {
            var fileInfo = FileSystem.FileInfo.FromFileName(@"c:\temp\test1.html");

            var node = new FolderNode(fileInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/"));

            Check.That(relative).IsEqualTo("test1.html");
        }

        [Test]
        public void GetRelativeUriTo_FileToFile_ReturnsNodesFileName()
        {
            var fileInfo = FileSystem.FileInfo.FromFileName(@"c:\temp\test1.html");

            var node = new FolderNode(fileInfo, "../");

            string relative = node.GetRelativeUriTo(new Uri(@"file:///c:/temp/test2.html"));

            Check.That(relative).IsEqualTo("test1.html");
        }

        [Test]
        public void RealData()
        {
            var originalLocation =
                FileSystem.DirectoryInfo.FromDirectoryName(
                    @"C:\tfs\Dev.CAX\src\CAX_Main\src\net\Projects\Aim.Gain.GoldenCopy.FunctionalTesting\CAX\DistributionOfRights");

            var node = new FolderNode(originalLocation, "");

            var other =
                new Uri(
                    "file:///C:/tfs/Dev.CAX/src/CAX_Main/src/net/Projects/Aim.Gain.GoldenCopy.FunctionalTesting/CAX/");

            string relative = node.GetRelativeUriTo(other);

            Check.That(relative).IsEqualTo("DistributionOfRights/");
        }
    }
}