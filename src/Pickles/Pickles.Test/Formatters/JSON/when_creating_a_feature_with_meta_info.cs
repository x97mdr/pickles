using System;
using System.IO.Abstractions;
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.JSON;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Test.Formatters.JSON
{
    public class when_creating_a_feature_with_meta_info : BaseFixture
    {
        private const string RELATIVE_PATH = @"AcceptanceTest";
        private const string ROOT_PATH = FileSystemPrefix + @"AcceptanceTest";
        private const string FEATURE_PATH = @"AdvancedFeature.feature";

        private FeatureNode _featureDirectoryNode;
        private FileInfoBase _featureFileInfo;
        private JsonFeatureWithMetaInfo _featureWithMeta;
        private Feature _testFeature;

        public void Setup()
        {
            this._testFeature = new Feature { Name = "Test" };
            this._featureFileInfo = this.FileSystem.FileInfo.FromFileName(FileSystem.Path.Combine(ROOT_PATH, FEATURE_PATH));
            this._featureDirectoryNode = new FeatureNode(this._featureFileInfo, RELATIVE_PATH, this._testFeature);

            this._featureWithMeta = new JsonFeatureWithMetaInfo(this._featureDirectoryNode);
        }

        [Test]
        public void it_should_contain_the_feature()
        {
            this.Setup();

            Check.That(this._featureWithMeta.Feature).IsNotNull();
            Check.That(this._featureWithMeta.Feature.Name).IsEqualTo("Test");
        }

        [Test]
        public void it_should_contain_the_relative_path()
        {
            this.Setup();

            Check.That(this._featureWithMeta.RelativeFolder).IsEqualTo(RELATIVE_PATH);
        }
    }
}