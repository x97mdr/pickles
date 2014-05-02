using System;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.TestFrameworks;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenResolvingTestResults : BaseFixture
    {
        private const string TestResultsResourcePrefix = "PicklesDoc.Pickles.Test.";

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
        public void ThenCanResolveAsSingletonWhenTestResultsAreMsTest()
        {
            FileSystem.AddFile("results-example-mstest.trx", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-mstest.trx"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.MsTest;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-mstest.trx") };

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
            FileSystem.AddFile("results-example-nunit.xml", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-nunit.xml"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.NUnit;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-nunit.xml") };

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
            FileSystem.AddFile("results-example-xunit.xml", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-xunit.xml"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.xUnit;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-xunit.xml") };

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
            FileSystem.AddFile("results-example-json.json", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-json.json"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.CucumberJson;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-json.json") };

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
            FileSystem.AddFile("results-example-specrun.html", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-specrun.html"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.SpecRun;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-specrun.html") };

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
        public void ThenCanResolveWhenTestResultsAreMsTest()
        {
            FileSystem.AddFile("results-example-mstest.trx", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-mstest.trx"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.MsTest;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-mstest.trx") };

            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<MsTestResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsAreNUnit()
        {
           FileSystem.AddFile("results-example-nunit.xml", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-nunit.xml"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.NUnit;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-nunit.xml") };

            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<NUnitResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsArexUnit()
        {
            FileSystem.AddFile("results-example-xunit.xml", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-xunit.xml"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.xUnit;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-xunit.xml") };

            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<XUnitResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsAreCucumberJson()
        {
            FileSystem.AddFile("results-example-json.json", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-json.json"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.CucumberJson;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-json.json") };

            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<CucumberJsonResults>(item);
        }

        [Test]
        public void ThenCanResolveWhenTestResultsAreSpecrun()
        {
            FileSystem.AddFile("results-example-specrun.html", RetrieveContentOfFileFromResources(TestResultsResourcePrefix + "results-example-specrun.html"));

            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFormat = TestResultsFormat.SpecRun;
            configuration.TestResultsFiles = new[] { FileSystem.FileInfo.FromFileName("results-example-specrun.html") };

            var item = Container.Resolve<ITestResults>();

            Assert.NotNull(item);
            Assert.IsInstanceOf<SpecRunResults>(item);
        }
    }
}