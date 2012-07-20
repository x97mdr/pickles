using System.IO;
using NUnit.Framework;
using Ninject;
using Pickles.DirectoryCrawler;

namespace Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlDocumentationBuilderTests : BaseFixture
    {
        private const string ROOT_PATH = @"FakeFolderStructures\EmptyFolderTests";

        [TestFixtureSetUp]
        public void SetUp()
        {
            if (!Directory.Exists(ROOT_PATH))
            {
                Directory.CreateDirectory(ROOT_PATH);
            }
        }

        [Test]
        public void ShouldNotBlowUpWHenParsingEmptyFolder()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = new DirectoryInfo(ROOT_PATH + @"..\");
            var features = Kernel.Get<DirectoryTreeCrawler>().Crawl(ROOT_PATH);
            var builder = Kernel.Get<HtmlDocumentationBuilder>();
            
            builder.Build(features);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (Directory.Exists(ROOT_PATH))
            {
                Directory.Delete(ROOT_PATH, true);
            }
        }
    }
}