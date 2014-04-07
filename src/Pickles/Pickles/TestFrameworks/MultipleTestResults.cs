using System;
using System.Collections.Generic;
using System.Linq;

using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.TestFrameworks
{
  public class MultipleTestResults : ITestResults
  {
    private readonly IEnumerable<ITestResults> testResults;

    public MultipleTestResults(IEnumerable<ITestResults> testResults)
    {
      this.testResults = testResults;
    }

    public TestResult GetFeatureResult(Feature feature)
    {
      var results = testResults.Select(tr => tr.GetFeatureResult(feature)).ToArray();

      return EvaluateTestResults(results);
    }

    private static TestResult EvaluateTestResults(TestResult[] results)
    {
      if (results.Any(r => r == TestResult.Failed))
      {
        return TestResult.Failed;
      }

      if (results.Any(r => r == TestResult.Passed))
      {
        return TestResult.Passed;
      }

      return TestResult.Inconclusive;
    }

    public TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
    {
      var results = testResults.Select(tr => tr.GetScenarioOutlineResult(scenarioOutline)).ToArray();

      return EvaluateTestResults(results);
    }

    public TestResult GetScenarioResult(Scenario scenario)
    {
      var results = testResults.Select(tr => tr.GetScenarioResult(scenario)).ToArray();

      return EvaluateTestResults(results);
    }
  }
}