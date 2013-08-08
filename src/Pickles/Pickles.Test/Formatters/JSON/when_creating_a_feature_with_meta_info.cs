using System;
using System.IO.Abstractions;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.JSON;
using PicklesDoc.Pickles.Parser;
using Should.Fluent;

namespace PicklesDoc.Pickles.Test.Formatters.JSON
{
    public class when_creating_a_feature_with_meta_info : BaseFixture
    {
        private const string RELATIVE_PATH = @"AcceptanceTest";
        private const string ROOT_PATH = @"FakeFolderStructures\AcceptanceTest";
        private const string FEATURE_PATH = @"AdvancedFeature.feature";

        private FeatureNode _featureDirectoryNode;
        private FileInfoBase _featureFileInfo;
        private FeatureWithMetaInfo _featureWithMeta;
        private Feature _testFeature;

        [TestFixtureSetUp]
        public void Setup()
        {
            this._testFeature = new Feature { Name = "Test" };
            this._featureFileInfo = this.RealFileSystem.FileInfo.FromFileName(MockFileSystem.Path.Combine(ROOT_PATH, FEATURE_PATH));
            this._featureDirectoryNode = new FeatureNode(this._featureFileInfo, RELATIVE_PATH, this._testFeature);

            this._featureWithMeta = new FeatureWithMetaInfo(this._featureDirectoryNode);
        }


        [Test]
        public void it_should_contain_the_feature()
        {
            this._featureWithMeta.Feature.Should().Not.Be.Null();
            this._featureWithMeta.Feature.Name.Should().Equal("Test");
        }

        [Test]
        public void it_should_contain_the_relative_path()
        {
            this._featureWithMeta.RelativeFolder.Should().Equal(RELATIVE_PATH);
        }
    }
}