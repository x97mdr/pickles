using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NFluent;
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
        private const string ROOT_PATH = FileSystemPrefix + @"FeatureCrawlerTests";
        private XElement _toc;

        public void Setup()
        {
            AddFakeFolderStructures();

            GeneralTree<INode> features = Container.Resolve<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

            var formatter = new HtmlTableOfContentsFormatter(null, this.FileSystem);
            this._toc = formatter.Format(features.ChildNodes[0].Data.OriginalLocationUrl, features,
                                    FileSystem.DirectoryInfo.FromDirectoryName(ROOT_PATH));
        }

        [Test]
        public void Can_crawl_directory_tree_for_features_successfully()
        {
            Setup();

            XElement ul = this._toc.FindFirstDescendantWithName("ul");
            XElement ul2 = ul.FindFirstDescendantWithName("ul");
            Check.That(ul2.HasElements).IsTrue();

            // Assert that a feature file is appropriately set deeper down in the TOC
            XElement li2 = ul2.FindFirstDescendantWithName("li");
            Check.That(li2).IsNotNull();

            XElement anchorInLI2 = li2.Elements().First();
            Check.That(anchorInLI2.HasAttributes).IsTrue();
            Check.That(anchorInLI2.Attribute("href").Value).IsEqualTo("SubLevelOne/LevelOneSublevelOne.html");
            Check.That(anchorInLI2.Value).IsEqualTo("Addition");
        }

        [Test]
        public void TableOfContent_Must_Contain_Link_To_Home()
        {
            Setup();

            XElement home =
                  this._toc.Descendants().SingleOrDefault(
                      d => d.Attributes().Any(a => a.Name.LocalName == "id" && a.Value == "root"));

            Check.That(home).IsNotNull();
        }

        [Test]
        public void TableOfContent_Must_Contain_One_Paragraph_With_Current_Class()
        {
            Setup();

            XElement span =
                  this._toc.Descendants().Where(e => e.Name.LocalName == "span").SingleOrDefault(
                      e => e.Attributes().Any(a => a.Name.LocalName == "class" && a.Value == "current"));

            Check.That(span).IsNotNull();
        }

        [Test]
        public void TableOfContent_Must_Link_Folder_Nodes_To_That_Folders_Index_File()
        {
            Setup();

            XElement directory =
                  this._toc.Descendants().First(
                      d =>
                      d.Name.LocalName == "div" &&
                      d.Attributes().Any(a => a.Name.LocalName == "class" && a.Value == "directory"));
            XElement link = directory.Descendants().First();

            Check.That(link.Name.LocalName).IsEqualTo("a");
            XAttribute href = link.Attributes().Single(a => a.Name.LocalName == "href");
            Check.That(href.Value).IsEqualTo("SubLevelOne/index.html");
        }

        [Test]
        public void Ul_Element_Must_Contain_Li_Children_Only()
        {
            Setup();

            IEnumerable<XElement> childrenOfUl = this._toc.Elements().First().Elements();

            int numberOfChildren = childrenOfUl.Count();
            int numberOfLiChildren = childrenOfUl.Count(e => e.Name.LocalName == "li");

            Check.That(numberOfLiChildren).IsEqualTo(numberOfChildren);
        }

        [Test]
        public void first_ul_node_should_be_index()
        {
            Setup();

            XElement ul = this._toc.FindFirstDescendantWithName("ul");

            // Assert that the first feature is appropriately set in the TOC
            Check.That(ul).IsNotNull();
            Check.That(ul.HasElements).IsTrue();

            XElement li1 = ul.Descendants().FirstOrDefault(d => d.Name.LocalName == "li");
            Check.That(li1).IsNotNull();

            XElement anchorInLI1 = li1.Elements().First();
            Check.That(anchorInLI1.HasAttributes).IsTrue();
            Check.That(anchorInLI1.Attribute("class").Value).IsEqualTo("current");
            Check.That(anchorInLI1.Value).IsEqualTo("Home");
        }

        [Test]
        public void toc_should_be_set_with_correct_attributes()
        {
            Setup();

            Check.That(this._toc).IsNotNull();
            Check.That(this._toc.Attributes("id").First().Value).IsEqualTo("toc");
        }
    }
}