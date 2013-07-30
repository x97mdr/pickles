using System;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;

namespace PicklesDoc.Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlDocumentationBuilderTests : BaseFixture
    {
        private const string ROOT_PATH = @"FakeFolderStructures\EmptyFolderTests";

        [TestFixtureSetUp]
        public void SetUp()
        {
            if (!System.IO.Directory.Exists(ROOT_PATH))
            {
                System.IO.Directory.CreateDirectory(ROOT_PATH);
            }
        }

        [Test]
        public void ShouldNotBlowUpWHenParsingEmptyFolder()
        {
            var configuration = Container.Resolve<Configuration>();
            configuration.OutputFolder = MockFileSystem.DirectoryInfo.FromDirectoryName(ROOT_PATH + @"..\");
            var features = Container.Resolve<DirectoryTreeCrawler>().Crawl(ROOT_PATH);
            var builder = Container.Resolve<HtmlDocumentationBuilder>();
            
            builder.Build(features);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (System.IO.Directory.Exists(ROOT_PATH))
            {
                System.IO.Directory.Delete(ROOT_PATH, true);
            }
        }
    }
}