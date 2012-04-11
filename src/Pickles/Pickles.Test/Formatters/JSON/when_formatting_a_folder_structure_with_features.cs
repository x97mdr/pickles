using System.IO;
using Ninject;
using NUnit.Framework;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.JSON;
using Should.Fluent;
using Pickles.Test.Helpers;
using System;
using Pickles.TestFrameworks;


namespace Pickles.Test.Formatters.JSON
{
    [TestFixture]
    public class when_formatting_a_folder_structure_with_features : BaseFixture
    {
        private const string ROOT_PATH = @"FakeFolderStructures";
        private const string OUTPUT_DIRECTORY = @"JSONFeatureOutput";
        private string filePath = Path.Combine(OUTPUT_DIRECTORY, JSONDocumentationBuilder.JS_FILE_NAME);

        [TestFixtureSetUp]
        public void Setup()
        {
            var features = Kernel.Get<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

            var outputDirectory = new DirectoryInfo(OUTPUT_DIRECTORY);
            if (!outputDirectory.Exists) outputDirectory.Create();

            var configuration = new Configuration
                                    {
                                        OutputFolder = new DirectoryInfo(OUTPUT_DIRECTORY), 
                                        DocumentationFormat = DocumentationFormat.JSON
                                    };

            
            var jsonDocumentationBuilder = new JSONDocumentationBuilder(configuration, null);
            jsonDocumentationBuilder.Build(features);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
          //Cleanup output folder
          if (Directory.Exists(OUTPUT_DIRECTORY))
          {
            Directory.Delete(OUTPUT_DIRECTORY, true);
          }
        }


        [Test]
        public void a_single_file_should_have_been_created()
        {
            File.Exists(filePath).Should().Be.True();
        }

        [Test]
        public void should_contain_the_features()
        {
            var content = File.ReadAllText(filePath);
            content.AssertJSONKeyValue("Name", "Addition");
        }
    }
}
