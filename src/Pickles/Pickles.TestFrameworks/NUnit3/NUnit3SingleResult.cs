//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="NUnit3SingleResult.cs" company="PicklesDoc">
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

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.TestFrameworks.NUnit3
{
    public class NUnit3SingleResult : ITestResults
    {
        public bool SupportsExampleResults
        {
            get { return true; }
        }

        internal NUnit3ExampleSignatureBuilder ExampleSignatureBuilder { get; set; }

        public TestResult GetFeatureResult(Feature feature)
        {
            throw new System.NotImplementedException();
        }

        public TestResult GetScenarioOutlineResult(ScenarioOutline scenarioOutline)
        {
            throw new System.NotImplementedException();
        }

        public TestResult GetScenarioResult(Scenario scenario)
        {
            throw new System.NotImplementedException();
        }

        public TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues)
        {
            throw new System.NotImplementedException();
        }
    }
}