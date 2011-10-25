using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingFeatureType : BaseFixture
    {
        [Test]
        public void Then_can_view_directory_node_successfully()
        {
            var directory = new DirectoryInfo(@"c:\directory");
            var node = new FeatureNode
            {
                Location = directory,
                Url = new Uri(directory.FullName),
            };

            Assert.AreEqual(FeatureNodeType.Directory, node.Type);
        }

        [Test]
        public void Then_can_view_feature_node_successfully()
        {
            var file = new FileInfo(@"c:\directory\my.feature");
            var node = new FeatureNode
            {
                Location = file,
                Url = new Uri(file.FullName),
            };

            Assert.AreEqual(FeatureNodeType.Feature, node.Type);
        }

        [Test]
        public void Then_can_view_unknown_node_successfully()
        {
            var file = new FileInfo(@"c:\directory\my.txt");
            var node = new FeatureNode
            {
                Location = file,
                Url = new Uri(file.FullName),
            };

            Assert.AreEqual(FeatureNodeType.Unknown, node.Type);
        }
    }
}
