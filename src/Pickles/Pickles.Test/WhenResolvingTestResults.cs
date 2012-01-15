using System.IO;
using Ninject;
using NUnit.Framework;
using Pickles.TestFrameworks;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenResolvingTestResults : BaseFixture
    {
        [Test]
        public void ThenCanResolveWhenNoTestResultsSelected()
        {
            var item = Kernel.Get<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<NullTestResults>(item);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenNoTestResultsSelected()
        {
            var item1 = Kernel.Get<ITestResults>();
            var item2 = Kernel.Get<ITestResults>();

            Assert.AreSame(item1, item2);
            Assert.NotNull(item1);
            Assert.IsInstanceOf<NullTestResults>(item1);
            Assert.NotNull(item2);
            Assert.IsInstanceOf<NullTestResults>(item2);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsAreNUnit()
        {
            const string resultsFilename = "results-example-nunit.xml";
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + resultsFilename)))
            using (var output = new StreamWriter(resultsFilename))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.NUnit;
            configuration.TestResultsFile = new System.IO.FileInfo(resultsFilename);

            var item = Kernel.Get<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<NUnitResults>(item);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenTestResultsAreNUnit()
        {
            const string resultsFilename = "results-example-nunit.xml";
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + resultsFilename)))
            using (var output = new StreamWriter(resultsFilename))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.NUnit;
            configuration.TestResultsFile = new System.IO.FileInfo(resultsFilename);

            var item1 = Kernel.Get<ITestResults>();
            var item2 = Kernel.Get<ITestResults>();

            Assert.AreSame(item1, item2);
            Assert.NotNull(item1);
            Assert.IsInstanceOf<NUnitResults>(item1);
            Assert.NotNull(item2);
            Assert.IsInstanceOf<NUnitResults>(item2);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsArexUnit()
        {
            const string resultsFilename = "results-example-xunit.xml";
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + resultsFilename)))
            using (var output = new StreamWriter(resultsFilename))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.xUnit;
            configuration.TestResultsFile = new System.IO.FileInfo(resultsFilename);

            var item = Kernel.Get<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<XUnitResults>(item);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenTestResultsArexUnit()
        {
            const string resultsFilename = "results-example-xunit.xml";
            using (var input = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Pickles.Test." + resultsFilename)))
            using (var output = new StreamWriter(resultsFilename))
            {
                output.Write(input.ReadToEnd());
            }

            var configuration = Kernel.Get<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.xUnit;
            configuration.TestResultsFile = new System.IO.FileInfo(resultsFilename);

            var item1 = Kernel.Get<ITestResults>();
            var item2 = Kernel.Get<ITestResults>();

            Assert.AreSame(item1, item2);
            Assert.NotNull(item1);
            Assert.IsInstanceOf<XUnitResults>(item1);
            Assert.NotNull(item2);
            Assert.IsInstanceOf<XUnitResults>(item2);
        }
    }
}
