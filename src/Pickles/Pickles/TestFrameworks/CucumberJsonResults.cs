using System;
using System.IO.Abstractions;

namespace PicklesDoc.Pickles.TestFrameworks
{
    public class CucumberJsonResults : MultipleTestResults
    {
        public CucumberJsonResults(Configuration configuration)
          : base(configuration)
        {
        }

        protected override ITestResults ConstructSingleTestResult(FileInfoBase fileInfo)
        {
          return new CucumberJsonSingleResults(fileInfo);
        }
    }
}
