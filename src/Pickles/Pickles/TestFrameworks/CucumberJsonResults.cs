using System;
using System.IO.Abstractions;

using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.TestFrameworks
{
    public class CucumberJsonResults : MultipleTestResults
    {
        public CucumberJsonResults(Configuration configuration)
          : base(false, configuration)
        {
        }

      public override TestResult GetExampleResult(ScenarioOutline scenario, string[] exampleValues)
      {
        throw new NotSupportedException();
      }

      protected override ITestResults ConstructSingleTestResult(FileInfoBase fileInfo)
        {
          return new CucumberJsonSingleResults(fileInfo);
        }
    }
}
