using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.Test.Helpers;

namespace PicklesDoc.Pickles.Test.Formatters
{
    [TestFixture]
    public class table_of_contents_should_be_created_from_a_folder_structure : BaseFixture
    {
        private const string ROOT_PATH = @"FakeFolderStructures\FeatureCrawlerTests";
        private XElement _toc;

        [TestFixtureSetUp]
        public void Setup()
        {
            GeneralTree<INode> features = Container.Resolve<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

            var formatter = new HtmlTableOfContentsFormatter(null);
            this._toc = formatter.Format(features.ChildNodes[0].Data.OriginalLocationUrl, features,
                                    new DirectoryInfo(ROOT_PATH));
        }

        [Test]
        public void Can_crawl_directory_tree_for_features_successfully()
        {
            XElement ul = this._toc.FindFirstDescendantWithName("ul");
            XElement ul2 = ul.FindFirstDescendantWithName("ul");
            Assert.AreEqual(true, ul2.HasElements);

            // Assert that a feature file is appropriately set deeper down in the TOC
            XElement li2 = ul2.FindFirstDescendantWithName("li");
            Assert.NotNull(li2);

            XElement anchorInLI2 = li2.Elements().First();
            Assert.AreEqual(true, anchorInLI2.HasAttributes);
            Assert.AreEqual("SubLevelOne/LevelOneSublevelOne.html", anchorInLI2.Attribute("href").Value);
            Assert.AreEqual("Addition", anchorInLI2.Value);
        }

        [Test]
        public void TableOfContent_Must_Contain_Link_To_Home()
        {
            XElement home =
                this._toc.Descendants().SingleOrDefault(
                    d => d.Attributes().Any(a => a.Name.LocalName == "id" && a.Value == "root"));

            Assert.IsNotNull(home);
        }

        [Test]
        public void TableOfContent_Must_Contain_One_Paragraph_With_Current_Class()
        {
            XElement span =
                this._toc.Descendants().Where(e => e.Name.LocalName == "span").SingleOrDefault(
                    e => e.Attributes().Any(a => a.Name.LocalName == "class" && a.Value == "current"));

            Assert.IsNotNull(span);
        }

        [Test]
        public void TableOfContent_Must_Link_Folder_Nodes_To_That_Folders_Index_File()
        {
            XElement directory =
                this._toc.Descendants().First(
                    d =>
                    d.Name.LocalName == "div" &&
                    d.Attributes().Any(a => a.Name.LocalName == "class" && a.Value == "directory"));
            XElement link = directory.Descendants().First();

            Assert.AreEqual("a", link.Name.LocalName);
            XAttribute href = link.Attributes().Single(a => a.Name.LocalName == "href");
            Assert.AreEqual("SubLevelOne/index.html", href.Value);
        }

        [Test]
        public void Ul_Element_Must_Contain_Li_Children_Only()
        {
            IEnumerable<XElement> childrenOfUl = this._toc.Elements().First().Elements();

            int numberOfChildren = childrenOfUl.Count();
            int numberOfLiChildren = childrenOfUl.Count(e => e.Name.LocalName == "li");

            Assert.AreEqual(numberOfChildren, numberOfLiChildren);
        }

        [Test]
        public void first_ul_node_should_be_index()
        {
            XElement ul = this._toc.FindFirstDescendantWithName("ul");

            // Assert that the first feature is appropriately set in the TOC
            Assert.NotNull(ul);
            Assert.AreEqual(true, ul.HasElements);

            XElement li1 =
                ul.Descendants().Where(d => d.Name.LocalName == "li").FirstOrDefault();
            Assert.NotNull(li1);

            XElement anchorInLI1 = li1.Elements().First();
            Assert.AreEqual(true, anchorInLI1.HasAttributes);
            Assert.AreEqual("current", anchorInLI1.Attribute("class").Value);
            Assert.AreEqual("Home", anchorInLI1.Value);
        }

        [Test]
        public void toc_should_be_set_with_correct_attributes()
        {
            Assert.NotNull(this._toc);
            Assert.AreEqual("toc", this._toc.Attributes("id").First().Value);
        }
    }
}