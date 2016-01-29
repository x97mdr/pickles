using System.Linq;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks
{
    public abstract class MultipleTestRunsBase<TSingleTestRun> : MultipleTestResults
        where TSingleTestRun : SingleTestRunBase
    {
        protected MultipleTestRunsBase(IConfiguration configuration, ISingleResultLoader singleResultLoader, IExampleSignatureBuilder exampleSignatureBuilder)
            : base(true, configuration, singleResultLoader)
        {
            this.SetExampleSignatureBuilder(exampleSignatureBuilder);
        }

        public void SetExampleSignatureBuilder(IExampleSignatureBuilder exampleSignatureBuilder)
        {
            foreach (var testResult in TestResults.OfType<TSingleTestRun>())
            {
                testResult.ExampleSignatureBuilder = exampleSignatureBuilder;
            }
        }

        public override TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] arguments)
        {
            var results = TestResults.OfType<TSingleTestRun>().Select(tr => tr.GetExampleResult(scenarioOutline, arguments)).ToArray();

            return EvaluateTestResults(results);
        }
    }
}