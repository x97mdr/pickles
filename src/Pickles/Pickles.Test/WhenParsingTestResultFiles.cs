using System;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using Autofac;

using StreamReader = System.IO.StreamReader;

namespace PicklesDoc.Pickles.Test
{
    public abstract class WhenParsingTestResultFiles<TResults> : BaseFixture
    {
        private readonly string resultsFileName;

        protected WhenParsingTestResultFiles(string resultsFileName)
        {
            this.resultsFileName = resultsFileName;
        }

        protected TResults ParseResultsFile()
        {
            this.AddTestResultsToConfiguration();

            var results = Container.Resolve<TResults>();
            return results;
        }

        private void AddTestResultsToConfiguration()
        {
            // Write out the embedded test results file
            using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFileName)))
            {
                MockFileSystem.AddFile(resultsFileName, new MockFileData(input.ReadToEnd()));
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFileName);
        }
    }
}