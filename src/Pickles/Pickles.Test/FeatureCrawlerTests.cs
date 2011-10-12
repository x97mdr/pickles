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
    public class FeatureCrawlerTests
    {
        [Test]
        public void Can_crawl_directory_tree_for_features_successfully()
        {
            var rootPath = @"FakeFolderStructures\FeatureCrawlerTests";
            var features = new FeatureCrawler().Crawl(rootPath);

            Assert.NotNull(features);

            var a = features.ChildNodes[0].Data;
            Assert.NotNull(a);
            Assert.AreEqual("LevelOne.feature", a.Name);

            var b = features.ChildNodes[1].Data;
            Assert.NotNull(b);
            Assert.AreEqual("SubLevelOne", b.Name);

            var c = features.ChildNodes[1].ChildNodes[0].Data;
            Assert.NotNull(c);
            Assert.AreEqual("LevelOneSublevelOne.feature", c.Name);

            var d = features.ChildNodes[1].ChildNodes[1].Data;
            Assert.NotNull(d);
            Assert.AreEqual("LevelOneSublevelTwo.feature", d.Name);

            var e = features.ChildNodes[1].ChildNodes[2].ChildNodes[0].Data;
            Assert.NotNull(e);
            Assert.AreEqual("LevelOneSublevelOneSubLevelTwo.feature", e.Name);
        }
    }
}
