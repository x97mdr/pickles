using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Pickles.Formatters;
using NGenerics.DataStructures.Trees;

namespace Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlTableOfContentsFormatterTests
    {
        [Test]
        public void Can_crawl_directory_tree_for_features_successfully()
        {
            var rootPath = @"FakeFolderStructures\FeatureCrawlerTests";
            var features = new FeatureCrawler().Crawl(rootPath);
            var formatter = new HtmlTableOfContentsFormatter();
            var toc = formatter.Format(features.ChildNodes[0].Data.Url, features);

            Assert.NotNull(toc);

            //var a = toc.ChildNodes[0].Data;
            //Assert.NotNull(a);
            //Assert.AreEqual("LevelOne.feature", a.Name);

            //var b = toc.ChildNodes[1].Data;
            //Assert.NotNull(b);
            //Assert.AreEqual("SubLevelOne", b.Name);

            //var c = toc.ChildNodes[1].ChildNodes[0].Data;
            //Assert.NotNull(c);
            //Assert.AreEqual("LevelOneSublevelOne.feature", c.Name);

            //var d = toc.ChildNodes[1].ChildNodes[1].Data;
            //Assert.NotNull(d);
            //Assert.AreEqual("LevelOneSublevelTwo.feature", d.Name);

            //var e = toc.ChildNodes[1].ChildNodes[2].ChildNodes[0].Data;
            //Assert.NotNull(e);
            //Assert.AreEqual("LevelOneSublevelOneSubLevelTwo.feature", e.Name);
        }
    }
}
