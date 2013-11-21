using System;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.TestFrameworks;
using Should;

using StreamReader = System.IO.StreamReader;
using StreamWriter = System.IO.StreamWriter;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenResolvingTestResults : BaseFixture
    {
        [Test]
        public void ThenCanResolveAsSingletonWhenNoTestResultsSelected()
        {
            var item1 = Container.Resolve<ITestResults>();
            var item2 = Container.Resolve<ITestResults>();

            item1.ShouldNotBeNull();
            item1.ShouldBeType<NullTestResults>();
            item2.ShouldNotBeNull();
            item2.ShouldBeType<NullTestResults>();
            item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenTestResultsAreMSTest()
        {
            const string resultsFilename = "results-example-mstest.trx";
            using (
                var input =
                    new StreamReader(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
            {
              MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.MsTest;
            configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

            var item1 = Container.Resolve<ITestResults>();
            var item2 = Container.Resolve<ITestResults>();

            item1.ShouldNotBeNull();
            item1.ShouldBeType<MsTestResults>();
            item2.ShouldNotBeNull();
            item2.ShouldBeType<MsTestResults>();
            item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenTestResultsAreNUnit()
        {
            const string resultsFilename = "results-example-nunit.xml";
            using (
                var input =
                    new StreamReader(
                        Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
            {
              MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.NUnit;
            configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

            var item1 = Container.Resolve<ITestResults>();
            var item2 = Container.Resolve<ITestResults>();

            item1.ShouldNotBeNull();
            item1.ShouldBeType<NUnitResults>();
            item2.ShouldNotBeNull();
            item2.ShouldBeType<NUnitResults>();
            item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenTestResultsArexUnit()
        {
          const string resultsFilename = "results-example-xunit.xml";
          using (
              var input =
                  new StreamReader(
                      Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
          {
            MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
          }

          var configuration = Container.Resolve<Configuration>();
          configuration.TestResultsFormat = TestResultsFormat.xUnit;
          configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

          var item1 = Container.Resolve<ITestResults>();
          var item2 = Container.Resolve<ITestResults>();

          item1.ShouldNotBeNull();
          item1.ShouldBeType<XUnitResults>();
          item2.ShouldNotBeNull();
          item2.ShouldBeType<XUnitResults>();
          item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenTestResultsAreCucumberJson()
        {
          const string resultsFilename = "results-example-json.json";
          using (
              var input =
                  new StreamReader(
                      Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
          {
            MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
          }

          var configuration = Container.Resolve<Configuration>();
          configuration.TestResultsFormat = TestResultsFormat.CucumberJson;
          configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

          var item1 = Container.Resolve<ITestResults>();
          var item2 = Container.Resolve<ITestResults>();

          item1.ShouldNotBeNull();
          item1.ShouldBeType<CucumberJsonResults>();
          item2.ShouldNotBeNull();
          item2.ShouldBeType<CucumberJsonResults>();
          item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveAsSingletonWhenTestResultsAreSpecrun()
        {
          const string resultsFilename = "results-example-specrun.html";
          using (
              var input =
                  new StreamReader(
                      Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
          {
            MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
          }

          var configuration = Container.Resolve<Configuration>();
          configuration.TestResultsFormat = TestResultsFormat.SpecRun;
          configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

          var item1 = Container.Resolve<ITestResults>();
          var item2 = Container.Resolve<ITestResults>();

          item1.ShouldNotBeNull();
          item1.ShouldBeType<SpecRunResults>();
          item2.ShouldNotBeNull();
          item2.ShouldBeType<SpecRunResults>();
          item1.ShouldBeSameAs(item2);
        }

        [Test]
        public void ThenCanResolveWhenNoTestResultsSelected()
        {
            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<NullTestResults>(item);
        }


        [Test]
        public void ThenCanResolveWhenTestResultsAreMSTest()
        {
            const string resultsFilename = "results-example-mstest.trx";
            using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
            {
              MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.MsTest;
            configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<MsTestResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsAreNUnit()
        {
            const string resultsFilename = "results-example-nunit.xml";
            using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
            {
              MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
            }

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.NUnit;
            configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<NUnitResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsArexUnit()
        {
          const string resultsFilename = "results-example-xunit.xml";
          using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
          {
            MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
          }

          var configuration = Container.Resolve<Configuration>();
          configuration.TestResultsFormat = TestResultsFormat.xUnit;
          configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

          var item = Container.Resolve<ITestResults>();

          Assert.NotNull(item);
          Assert.IsInstanceOf<XUnitResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsAreCucumberJson()
        {
          const string resultsFilename = "results-example-json.json";
          using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
          {
            MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
          }

          var configuration = Container.Resolve<Configuration>();
          configuration.TestResultsFormat = TestResultsFormat.CucumberJson;
          configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

          var item = Container.Resolve<ITestResults>();

          Assert.NotNull(item);
          Assert.IsInstanceOf<CucumberJsonResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsAreSpecrun()
        {
          const string resultsFilename = "results-example-specrun.html";
          using (var input = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("PicklesDoc.Pickles.Test." + resultsFilename)))
          {
              MockFileSystem.AddFile(resultsFilename, new MockFileData(input.ReadToEnd()));
          }

          var configuration = Container.Resolve<Configuration>();
          configuration.TestResultsFormat = TestResultsFormat.SpecRun;
          configuration.TestResultsFile = MockFileSystem.FileInfo.FromFileName(resultsFilename);

          var item = Container.Resolve<ITestResults>();

          Assert.NotNull(item);
          Assert.IsInstanceOf<SpecRunResults>(item);
        }
    }
}