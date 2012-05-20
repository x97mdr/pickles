using System.IO;
using NUnit.Framework;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.JSON;
using Pickles.Parser;
using Should.Fluent;

namespace Pickles.Test.Formatters.JSON
{
    public class when_creating_a_feature_with_meta_info
    {
        private const string RELATIVE_PATH = @"AcceptanceTest";
        private const string ROOT_PATH = @"FakeFolderStructures\AcceptanceTest";
        private const string FEATURE_PATH = @"AdvancedFeature.feature";

        private FeatureDirectoryTreeNode _featureDirectoryNode;
        private FileInfo _featureFileInfo;
        private FeatureWithMetaInfo _featureWithMeta;
        private Feature _testFeature;

        [TestFixtureSetUp]
        public void Setup()
        {
            _testFeature = new Feature {Name = "Test"};
            _featureFileInfo = new FileInfo(Path.Combine(ROOT_PATH, FEATURE_PATH));
            _featureDirectoryNode = new FeatureDirectoryTreeNode(_featureFileInfo, RELATIVE_PATH, _testFeature);

            _featureWithMeta = new FeatureWithMetaInfo(_featureDirectoryNode);
        }


        [Test]
        public void it_should_contain_the_feature()
        {
            _featureWithMeta.Feature.Should().Not.Be.Null();
            _featureWithMeta.Feature.Name.Should().Equal("Test");
        }

        [Test]
        public void it_should_contain_the_relative_path()
        {
            _featureWithMeta.RelativeFolder.Should().Equal(RELATIVE_PATH);
        }
    }
}