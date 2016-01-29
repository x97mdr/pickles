using System.Linq;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.XUnit
{
    public abstract class XUnitResultsBase<TSingleResult> : MultipleTestResults
        where TSingleResult : XUnitSingleResultsBase
    {
        protected XUnitResultsBase(IConfiguration configuration, ISingleResultLoader singleResultLoader, IExampleSignatureBuilder exampleSignatureBuilder)
            : base(true, configuration, singleResultLoader)
        {
            this.SetExampleSignatureBuilder(exampleSignatureBuilder);
        }

        public void SetExampleSignatureBuilder(IExampleSignatureBuilder exampleSignatureBuilder)
        {
            foreach (var testResult in TestResults.OfType<TSingleResult>())
            {
                testResult.ExampleSignatureBuilder = exampleSignatureBuilder;
            }
        }

        public override TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] arguments)
        {
            var results = TestResults.OfType<TSingleResult>().Select(tr => tr.GetExampleResult(scenarioOutline, arguments)).ToArray();

            return EvaluateTestResults(results);
        }
    }
}