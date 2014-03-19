using System;
using NGenerics.DataStructures.Trees;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenCrawlingFoldersForFeatures : BaseFixture
    {
        [Test]
        public void Then_can_crawl_all_folders_including_subfolders_for_features_successfully()
        {
            AddFakeFolderStructures();

            string rootPath = FileSystemPrefix + @"FeatureCrawlerTests";
            GeneralTree<INode> features = Container.Resolve<DirectoryTreeCrawler>().Crawl(rootPath);

            Assert.NotNull(features);

            INode indexMd = features.ChildNodes[0].Data;
            indexMd.ShouldNotBeNull();
            indexMd.Name.ShouldEqual("This is an index written in Markdown");
            indexMd.RelativePathFromRoot.ShouldEqual("index.md");
            indexMd.ShouldBeType<MarkdownNode>();

            INode levelOneFeature = features.ChildNodes[1].Data;
            levelOneFeature.ShouldNotBeNull();
            levelOneFeature.Name.ShouldEqual("Addition");
            levelOneFeature.RelativePathFromRoot.ShouldEqual("LevelOne.feature");
            levelOneFeature.ShouldBeType<FeatureNode>();

            INode subLevelOneDirectory = features.ChildNodes[2].Data;
            subLevelOneDirectory.ShouldNotBeNull();
            subLevelOneDirectory.Name.ShouldEqual("Sub Level One");
            subLevelOneDirectory.RelativePathFromRoot.ShouldEqual(@"SubLevelOne\");
            subLevelOneDirectory.ShouldBeType<FolderNode>();

            GeneralTree<INode> subLevelOneNode = features.ChildNodes[2];
            subLevelOneNode.ChildNodes.Count.ShouldEqual(3);

            INode levelOneSublevelOneFeature = subLevelOneNode.ChildNodes[0].Data;
            levelOneSublevelOneFeature.ShouldNotBeNull();
            levelOneSublevelOneFeature.Name.ShouldEqual("Addition");
            levelOneSublevelOneFeature.RelativePathFromRoot.ShouldEqual(@"SubLevelOne\LevelOneSublevelOne.feature");
            levelOneSublevelOneFeature.ShouldBeType<FeatureNode>();

            INode levelOneSublevelTwoFeature = subLevelOneNode.ChildNodes[1].Data;
            levelOneSublevelTwoFeature.ShouldNotBeNull();
            levelOneSublevelTwoFeature.Name.ShouldEqual("Addition");
            levelOneSublevelTwoFeature.RelativePathFromRoot.ShouldEqual(@"SubLevelOne\LevelOneSublevelTwo.feature");
            levelOneSublevelTwoFeature.ShouldBeType<FeatureNode>();

            GeneralTree<INode> subLevelTwoNode = subLevelOneNode.ChildNodes[2];
            subLevelTwoNode.ChildNodes.Count.ShouldEqual(1);

            INode subLevelTwoDirectory = subLevelOneNode.ChildNodes[2].Data;
            subLevelTwoDirectory.ShouldNotBeNull();
            subLevelTwoDirectory.Name.ShouldEqual("Sub Level Two");
            subLevelTwoDirectory.RelativePathFromRoot.ShouldEqual(@"SubLevelOne\SubLevelTwo\");
            subLevelTwoDirectory.ShouldBeType<FolderNode>();

            INode levelOneSublevelOneSubLevelTwoDirectory = subLevelOneNode.ChildNodes[2].ChildNodes[0].Data;
            levelOneSublevelOneSubLevelTwoDirectory.ShouldNotBeNull();
            levelOneSublevelOneSubLevelTwoDirectory.Name.ShouldEqual("Addition");
            levelOneSublevelOneSubLevelTwoDirectory.RelativePathFromRoot.ShouldEqual(@"SubLevelOne\SubLevelTwo\LevelOneSublevelOneSubLevelTwo.feature");
            levelOneSublevelOneSubLevelTwoDirectory.ShouldBeType<FeatureNode>();
        }
    }
}