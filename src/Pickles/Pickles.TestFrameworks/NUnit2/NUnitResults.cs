//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NUnitResults.cs" company="PicklesDoc">
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
using System.IO.Abstractions;
using System.Linq;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.NUnit2
{
    public class NUnitResults : MultipleTestResults
    {
        private readonly NUnit2SingleResultLoader singleResultLoader = new NUnit2SingleResultLoader();

        public NUnitResults(IConfiguration configuration, NUnit2SingleResultLoader singleResultLoader, NUnitExampleSignatureBuilder exampleSignatureBuilder)
            : base(true, configuration, singleResultLoader)
        {
            this.SetExampleSignatureBuilder(exampleSignatureBuilder);
        }

        public void SetExampleSignatureBuilder(NUnitExampleSignatureBuilder exampleSignatureBuilder)
        {
            foreach (var testResult in TestResults.OfType<NUnitSingleResults>())
            {
                testResult.ExampleSignatureBuilder = exampleSignatureBuilder;
            }
        }

        public override TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] arguments)
        {
            var results = TestResults.OfType<NUnitSingleResults>().Select(tr => tr.GetExampleResult(scenarioOutline, arguments)).ToArray();

            return EvaluateTestResults(results);
        }

        protected override ITestResults ConstructSingleTestResult(FileInfoBase fileInfo)
        {
            return this.singleResultLoader.Load(fileInfo);
        }
    }
}
