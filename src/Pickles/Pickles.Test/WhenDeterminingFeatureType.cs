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
        public void Then_can_view_markdown_node_with_markdown_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.markdown") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_mdown_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.mdown") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_mkdn_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.mkdn") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_md_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.md") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_mdwn_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.mdwn") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_mdtxt_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.mdtxt") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_mdtext_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.mdtext") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_text_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.text") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_markdown_node_with_txt_extension_successfully()
        {
            var node = new FeatureNode { Location = new FileInfo(@"c:\directory\my.txt") };
            Assert.AreEqual(FeatureNodeType.Markdown, node.Type);
        }

        [Test]
        public void Then_can_view_unknown_node_successfully()
        {
            var file = new FileInfo(@"c:\directory\my.pdf");
            var node = new FeatureNode
            {
                Location = file,
                Url = new Uri(file.FullName),
            };

            Assert.AreEqual(FeatureNodeType.Unknown, node.Type);
        }
    }
}
