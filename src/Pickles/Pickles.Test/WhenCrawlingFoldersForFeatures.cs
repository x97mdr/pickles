using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using System.IO;
using NGenerics.DataStructures.Trees;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenCrawlingFoldersForFeatures
    {
        [Test]
        public void Then_can_crawl_all_folders_including_subfolders_for_features_successfully()
        {
            var rootPath = @"FakeFolderStructures\FeatureCrawlerTests";
            var features = new FeatureCrawler(new FeatureParser()).Crawl(rootPath);

            Assert.NotNull(features);

            var a = features.ChildNodes[0].Data;
            Assert.NotNull(a);
            Assert.AreEqual("LevelOne", a.Name);
            Assert.AreEqual(@"LevelOne.feature", a.RelativePathFromRoot);

            var b = features.ChildNodes[1].Data;
            Assert.NotNull(b);
            Assert.AreEqual("SubLevelOne", b.Name);
            Assert.AreEqual(@"SubLevelOne\", b.RelativePathFromRoot);

            var c = features.ChildNodes[1].ChildNodes[0].Data;
            Assert.NotNull(c);
            Assert.AreEqual("LevelOneSublevelOne", c.Name);
            Assert.AreEqual(@"SubLevelOne\LevelOneSublevelOne.feature", c.RelativePathFromRoot);

            var d = features.ChildNodes[1].ChildNodes[1].Data;
            Assert.NotNull(d);
            Assert.AreEqual("LevelOneSublevelTwo", d.Name);
            Assert.AreEqual(@"SubLevelOne\LevelOneSublevelTwo.feature", d.RelativePathFromRoot);

            var e = features.ChildNodes[1].ChildNodes[2].Data;
            Assert.NotNull(e);
            Assert.AreEqual("SubLevelTwo", e.Name);
            Assert.AreEqual(@"SubLevelOne\SubLevelTwo\", e.RelativePathFromRoot);

            var f = features.ChildNodes[1].ChildNodes[2].ChildNodes[0].Data;
            Assert.NotNull(f);
            Assert.AreEqual("LevelOneSublevelOneSubLevelTwo", f.Name);
            Assert.AreEqual(@"SubLevelOne\SubLevelTwo\LevelOneSublevelOneSubLevelTwo.feature", f.RelativePathFromRoot);
        }
    }
}
