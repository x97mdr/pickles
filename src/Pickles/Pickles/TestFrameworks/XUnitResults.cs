//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="XUnitResults.cs" company="PicklesDoc">
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

using System.IO.Abstractions;
using System.Linq;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.TestFrameworks
{
  public class XUnitResults : MultipleTestResults
  {
    private static readonly XDocumentLoader DocumentLoader = new XDocumentLoader();

    public XUnitResults(Configuration configuration, xUnitExampleSignatureBuilder exampleSignatureBuilder)
      : base(true, configuration)
    {
      this.SetExampleSignatureBuilder(exampleSignatureBuilder);
    }

    public void SetExampleSignatureBuilder(xUnitExampleSignatureBuilder exampleSignatureBuilder)
    {
      foreach (var testResult in TestResults.OfType<XUnitSingleResults>())
      {
        testResult.ExampleSignatureBuilder = exampleSignatureBuilder;
      }
    }

    protected override ITestResults ConstructSingleTestResult(FileInfoBase fileInfo)
    {
      return new XUnitSingleResults(DocumentLoader.Load(fileInfo));
    }

    public override TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] arguments)
    {
      var results = TestResults.OfType<XUnitSingleResults>().Select(tr => tr.GetExampleResult(scenarioOutline, arguments)).ToArray();

      return EvaluateTestResults(results);
    }
  }
}