#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System;
using System.IO.Abstractions;
using System.Linq;

using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.TestFrameworks
{
  public class NUnitResults : MultipleTestResults
  {
    private static readonly XDocumentLoader DocumentLoader = new XDocumentLoader();

    public NUnitResults(Configuration configuration, NUnitExampleSignatureBuilder exampleSignatureBuilder)
      : base(configuration)
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

    protected override ITestResults ConstructSingleTestResult(FileInfoBase fileInfo)
    {
      return new NUnitSingleResults(DocumentLoader.Load(fileInfo));
    }

    public TestResult GetExampleResult(ScenarioOutline scenarioOutline, string[] arguments)
    {
      var results = TestResults.OfType<NUnitSingleResults>().Select(tr => tr.GetExampleResult(scenarioOutline, arguments)).ToArray();

      return EvaluateTestResults(results);
    }
  }
}