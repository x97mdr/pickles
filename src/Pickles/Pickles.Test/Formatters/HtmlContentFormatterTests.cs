using System;
using Moq;
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlContentFormatterTests : BaseFixture
    {
        [Test]
        public void Constructor_NullHtmlFeatureFormatter_ThrowsArgumentNullException()
        {
          Check.ThatCode(() => new HtmlContentFormatter(null, null))
            .Throws<ArgumentNullException>()
            .WithProperty("ParamName", "htmlFeatureFormatter");
        }

        [Test]
        public void Constructor_NullHtmlIndexFormatter_ThrowsArgumentNullException()
        {
          Check.ThatCode(() => new HtmlContentFormatter(new Mock<IHtmlFeatureFormatter>().Object, null))
            .Throws<ArgumentNullException>()
            .WithProperty("ParamName", "htmlIndexFormatter");
        }

        [Test]
        public void Format_ContentIsFeatureNode_UsesHtmlFeatureFormatterWithCorrectArgument()
        {
            var fakeHtmlFeatureFormatter = new Mock<IHtmlFeatureFormatter>();
            var formatter = new HtmlContentFormatter(fakeHtmlFeatureFormatter.Object, new HtmlIndexFormatter());

            var featureNode = new FeatureNode(
                FileSystem.FileInfo.FromFileName(@"c:\temp\test.feature"),
                ".",
                new Feature());

            formatter.Format(featureNode, new INode[0]);

            fakeHtmlFeatureFormatter.Verify(f => f.Format(featureNode.Feature));
        }
    }
}