using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.XUnit
{
    public abstract class XUnitSingleResultsBase : ITestResults
    {
        internal XUnitExampleSignatureBuilder ExampleSignatureBuilder { get; set; }

        public abstract bool SupportsExampleResults { get; }

        public abstract TestResult GetFeatureResult(Feature feature);

        public abstract TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline);

        public abstract TestResult GetScenarioResult(Scenario scenario);

        public abstract TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues);
    }
}