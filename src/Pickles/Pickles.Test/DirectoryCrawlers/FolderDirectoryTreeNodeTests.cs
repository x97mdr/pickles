//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="FolderDirectoryTreeNodeTests.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

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