using Ninject;
using NUnit.Framework;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.JSON;

namespace Pickles.Test.Formatters.JSON
{
    [TestFixture]
    public class when_formatting_features_to_JSON : BaseFixture
    {
        private const string ROOT_PATH = @"FakeFolderStructures\FeatureCrawlerTests";

        [TestFixtureSetUp]
        public void Setup()
        {
        }

        [Test]
        public void should_format_a_single_feature()
        {
            // Arrange
            

            // Act

            // Assert
            Assert.Inconclusive("Test to be written");
        }
    }
}
