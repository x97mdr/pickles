using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using Ninject;
using NUnit.Framework;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Test.Helpers;

namespace Pickles.Test.Formatters
{
    [TestFixture]
    public class table_of_contents_should_be_created_from_a_folder_structure : BaseFixture
    {
        private const string ROOT_PATH = @"FakeFolderStructures\FeatureCrawlerTests";
        private XElement _toc;

        [TestFixtureSetUp]
        public void Setup()
        {
            var features = Kernel.Get<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

            var formatter = new HtmlTableOfContentsFormatter();
            _toc = formatter.Format(features.ChildNodes[0].Data.OriginalLocationUrl, features);

        }

        [Test]
        public void toc_should_be_set_with_correct_attributes()
        {
            Assert.NotNull(_toc);
            Assert.AreEqual("toc", _toc.Attributes("id").First().Value);
        }

        [Test]
        public void first_node_should_be_index()
        {
            var ul = _toc.Elements().First();
            
            // Assert that the first feature is appropriately set in the TOC
            Assert.NotNull(ul);
            Assert.AreEqual(true, ul.HasElements);

            var li1 = ul.FindFirstDescendantWithName("li");
            Assert.NotNull(li1);

            var anchorInLI1 = li1.Elements().First();
            Assert.AreEqual(true, anchorInLI1.HasAttributes);
            Assert.AreEqual("#", anchorInLI1.Attribute("href").Value);
            Assert.AreEqual("This is an index written in Markdown", anchorInLI1.Value);
        }

        [Test]
        public void Can_crawl_directory_tree_for_features_successfully()
        {
            var ul = _toc.Elements().First();
            var ul2 = ul.FindFirstDescendantWithName("ul");
            Assert.AreEqual(true, ul2.HasElements);

            // Assert that a feature file is appropriately set deeper down in the TOC
            var li2 = ul2.FindFirstDescendantWithName("li");
            Assert.NotNull(li2);

            var anchorInLI2 = li2.Elements().First();
            Assert.AreEqual(true, anchorInLI2.HasAttributes);
            Assert.AreEqual("SubLevelOne/LevelOneSublevelOne.html", anchorInLI2.Attribute("href").Value);
            Assert.AreEqual("Addition", anchorInLI2.Value);
        }
    }
}
