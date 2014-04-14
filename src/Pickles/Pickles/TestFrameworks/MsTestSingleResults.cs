using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.TestFrameworks
{
  public class MsTestSingleResults : ITestResults
  {
    private static readonly XNamespace ns = @"http://microsoft.com/schemas/VisualStudio/TeamTest/2010";
    private readonly XDocument resultsDocument;

    public MsTestSingleResults(XDocument resultsDocument)
    {
      this.resultsDocument = resultsDocument;
    }

    private Guid GetScenarioExecutionId(Scenario queriedScenario)
    {
      var idString =
        (from scenario in this.AllScenariosInResultFile()
         let properties = PropertiesOf(scenario)
         where properties != null
         where FeatureNamePropertyExistsWith(queriedScenario.Feature.Name, among: properties)
         where NameOf(scenario) == queriedScenario.Name
         select ScenarioExecutionIdStringOf(scenario)).FirstOrDefault();

      return !string.IsNullOrEmpty(idString) ? new Guid(idString) : Guid.Empty;
    }

    private TestResult GetExecutionResult(Guid scenarioExecutionId)
    {
      var resultText =
        (from scenarioResult in this.AllScenarioExecutionResultsInResultFile()
         let executionId = ResultExecutionIdOf(scenarioResult)
         where scenarioExecutionId == executionId
         let outcome = ResultOutcomeOf(scenarioResult)
         select outcome).FirstOrDefault() ?? string.Empty;

      switch (resultText.ToLowerInvariant())
      {
        case "passed":
          return TestResult.Passed;
        case "failed":
          return TestResult.Failed;
        default:
          return TestResult.Inconclusive;
      }
    }

    private static string ResultOutcomeOf(XElement scenarioResult)
    {
      return scenarioResult.Attribute("outcome").Value;
    }

    private static Guid ResultExecutionIdOf(XElement unitTestResult)
    {
      return new Guid(unitTestResult.Attribute("executionId").Value);
    }

    #region ITestResults Members

    #endregion

    #region Linq Helpers

    public TestResult GetFeatureResult(Feature feature)
    {
      var featureExecutionIds =
        from scenario in this.AllScenariosInResultFile()
        let properties = PropertiesOf(scenario)
        where properties != null
        where FeatureNamePropertyExistsWith(feature.Name, among: properties)
        select ScenarioExecutionIdOf(scenario);

      TestResult result = featureExecutionIds.Select(this.GetExecutionResult).Merge();

      return result;
    }

    public TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
    {
      var queriedFeatureName = scenarioOutline.Feature.Name;
      var queriedScenarioOutlineName = scenarioOutline.Name;

      var allScenariosForAFeature =
        from scenario in this.AllScenariosInResultFile()
        let scenarioProperties = PropertiesOf(scenario)
        where scenarioProperties != null
        where FeatureNamePropertyExistsWith(queriedFeatureName, among: scenarioProperties)
        select scenario;

      var scenarioOutlineExecutionIds = from scenario in allScenariosForAFeature
                                        where NameOf(scenario) == queriedScenarioOutlineName
                                        select ScenarioExecutionIdOf(scenario);

      TestResult result = scenarioOutlineExecutionIds.Select(this.GetExecutionResult).Merge();

      return result;
    }


    public TestResult GetScenarioResult(Scenario scenario)
    {
      Guid scenarioExecutionId = this.GetScenarioExecutionId(scenario);
      return this.GetExecutionResult(scenarioExecutionId);
    }


    private static Guid ScenarioExecutionIdOf(XElement scenario)
    {
      return new Guid(ScenarioExecutionIdStringOf(scenario));
    }

    private static string ScenarioExecutionIdStringOf(XElement scenario)
    {
      return scenario.Element(ns + "Execution").Attribute("id").Value;
    }

    private static string NameOf(XElement scenario)
    {
      return scenario.Element(ns + "Description").Value;
    }

    private static XElement PropertiesOf(XElement scenariosReportes)
    {
      return scenariosReportes.Element(ns + "Properties");
    }

    private static bool FeatureNamePropertyExistsWith(string featureName, XElement among)
    {
      var properties = among;
      return (from property in properties.Elements(ns + "Property")
              let key = property.Element(ns + "Key")
              let value = property.Element(ns + "Value")
              where key.Value == "FeatureTitle" && value.Value == featureName
              select property).Any();
    }

    private IEnumerable<XElement> AllScenariosInResultFile()
    {
      return this.resultsDocument.Root.Descendants(ns + "UnitTest");
    }

    private IEnumerable<XElement> AllScenarioExecutionResultsInResultFile()
    {
      return this.resultsDocument.Root.Descendants(ns + "UnitTestResult");
    }

    #endregion
  }
}