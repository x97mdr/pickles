using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Ninject;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;

namespace Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlContentFormatterTests : BaseFixture
    {
        [Test]
        public void Constructor_NullHtmlFeatureFormatter_ThrowsArgumentNullException()
        {
            ArgumentNullException exception =
                Assert.Throws<ArgumentNullException>(
                    () =>
                    new HtmlContentFormatter(null, null));

            Assert.AreEqual("htmlFeatureFormatter", exception.ParamName);
        }

        [Test]
        public void Constructor_NullHtmlIndexFormatter_ThrowsArgumentNullException()
        {
            ArgumentNullException exception =
                Assert.Throws<ArgumentNullException>(
                    () =>
                    new HtmlContentFormatter(
                        Kernel.Get<HtmlFeatureFormatter>(),
                        null));

            Assert.AreEqual("htmlIndexFormatter", exception.ParamName);
        }

        [Test]
        public void Format_ContentIsFeatureNode_UsesHtmlFeatureFormatterWithCorrectArgument()
        {
            var fakeHtmlFeatureFormatter = new Mock<IHtmlFeatureFormatter>();
            var formatter = new HtmlContentFormatter(fakeHtmlFeatureFormatter.Object, Kernel.Get<HtmlIndexFormatter>());

            var featureNode = new FeatureDirectoryTreeNode(
                new FileInfo(@"c:\temp\test.feature"),
                    ".",
                    new Feature());

            formatter.Format(featureNode, new IDirectoryTreeNode[0]);

            fakeHtmlFeatureFormatter.Verify(f => f.Format(featureNode.Feature));
        }
    }
}