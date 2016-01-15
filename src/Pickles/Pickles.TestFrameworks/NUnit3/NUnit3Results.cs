//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NUnit3Results.cs" company="PicklesDoc">
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

using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.NUnit3
{
    public class NUnit3Results : MultipleTestResults
    {
        public NUnit3Results(IEnumerable<ITestResults> testResults)
            : base(true, testResults)
        {
        }

        public NUnit3Results(IConfiguration configuration)
            : base(true, configuration)
        {
        }

        public void SetExampleSignatureBuilder(NUnit3ExampleSignatureBuilder exampleSignatureBuilder)
        {
            foreach (var testResult in TestResults.OfType<NUnit3SingleResult>())
            {
                testResult.ExampleSignatureBuilder = exampleSignatureBuilder;
            }
        }

        public override TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] arguments)
        {
            var results = TestResults.OfType<NUnit3SingleResult>().Select(tr => tr.GetExampleResult(scenarioOutline, arguments)).ToArray();

            return EvaluateTestResults(results);
        }

        protected override ITestResults ConstructSingleTestResult(FileInfoBase fileInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}