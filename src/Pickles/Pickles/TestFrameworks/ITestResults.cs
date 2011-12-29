using System;

namespace Pickles.TestFrameworks
{
    public interface ITestResults
    {
        TestResult GetExampleResult(Pickles.Parser.ScenarioOutline scenarioOutline, string[] row);
        TestResult GetFeatureResult(Pickles.Parser.Feature feature);
        TestResult GetScenarioOutlineResult(Pickles.Parser.ScenarioOutline scenarioOutline);
        TestResult GetScenarioResult(Pickles.Parser.Scenario scenario);
    }
}
