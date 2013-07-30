using System;
using System.IO.Abstractions.TestingHelpers;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;
using Should;

namespace PicklesDoc.Pickles.Test.Extensions
{
    [TestFixture]
    internal class PathExtensionsTests : BaseFixture
    {
        [Test]
        public void Get_A_Relative_Path_When_Location_Is_Deeper_Than_Root()
        {
            MockFileSystem fileSystem = MockFileSystem;
            fileSystem.AddFile(@"c:\test\deep\blah.feature", "Feature:"); // adding a file automatically adds all parent directories

            string actual = PathExtensions.MakeRelativePath(@"c:\test", @"c:\test\deep\blah.feature", fileSystem);

            actual.ShouldEqual(@"deep\blah.feature");
        }

        [Test]
        public void Get_A_Relative_Path_When_Location_Is_Deeper_Than_Root_Even_When_Root_Contains_End_Slash()
        {
            MockFileSystem fileSystem = MockFileSystem;
            fileSystem.AddFile(@"c:\test\deep\blah.feature", "Feature:"); // adding a file automatically adds all parent directories

            string actual = PathExtensions.MakeRelativePath(@"c:\test\", @"c:\test\deep\blah.feature", fileSystem);

            actual.ShouldEqual(@"deep\blah.feature");
        }
    }
}