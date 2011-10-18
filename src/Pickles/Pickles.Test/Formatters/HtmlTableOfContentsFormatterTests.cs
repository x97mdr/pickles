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
            var features = new FeatureCrawler(new FeatureParser()).Crawl(rootPath);

            var formatter = new HtmlTableOfContentsFormatter();
            var toc = formatter.Format(features.ChildNodes[0].Data.Url, features);

            Assert.NotNull(toc);

            // Assert that the first feature is appropriately set in the TOC
            var ul = toc.Elements().First();
            Assert.NotNull(ul);
            Assert.AreEqual(true, ul.HasElements);

            var li1 = ul.Elements().First();
            Assert.NotNull(li1);

            var li1a = li1.Elements().First();
            Assert.AreEqual(true, li1a.HasAttributes);
            Assert.AreEqual("#", li1a.Attribute("href").Value);
            Assert.AreEqual("LevelOne", li1a.Value);

            // Assert that a directory is appropriately set in the TOC
            var ul2 = ul.Elements().ElementAt(1);
            Assert.AreEqual(true, ul2.HasElements);

            // Assert that a feature file is appropriately set deeper down in the TOC
            var li2 = ul2.Elements().First();
            Assert.NotNull(li2);

            var li2a = li2.Elements().First();
            Assert.AreEqual(true, li2a.HasAttributes);
            Assert.AreEqual("SubLevelOne/LevelOneSublevelOne.xhtml", li2a.Attribute("href").Value);
            Assert.AreEqual("LevelOneSublevelOne", li2a.Value);
        }
    }
}
