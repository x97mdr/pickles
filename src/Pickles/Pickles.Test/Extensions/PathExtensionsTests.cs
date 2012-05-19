using System;
using System.IO;
using NUnit.Framework;
using Pickles.Extensions;

namespace Pickles.Test.Extensions
{
    [TestFixture]
    class PathExtensionsTests
    {
        [Test]
        public void Get_A_Relative_Path_When_Location_Is_Deeper_Than_Root()
        {
            // Arrange
            string root = @"c:\test";
            string location = @"c:\test\blah.feature";

            string expected = @"test\blah.feature";
            
            // Act
            string actual = PathExtensions.MakeRelativePath(root, location);

            // Assert
            Assert.AreEqual(actual, expected, string.Format("Expected {0} got {1}", expected, actual));
        }

        [Test]
        public void Get_A_Relative_Path_When_Location_Is_Deeper_Than_Root_Even_When_Root_Contains_End_Slash()
        {
            string root = @"c:\test\";
            string location = @"c:\test\blah.feature";
            string expected = @"test\blah.feature";

            string actual = PathExtensions.MakeRelativePath(root, location);

            Assert.AreEqual(actual, expected, string.Format("Expected {0} got {1}", expected, actual));
        }

    }
}
