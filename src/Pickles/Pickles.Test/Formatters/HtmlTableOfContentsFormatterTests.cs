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
using Pickles.DirectoryCrawler;


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
        public void first_ul_node_should_be_index()
        {
            var ul = _toc.FindFirstDescendantWithName("ul");
            
            // Assert that the first feature is appropriately set in the TOC
            Assert.NotNull(ul);
            Assert.AreEqual(true, ul.HasElements);

            var li1 = ul.FindFirstDescendantWithName("li");
            Assert.NotNull(li1);

            var anchorInLI1 = li1.Elements().First();
            Assert.AreEqual(true, anchorInLI1.HasAttributes);
            Assert.AreEqual("current", anchorInLI1.Attribute("class").Value);
            Assert.AreEqual("This is an index written in Markdown", anchorInLI1.Value);
        }

        [Test]
        public void Can_crawl_directory_tree_for_features_successfully()
        {
            var ul = _toc.FindFirstDescendantWithName("ul");
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

        [Test]
        public void Ul_Element_Must_Contain_Li_Children_Only()
        {
            var childrenOfUl = _toc.Elements().First().Elements();

            int numberOfChildren = childrenOfUl.Count();
            int numberOfLiChildren = childrenOfUl.Count(e => e.Name.LocalName == "li");

            Assert.AreEqual(numberOfChildren, numberOfLiChildren);
        }

        [Test]
        public void TableOfContent_Must_Contain_One_Paragraph_With_Current_Class()
        {
            _toc.Descendants().Where(e => e.Name.LocalName == "span").Single(e => e.Attributes().Any(a => a.Name.LocalName == "class" && a.Value == "current"));
        }

      [Test]
      public void TableOfContent_Must_Link_Folder_Nodes_To_That_Folders_Index_File()
      {
        XElement directory = _toc.Descendants().First(d => d.Name.LocalName == "div" && d.Attributes().Any(a => a.Name.LocalName == "class" && a.Value == "directory"));
        XElement link = directory.Descendants().First();

        Assert.AreEqual("a", link.Name.LocalName);
        var href = link.Attributes().Single(a => a.Name.LocalName == "href");
        Assert.AreEqual("SubLevelOne/index.html", href.Value);
      }
    }
}
