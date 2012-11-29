using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NGenerics.DataStructures.Trees;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.JSON;
using PicklesDoc.Pickles.Test.Helpers;
using PicklesDoc.Pickles.TestFrameworks;
using Should.Fluent;

namespace PicklesDoc.Pickles.Test.Formatters.JSON
{
    public class when_creating_a_feature_with_meta_info_and_test_result_in_mstest_format : BaseFixture
    {
        private const string ROOT_PATH = @"Formatters\JSON\Features";
        private const string OUTPUT_DIRECTORY = @"JSONFeatureOutput";
        private readonly string filePath = Path.Combine(OUTPUT_DIRECTORY, JSONDocumentationBuilder.JsonFileName);
        private string testResultFilePath = @"Formatters\JSON\results-example-failing-and-pasing-mstest.trx";

        [TestFixtureSetUp]
        public void Setup()
        {
            if (File.Exists(this.testResultFilePath) == false)
            {
                throw new FileNotFoundException("File " + this.testResultFilePath + " was not found");
            }

            GeneralTree<IDirectoryTreeNode> features = Container.Resolve<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

            var outputDirectory = new DirectoryInfo(OUTPUT_DIRECTORY);
            if (!outputDirectory.Exists) outputDirectory.Create();

            var configuration = new Configuration
                                    {
                                        OutputFolder = new DirectoryInfo(OUTPUT_DIRECTORY),
                                        DocumentationFormat = DocumentationFormat.JSON,
                                        TestResultsFile = new FileInfo(this.testResultFilePath),
                                        TestResultsFormat = TestResultsFormat.MsTest
                                    };

            ITestResults testResults = new MsTestResults(configuration);
            var jsonDocumentationBuilder = new JSONDocumentationBuilder(configuration, testResults);
            jsonDocumentationBuilder.Build(features);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            if (Directory.Exists(OUTPUT_DIRECTORY))
            {
                Directory.Delete(OUTPUT_DIRECTORY, true);
            }
        }


        [Test]
        public void a_single_file_should_have_been_created()
        {
            File.Exists(this.filePath).Should().Be.True();
        }

        [Test]
        public void it_should_contain_result_keys_in_the_json_document()
        {
            string content = File.ReadAllText(this.filePath);
            content.AssertJsonContainsKey("Result");
        }

        [Test]
        public void it_should_indicate_WasSuccessful_is_true()
        {
            string content = File.ReadAllText(this.filePath);
            JArray jsonArray = JArray.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonArray
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Two more scenarios transfering funds between accounts")
                                                     select feat;

            Assert.IsTrue(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
        }

        [Test]
        public void it_should_indicate_WasSuccessful_is_true_for_the_other_success_feature()
        {
            string content = File.ReadAllText(this.filePath);
            JArray jsonArray = JArray.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonArray
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Transfer funds between accounts")
                                                     select feat;

            Assert.IsTrue(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
        }

        [Test]
        public void it_should_indicate_WasSuccessful_is_false_for_failing_scenario()
        {
            string content = File.ReadAllText(this.filePath);
            JArray jsonArray = JArray.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonArray
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Transfer funds between accounts onc scenario and FAILING")
                                                     select feat;

            Assert.IsFalse(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
        }


        [Test]
        public void it_should_indicate_WasSuccessful_is_false_for_another_failing_scenario()
        {
            string content = File.ReadAllText(this.filePath);
            JArray jsonArray = JArray.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonArray
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Two more scenarios transfering funds between accounts - one failng and one succeding")
                                                     select feat;

            Assert.IsFalse(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
        }


        [Test]
        public void it_should_contain_WasSuccessful_key_in_Json_document()
        {
            string content = File.ReadAllText(this.filePath);
            JArray jsonArray = JArray.Parse(content);

            Assert.IsNotEmpty(jsonArray[0]["Result"]["WasSuccessful"].ToString());
        }


        [Test]
        public void it_should_WasSuccessful_false_for_feature_X_Json_document()
        {
            string content = File.ReadAllText(this.filePath);
            JArray jsonArray = JArray.Parse(content);

            Assert.IsNotEmpty(jsonArray[0]["Result"]["WasSuccessful"].ToString());
        }
    }
}