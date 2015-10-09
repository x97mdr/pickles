//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MultipleTestResults.cs" company="PicklesDoc">
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
using System.IO.Abstractions;
using System.Linq;

using PicklesDoc.Pickles.ObjectModel;

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
            get { return this.supportsExampleResults; }
        }

        protected IEnumerable<ITestResults> TestResults
        {
            get { return this.testResults; }
        }

        private IEnumerable<ITestResults> GetSingleTestResults(Configuration configuration)
        {
            ITestResults[] results;

            if (configuration.HasTestResults)
            {
                results = configuration.TestResultsFiles.Select(this.ConstructSingleTestResult).ToArray();
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
