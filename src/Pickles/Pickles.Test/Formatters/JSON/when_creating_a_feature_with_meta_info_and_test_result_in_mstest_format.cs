//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="when_creating_a_feature_with_meta_info_and_test_result_in_mstest_format.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using NFluent;
using NGenerics.DataStructures.Trees;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.JSON;
using PicklesDoc.Pickles.Test.Helpers;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.Test.Formatters.JSON
{
    public class when_creating_a_feature_with_meta_info_and_test_result_in_mstest_format : BaseFixture
    {
        public string Setup()
        {
            const string OUTPUT_DIRECTORY = FileSystemPrefix + @"JSONFeatureOutput";
            const string ROOT_PATH = FileSystemPrefix + @"JSON\Features";
            const string testResultFilePath = FileSystemPrefix + @"JSON\results-example-failing-and-pasing-mstest.trx";

            string filePath = FileSystem.Path.Combine(OUTPUT_DIRECTORY, JSONDocumentationBuilder.JsonFileName);

            AddFakeFolderAndFiles("JSON", new[] { "results-example-failing-and-pasing-mstest.trx" });
            AddFakeFolderAndFiles(
                @"JSON\Features",
                new[]
                    {
                      "OneScenarioTransferingMoneyBetweenAccountsFailing.feature",
                      "TransferBetweenAccounts_WithSuccess.feature",
                      "TwoScenariosTransferingFundsOneFailingOneSuccess.feature",
                      "TwoScenariosTransferingMoneyBetweenAccoutsWithSuccess.feature",
                    });

            var resultFile = RetrieveContentOfFileFromResources(ResourcePrefix + "JSON.results-example-failing-and-pasing-mstest.trx");
            FileSystem.AddFile(testResultFilePath, resultFile);

            GeneralTree<INode> features = Container.Resolve<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

            var outputDirectory = FileSystem.DirectoryInfo.FromDirectoryName(OUTPUT_DIRECTORY);
            if (!outputDirectory.Exists) outputDirectory.Create();

            var configuration = new Configuration() {
                                    OutputFolder = FileSystem.DirectoryInfo.FromDirectoryName(OUTPUT_DIRECTORY),
                                    DocumentationFormat = DocumentationFormat.JSON,
                                    TestResultsFormat = TestResultsFormat.MsTest,
                                    SystemUnderTestName = "SUT Name",
                                    SystemUnderTestVersion = "SUT Version"
                                };
            configuration.AddTestResultFile(FileSystem.FileInfo.FromFileName(testResultFilePath));

            ITestResults testResults = new MsTestResults(configuration);
            var jsonDocumentationBuilder = new JSONDocumentationBuilder(configuration, testResults, FileSystem);
            jsonDocumentationBuilder.Build(features);
            string content = FileSystem.File.ReadAllText(filePath);

            return content;
        }

        [Test]
        public void it_should_contain_result_keys_in_the_json_document()
        {
            string content = this.Setup();

            content.AssertJsonContainsKey("Result");
        }

        [Test]
        public void it_should_contain_the_sut_info_in_the_json_document()
        {
            string content = this.Setup();

            var jsonObj = JObject.Parse(content);

            var configuration = jsonObj["Configuration"];

            Check.That(configuration["SutName"].ToString()).IsEqualTo("SUT Name");
            Check.That(configuration["SutVersion"].ToString()).IsEqualTo("SUT Version");
        }

        [Test]
        public void it_should_indicate_WasSuccessful_is_true()
        {
            string content = this.Setup();

            var jsonObj = JObject.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonObj["Features"]
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Two more scenarios transfering funds between accounts")
                                                     select feat;

            Check.That(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>()).IsTrue();
        }

        [Test]
        public void it_should_indicate_WasSuccessful_is_true_for_the_other_success_feature()
        {
            string content = this.Setup();

            var jsonObj = JObject.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonObj["Features"]
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Transfer funds between accounts")
                                                     select feat;

            Check.That(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>()).IsTrue();
        }

        [Test]
        public void it_should_indicate_WasSuccessful_is_false_for_failing_scenario()
        {
            string content = this.Setup();

            var jsonObj = JObject.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonObj["Features"]
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Transfer funds between accounts onc scenario and FAILING")
                                                     select feat;

            Check.That(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>()).IsFalse();
        }


        [Test]
        public void it_should_indicate_WasSuccessful_is_false_for_another_failing_scenario()
        {
            string content = this.Setup();

            var jsonObj = JObject.Parse(content);


            IEnumerable<JToken> featureJsonElement = from feat in jsonObj["Features"]
                                                     where
                                                         feat["Feature"]["Name"].Value<string>().Equals(
                                                             "Two more scenarios transfering funds between accounts - one failng and one succeding")
                                                     select feat;

            Check.That(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>()).IsFalse();
        }


        [Test]
        public void it_should_contain_WasSuccessful_key_in_Json_document()
        {
            string content = this.Setup();

            var jsonObj = JObject.Parse(content);

            Check.That(jsonObj["Features"][0]["Result"]["WasSuccessful"].ToString()).IsNotEmpty();
        }


        [Test]
        public void it_should_WasSuccessful_false_for_feature_X_Json_document()
        {
            string content = this.Setup();

            var jsonObj = JObject.Parse(content);

            Check.That(jsonObj["Features"][0]["Result"]["WasSuccessful"].ToString()).IsNotEmpty();
        }
    }
}