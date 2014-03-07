using System;
using System.Xml.Linq;
using Autofac;
using Moq;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlContentFormatterTests : BaseFixture
    {
        [Test]
        public void Constructor_NullHtmlFeatureFormatter_ThrowsArgumentNullException()
        {
            var exception =
                Assert.Throws<ArgumentNullException>(
                    () =>
                    new HtmlContentFormatter(null, null));

            Assert.AreEqual("htmlFeatureFormatter", exception.ParamName);
        }

        [Test]
        public void Constructor_NullHtmlIndexFormatter_ThrowsArgumentNullException()
        {
            var exception =
                Assert.Throws<ArgumentNullException>(
                    () =>
                    new HtmlContentFormatter(
                        Container.Resolve<HtmlFeatureFormatter>(),
                        null));

            Assert.AreEqual("htmlIndexFormatter", exception.ParamName);
        }

        [Test]
        public void Format_ContentIsFeatureNode_UsesHtmlFeatureFormatterWithCorrectArgument()
        {
            var fakeHtmlFeatureFormatter = new Mock<IHtmlFeatureFormatter>();
            var formatter = new HtmlContentFormatter(fakeHtmlFeatureFormatter.Object, Container.Resolve<HtmlIndexFormatter>());

            var featureNode = new FeatureNode(
                MockFileSystem.FileInfo.FromFileName(@"c:\temp\test.feature"),
                ".",
                new Feature());

            formatter.Format(featureNode, new INode[0]);

            fakeHtmlFeatureFormatter.Verify(f => f.Format(featureNode.Feature));
        }
    }
}