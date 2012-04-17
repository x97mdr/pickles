using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Ninject;
using NUnit.Framework;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.JSON;
using Pickles.Test.Helpers;
using Pickles.TestFrameworks;
using Should.Fluent;

namespace Pickles.Test.Formatters.JSON
{
  public class when_creating_a_feature_with_meta_info_and_test_result_in_mstest_format : BaseFixture
  {
    private const string ROOT_PATH = @"Formatters\JSON\Features";
    private const string OUTPUT_DIRECTORY = @"JSONFeatureOutput";
    private string filePath = Path.Combine(OUTPUT_DIRECTORY, JSONDocumentationBuilder.JS_FILE_NAME);
    private string testResultFilePath = @"Formatters\JSON\results-example-failing-and-pasing-mstest.trx";

    [TestFixtureSetUp]
    public void Setup()
    {
      if (File.Exists(testResultFilePath) == false)
      {
        throw new FileNotFoundException("File " + testResultFilePath + " was not found");
      }

      var features = Kernel.Get<DirectoryTreeCrawler>().Crawl(ROOT_PATH);

      var outputDirectory = new DirectoryInfo(OUTPUT_DIRECTORY);
      if (!outputDirectory.Exists) outputDirectory.Create();

      var configuration = new Configuration
                            {
                              OutputFolder = new DirectoryInfo(OUTPUT_DIRECTORY),
                              DocumentationFormat = DocumentationFormat.JSON,
                              TestResultsFile = new FileInfo(testResultFilePath),
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
      File.Exists(filePath).Should().Be.True();
    }

    [Test]
    public void it_should_contain_result_keys_in_the_json_document()
    {
      var content = File.ReadAllText(filePath);
      content.AssertJsonContainsKey("Result");
    }

    [Test]
    public void it_should_indicate_WasSuccessful_is_true()
    {
      var content = File.ReadAllText(filePath);
      JArray jsonArray = JArray.Parse(content);

    
      var featureJsonElement = from feat in jsonArray
                where feat["Feature"]["Name"].Value<string>().Equals("Two more scenarios transfering funds between accounts")
                select feat;

      Assert.IsTrue( featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
    }

    [Test]
    public void it_should_indicate_WasSuccessful_is_true_for_the_other_success_feature()
    {
      var content = File.ReadAllText(filePath);
      JArray jsonArray = JArray.Parse(content);


      var featureJsonElement = from feat in jsonArray
                               where feat["Feature"]["Name"].Value<string>().Equals("Transfer funds between accounts")
                               select feat;

      Assert.IsTrue(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
    }

    [Test]
    public void it_should_indicate_WasSuccessful_is_false_for_failing_scenario()
    {
      var content = File.ReadAllText(filePath);
      JArray jsonArray = JArray.Parse(content);


      var featureJsonElement = from feat in jsonArray
                               where feat["Feature"]["Name"].Value<string>().Equals("Transfer funds between accounts onc scenario and FAILING")
                               select feat;

      Assert.IsFalse(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
    }


    [Test]
    public void it_should_indicate_WasSuccessful_is_false_for_another_failing_scenario()
    {
      var content = File.ReadAllText(filePath);
      JArray jsonArray = JArray.Parse(content);


      var featureJsonElement = from feat in jsonArray
                               where feat["Feature"]["Name"].Value<string>().Equals("Two more scenarios transfering funds between accounts - one failng and one succeding")
                               select feat;

      Assert.IsFalse(featureJsonElement.ElementAt(0)["Result"]["WasSuccessful"].Value<bool>());
    }


    [Test]
    public void it_should_contain_WasSuccessful_key_in_Json_document()
    {
      string content = File.ReadAllText(filePath);
      JArray jsonArray = JArray.Parse(content);

      Assert.IsNotEmpty(jsonArray[0]["Result"]["WasSuccessful"].ToString());
    }


    [Test]
    public void it_should_WasSuccessful_false_for_feature_X_Json_document()
    {
      string content = File.ReadAllText(filePath);
      JArray jsonArray = JArray.Parse(content);

      Assert.IsNotEmpty(jsonArray[0]["Result"]["WasSuccessful"].ToString());
    }
  }
}