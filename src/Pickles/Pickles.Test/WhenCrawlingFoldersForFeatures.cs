using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Moq;
using NGenerics.DataStructures.Trees;
using Ninject;
using NUnit.Framework;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenCrawlingFoldersForFeatures : BaseFixture
    {
        [Test]
        public void Then_can_crawl_all_folders_including_subfolders_for_features_successfully()
        {
            var rootPath = @"FakeFolderStructures\FeatureCrawlerTests";
            var features = Kernel.Get<FeatureCrawler>().Crawl(rootPath);

            Assert.NotNull(features);

            var a = features.ChildNodes[0].Data;
            Assert.NotNull(a);
            Assert.AreEqual("LevelOne", a.Name);
            Assert.AreEqual(@"LevelOne.feature", a.RelativePathFromRoot);

            var b = features.ChildNodes[1].Data;
            Assert.NotNull(b);
            Assert.AreEqual("SubLevelOne", b.Name);
            Assert.AreEqual(@"SubLevelOne\", b.RelativePathFromRoot);

            var subLevelOne = features.ChildNodes[1];
            Assert.AreEqual(3, subLevelOne.ChildNodes.Count);

            var levelOneSublevelOneFeature = subLevelOne.ChildNodes[0].Data;
            Assert.NotNull(levelOneSublevelOneFeature);
            Assert.AreEqual("LevelOneSublevelOne", levelOneSublevelOneFeature.Name);
            Assert.AreEqual(@"SubLevelOne\LevelOneSublevelOne.feature", levelOneSublevelOneFeature.RelativePathFromRoot);

            var levelOneSublevelTwoFeature = subLevelOne.ChildNodes[1].Data;
            Assert.NotNull(levelOneSublevelTwoFeature);
            Assert.AreEqual("LevelOneSublevelTwo", levelOneSublevelTwoFeature.Name);
            Assert.AreEqual(@"SubLevelOne\LevelOneSublevelTwo.feature", levelOneSublevelTwoFeature.RelativePathFromRoot);

            var subLevelTwo = subLevelOne.ChildNodes[2];
            Assert.AreEqual(1, subLevelTwo.ChildNodes.Count);

            var subLevelTwoNode = subLevelOne.ChildNodes[2].Data;
            Assert.NotNull(subLevelTwoNode);
            Assert.AreEqual("SubLevelTwo", subLevelTwoNode.Name);
            Assert.AreEqual(@"SubLevelOne\SubLevelTwo\", subLevelTwoNode.RelativePathFromRoot);

            var levelOneSublevelOneSubLevelTwo = subLevelOne.ChildNodes[2].ChildNodes[0].Data;
            Assert.NotNull(levelOneSublevelOneSubLevelTwo);
            Assert.AreEqual("LevelOneSublevelOneSubLevelTwo", levelOneSublevelOneSubLevelTwo.Name);
            Assert.AreEqual(@"SubLevelOne\SubLevelTwo\LevelOneSublevelOneSubLevelTwo.feature", levelOneSublevelOneSubLevelTwo.RelativePathFromRoot);
        }
    }
}
