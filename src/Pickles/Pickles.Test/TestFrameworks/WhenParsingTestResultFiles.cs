using System;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Reflection;
using Autofac;

using StreamReader = System.IO.StreamReader;

namespace PicklesDoc.Pickles.Test
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
          foreach (var fileName in resultsFileNames)
          {
              // Write out the embedded test results file
              using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + fileName)))
              {
                  FileSystem.AddFile(fileName, new MockFileData(input.ReadToEnd()));
              }
          }

            var configuration = Container.Resolve<Configuration>();

            configuration.TestResultsFiles = resultsFileNames.Select(f => FileSystem.FileInfo.FromFileName(f)).ToArray();
        }
    }
}