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
        private const string ROOT_PATH = FileSystemPrefix + @"EmptyFolderTests";

        [Test]
        public void ShouldNotBlowUpWHenParsingEmptyFolder()
        {
            AddFakeFolderStructures();

            var configuration = Container.Resolve<Configuration>();
            configuration.OutputFolder = FileSystem.DirectoryInfo.FromDirectoryName(FileSystemPrefix);
            var features = Container.Resolve<DirectoryTreeCrawler>().Crawl(ROOT_PATH);
            var builder = Container.Resolve<HtmlDocumentationBuilder>();
            
            builder.Build(features);
        }
    }
}