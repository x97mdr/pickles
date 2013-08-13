using System;
using System.Reflection;
using Autofac;

using StreamReader = System.IO.StreamReader;
using StreamWriter = System.IO.StreamWriter;

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
                using (var output = new StreamWriter(resultsFileName))
                {
                    output.Write(input.ReadToEnd());
                }
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFile = RealFileSystem.FileInfo.FromFileName(resultsFileName);
        }
    }
}