using System;
using NGenerics.DataStructures.Trees;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.JSON;
using PicklesDoc.Pickles.Test.Helpers;

namespace PicklesDoc.Pickles.Test.Formatters.JSON
{
    [TestFixture]
    public class when_formatting_a_folder_structure_with_features : BaseFixture
    {
        private const string OUTPUT_DIRECTORY = FileSystemPrefix + @"JSONFeatureOutput";

        public void Setup()
        {
            AddFakeFolderStructures();

            GeneralTree<INode> features = Container.Resolve<DirectoryTreeCrawler>().Crawl(FileSystemPrefix);

            var outputDirectory = FileSystem.DirectoryInfo.FromDirectoryName(OUTPUT_DIRECTORY);
            if (!outputDirectory.Exists) outputDirectory.Create();

            var configuration = new Configuration() {
                                      OutputFolder = FileSystem.DirectoryInfo.FromDirectoryName(OUTPUT_DIRECTORY),
                                        DocumentationFormat = DocumentationFormat.JSON
                                    };


            var jsonDocumentationBuilder = new JSONDocumentationBuilder(configuration, null, FileSystem);
            jsonDocumentationBuilder.Build(features);
        }

        [Test]
        public void should_contain_the_features()
        {
            Setup();

            string content = FileSystem.File.ReadAllText(this.FileSystem.Path.Combine(OUTPUT_DIRECTORY, JSONDocumentationBuilder.JsonFileName));
            content.AssertJSONKeyValue("Name", "Addition");
        }
    }
}