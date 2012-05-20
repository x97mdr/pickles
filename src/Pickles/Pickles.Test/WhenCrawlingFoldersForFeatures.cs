using NGenerics.DataStructures.Trees;
using NUnit.Framework;
using Ninject;
using Pickles.DirectoryCrawler;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenCrawlingFoldersForFeatures : BaseFixture
    {
        [Test]
        public void Then_can_crawl_all_folders_including_subfolders_for_features_successfully()
        {
            string rootPath = @"FakeFolderStructures\FeatureCrawlerTests";
            GeneralTree<IDirectoryTreeNode> features = Kernel.Get<DirectoryTreeCrawler>().Crawl(rootPath);

            Assert.NotNull(features);

            IDirectoryTreeNode indexMd = features.ChildNodes[0].Data;
            Assert.NotNull(indexMd);
            Assert.AreEqual("This is an index written in Markdown", indexMd.Name);
            Assert.AreEqual(@"index.md", indexMd.RelativePathFromRoot);
            Assert.IsInstanceOf<MarkdownTreeNode>(indexMd);

            IDirectoryTreeNode levelOneFeature = features.ChildNodes[1].Data;
            Assert.NotNull(levelOneFeature);
            Assert.AreEqual("Addition", levelOneFeature.Name);
            Assert.AreEqual(@"LevelOne.feature", levelOneFeature.RelativePathFromRoot);
            Assert.IsInstanceOf<FeatureDirectoryTreeNode>(levelOneFeature);

            IDirectoryTreeNode subLevelOneDirectory = features.ChildNodes[2].Data;
            Assert.NotNull(subLevelOneDirectory);
            Assert.AreEqual("Sub Level One", subLevelOneDirectory.Name);
            Assert.AreEqual(@"SubLevelOne\", subLevelOneDirectory.RelativePathFromRoot);
            Assert.IsInstanceOf<FolderDirectoryTreeNode>(subLevelOneDirectory);

            GeneralTree<IDirectoryTreeNode> subLevelOneNode = features.ChildNodes[2];
            Assert.AreEqual(3, subLevelOneNode.ChildNodes.Count);

            IDirectoryTreeNode levelOneSublevelOneFeature = subLevelOneNode.ChildNodes[0].Data;
            Assert.NotNull(levelOneSublevelOneFeature);
            Assert.AreEqual("Addition", levelOneSublevelOneFeature.Name);
            Assert.AreEqual(@"SubLevelOne\LevelOneSublevelOne.feature", levelOneSublevelOneFeature.RelativePathFromRoot);
            Assert.IsInstanceOf<FeatureDirectoryTreeNode>(levelOneSublevelOneFeature);

            IDirectoryTreeNode levelOneSublevelTwoFeature = subLevelOneNode.ChildNodes[1].Data;
            Assert.NotNull(levelOneSublevelTwoFeature);
            Assert.AreEqual("Addition", levelOneSublevelTwoFeature.Name);
            Assert.AreEqual(@"SubLevelOne\LevelOneSublevelTwo.feature", levelOneSublevelTwoFeature.RelativePathFromRoot);
            Assert.IsInstanceOf<FeatureDirectoryTreeNode>(levelOneSublevelTwoFeature);

            GeneralTree<IDirectoryTreeNode> subLevelTwoNode = subLevelOneNode.ChildNodes[2];
            Assert.AreEqual(1, subLevelTwoNode.ChildNodes.Count);

            IDirectoryTreeNode subLevelTwoDirectory = subLevelOneNode.ChildNodes[2].Data;
            Assert.NotNull(subLevelTwoDirectory);
            Assert.AreEqual("Sub Level Two", subLevelTwoDirectory.Name);
            Assert.AreEqual(@"SubLevelOne\SubLevelTwo\", subLevelTwoDirectory.RelativePathFromRoot);
            Assert.IsInstanceOf<FolderDirectoryTreeNode>(subLevelTwoDirectory);

            IDirectoryTreeNode levelOneSublevelOneSubLevelTwoDirectory =
                subLevelOneNode.ChildNodes[2].ChildNodes[0].Data;
            Assert.NotNull(levelOneSublevelOneSubLevelTwoDirectory);
            Assert.AreEqual("Addition", levelOneSublevelOneSubLevelTwoDirectory.Name);
            Assert.AreEqual(@"SubLevelOne\SubLevelTwo\LevelOneSublevelOneSubLevelTwo.feature",
                            levelOneSublevelOneSubLevelTwoDirectory.RelativePathFromRoot);
            Assert.IsInstanceOf<FeatureDirectoryTreeNode>(levelOneSublevelOneSubLevelTwoDirectory);
        }
    }
}