using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks
{
    public abstract class SingleTestRunBase : ITestResults
    {
        public abstract bool SupportsExampleResults { get; }

        internal IExampleSignatureBuilder ExampleSignatureBuilder { get; set; }

        public abstract TestResult GetFeatureResult(Feature feature);

        public abstract TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline);

        public abstract TestResult GetScenarioResult(Scenario scenario);

        public abstract TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues);

        protected TestResult GetAggregateResult(int passedCount, int failedCount, int skippedCount)
        {
            TestResult result;
            if (passedCount > 0 && failedCount == 0)
            {
                result = TestResult.Passed;
            }
            else if (failedCount > 0)
            {
                result = TestResult.Failed;
            }
            else
            {
                result = TestResult.Inconclusive;
            }

            return result;
        }

        protected TestResult DetermineAggregateResult(IEnumerable<TestResult> exampleResults)
        {
            int passedCount = 0;
            int failedCount = 0;
            int skippedCount = 0;

            foreach (TestResult result in exampleResults)
            {
                if (result == TestResult.Inconclusive)
                {
                    skippedCount++;
                }

                if (result == TestResult.Passed)
                {
                    passedCount++;
                }

                if (result == TestResult.Failed)
                {
                    failedCount++;
                }
            }

            return this.GetAggregateResult(passedCount, failedCount, skippedCount);
        }

        protected Regex CreateSignatureRegex(ScenarioOutline scenarioOutline, string[] exampleValues)
        {
            var signatureBuilder = this.ExampleSignatureBuilder;

            if (signatureBuilder == null)
            {
                throw new InvalidOperationException(
                    "You need to set the ExampleSignatureBuilder before using GetExampleResult.");
            }

            Regex signature = signatureBuilder.Build(scenarioOutline, exampleValues);
            return signature;
        }
    }
}