//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="SingleTestRunBase.cs" company="PicklesDoc">
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

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks
{
    public abstract class SingleTestRunBase : ITestResults
    {
        public abstract bool SupportsExampleResults { get; }

        internal IScenarioOutlineExampleMatcher ScenarioOutlineExampleMatcher { get; set; }

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
    }
}