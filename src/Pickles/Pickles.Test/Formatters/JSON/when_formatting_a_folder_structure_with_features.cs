﻿using System.IO;
using Ninject;
using NUnit.Framework;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.JSON;
using Should.Fluent;
using Pickles.Test.Helpers;


namespace Pickles.Test.Formatters.JSON
{
    [TestFixture]
    public class when_formatting_a_folder_structure_with_features : BaseFixture
    {
        private string filePath = Path.Combine(OUTPUT_DIRECTORY, JSONDocumentationBuilder.JS_FILE_NAME);
        private const string ROOT_PATH = @"FakeFolderStructures";
        private const string OUTPUT_DIRECTORY = @"C:\temp\";


        [TestFixtureSetUp]
        public void Setup()
        {
            var features = Kernel.Get<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

            var configuration = new Configuration
                                    {
                                        OutputFolder = new DirectoryInfo(OUTPUT_DIRECTORY), 
                                        DocumentationFormat = DocumentationFormat.JSON
                                    };

            var jsonDocumentationBuilder = new JSONDocumentationBuilder(configuration);
            jsonDocumentationBuilder.Build(features);
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
