using System;
using System.IO;
using Moq;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.Parser;
using System.Xml.Linq;

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
                    new HtmlContentFormatter(null, null, null));

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
                        null,
                        null));

            Assert.AreEqual("htmlIndexFormatter", exception.ParamName);
        }

        [Test]
        public void Format_ContentIsFeatureNode_UsesHtmlFeatureFormatterWithCorrectArgument()
        {
            var fakeHtmlFeatureFormatter = new Mock<IHtmlFeatureFormatter>();
            var fakeHtmlImageRelocator = new Mock<HtmlImageRelocator>(null);
            fakeHtmlImageRelocator.Setup(x => x.Relocate(It.IsAny<INode>(), It.IsAny<XElement>()));
            var formatter = new HtmlContentFormatter(fakeHtmlFeatureFormatter.Object, Container.Resolve<HtmlIndexFormatter>(), fakeHtmlImageRelocator.Object);

            var featureNode = new FeatureNode(
                FileSystem.FileInfo.FromFileName(@"c:\temp\test.feature"),
                ".",
                new Feature());

            formatter.Format(featureNode, new INode[0]);

            fakeHtmlFeatureFormatter.Verify(f => f.Format(featureNode.Feature));
        }
    }
}