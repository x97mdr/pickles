using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.TestFrameworks
{
  public abstract class MultipleTestResults : ITestResults
  {
    private readonly bool supportsExampleResults;

    private readonly IEnumerable<ITestResults> testResults;

    protected MultipleTestResults(bool supportsExampleResults, IEnumerable<ITestResults> testResults)
    {
      this.supportsExampleResults = supportsExampleResults;
      this.testResults = testResults;
    }

    protected MultipleTestResults(bool supportsExampleResults, Configuration configuration)
    {
      this.supportsExampleResults = supportsExampleResults;
      this.testResults = this.GetSingleTestResults(configuration);
    }

    public abstract TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues);

    public bool SupportsExampleResults
    {
      get
      {
        return supportsExampleResults;
      }
    }

    protected IEnumerable<ITestResults> TestResults
    {
      get
      {
        return this.testResults;
      }
    }

    private IEnumerable<ITestResults> GetSingleTestResults(Configuration configuration)
    {
      ITestResults[] results;

      if (configuration.HasTestResults)
      {
        results = configuration.TestResultsFiles.Select(ConstructSingleTestResult).ToArray();
      }
      else
      {
        results = new ITestResults[0];
      }

      return results;
    }

    protected abstract ITestResults ConstructSingleTestResult(FileInfoBase fileInfo);

    public TestResult GetFeatureResult(Feature feature)
    {
      var results = this.TestResults.Select(tr => tr.GetFeatureResult(feature)).ToArray();

      return EvaluateTestResults(results);
    }

    protected static TestResult EvaluateTestResults(TestResult[] results)
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
      var results = this.TestResults.Select(tr => tr.GetScenarioOutlineResult(scenarioOutline)).ToArray();

      return EvaluateTestResults(results);
    }

    public TestResult GetScenarioResult(Scenario scenario)
    {
      var results = this.TestResults.Select(tr => tr.GetScenarioResult(scenario)).ToArray();

      return EvaluateTestResults(results);
    }
  }
}