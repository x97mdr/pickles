using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Reflection;

using Autofac;

namespace PicklesDoc.Pickles.Test.TestFrameworks
{
    public abstract class WhenParsingTestResultFiles<TResults> : BaseFixture
    {
        private readonly string[] resultsFileNames;

        protected WhenParsingTestResultFiles(string resultsFileName)
        {
            this.resultsFileNames = resultsFileName.Split(';');
        }

        protected TResults ParseResultsFile()
        {
            this.AddTestResultsToConfiguration();

            var results = Container.Resolve<TResults>();
            return results;
        }

        protected void AddTestResultsToConfiguration()
        {
          foreach (var fileName in this.resultsFileNames)
          {
              // Write out the embedded test results file
              using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + fileName)))
              {
                  FileSystem.AddFile(fileName, new MockFileData(input.ReadToEnd()));
              }
          }

            var configuration = Container.Resolve<Configuration>();

            configuration.TestResultsFiles = this.resultsFileNames.Select(f => FileSystem.FileInfo.FromFileName(f)).ToArray();
        }
    }
}