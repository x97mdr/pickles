using System;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Reflection;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;
using Should;

using TextWriter = System.IO.TextWriter;
using StringWriter = System.IO.StringWriter;

namespace PicklesDoc.Pickles.Test
{
  [TestFixture]
  public class WhenParsingCommandLineArguments : BaseFixture
  {
    private const string expectedHelpString = @"  -f, --feature-directory=VALUE
                             directory to start scanning recursively for 
                               features
  -o, --output-directory=VALUE
                             directory where output files will be placed
      --trfmt, --test-results-format=VALUE
                             the format of the linked test results 
                               (nunit|xunit)
      --lr, --link-results-file=VALUE
                             the path to the linked test results file
      --sn, --system-under-test-name=VALUE
                             a file containing the results of testing the 
                               features
      --sv, --system-under-test-version=VALUE
                             the name of the system under test
  -l, --language=VALUE       the language of the feature files
      --df, --documentation-format=VALUE
                             the format of the output documentation
  -v, --version              
  -h, -?, --help";

    private const string CurrentDirectory = @"C:\Foo\Bar"; // this is the default current directory of MockFileSystem

    private static readonly string expectedVersionString =
        string.Format(@"Pickles version {0}", Assembly.GetExecutingAssembly().GetName().Version);

    [Test]
    public void ThenCanParseExcelDocumentationFormatWithLongFormSuccessfully()
    {
      var args = new[] { @"-documentation-format=excel" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      configuration.DocumentationFormat.ShouldEqual(DocumentationFormat.Excel);
    }

    [Test]
    public void ThenCanParseExcelDocumentationFormatWithShortFormSuccessfully()
    {
      var args = new[] { @"-df=excel" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      configuration.DocumentationFormat.ShouldEqual(DocumentationFormat.Excel);
    }

    [Test]
    public void ThenCanParseHelpRequestWithLongFormSuccessfully()
    {
      var args = new[] { @"--help" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);


      StringAssert.Contains(expectedHelpString.ComparisonNormalize(),
                            writer.GetStringBuilder().ToString().ComparisonNormalize());
      shouldContinue.ShouldBeFalse();
    }

    [Test]
    public void ThenCanParseHelpRequestWithQuestionMarkSuccessfully()
    {
      var args = new[] { @"-?" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      StringAssert.Contains(expectedHelpString.ComparisonNormalize(),
                            writer.GetStringBuilder().ToString().ComparisonNormalize());
      shouldContinue.ShouldBeFalse();
    }

    [Test]
    public void ThenCanParseHelpRequestWithShortFormSuccessfully()
    {
      var args = new[] { @"-h" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      StringAssert.Contains(expectedHelpString.ComparisonNormalize(),
                            writer.GetStringBuilder().ToString().ComparisonNormalize());
      shouldContinue.ShouldBeFalse();
    }

    [Test]
    public void ThenCanParseLongFormArgumentsSuccessfully()
    {
      var args = new[] { @"--feature-directory=c:\features", @"--output-directory=c:\features-output" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(@"c:\features", configuration.FeatureFolder.FullName);
      Assert.AreEqual(@"c:\features-output", configuration.OutputFolder.FullName);
    }

    [Test]
    public void ThenCanParseResultsFileWithLongFormSuccessfully()
    {
      var args = new[] { @"-link-results-file=c:\results.xml" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      configuration.HasTestResults.ShouldBeTrue();
      Assert.AreEqual(@"c:\results.xml", configuration.TestResultsFile.FullName);
    }

    [Test]
    public void ThenCanParseResultsFileWithShortFormSuccessfully()
    {
      var args = new[] { @"-lr=c:\results.xml" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      configuration.HasTestResults.ShouldBeTrue();
      Assert.AreEqual(@"c:\results.xml", configuration.TestResultsFile.FullName);
    }

    [Test]
    public void ThenCanParseResultsFormatMstestWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=mstest" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.MsTest, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatMstestWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=mstest" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.MsTest, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatNunitWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=nunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.NUnit, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatNunitWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=nunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.NUnit, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatXunitWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=xunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.xUnit, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatXunitWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=xunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.xUnit, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseShortFormArgumentsSuccessfully()
    {
      var args = new[] { @"-f=c:\features", @"-o=c:\features-output" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(@"c:\features", configuration.FeatureFolder.FullName);
      Assert.AreEqual(@"c:\features-output", configuration.OutputFolder.FullName);
    }

    [Test]
    public void ThenCanParseVersionRequestLongFormSuccessfully()
    {
      var args = new[] { @"--version" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      StringAssert.IsMatch(expectedVersionString.ComparisonNormalize(),
                           writer.GetStringBuilder().ToString().ComparisonNormalize());
      shouldContinue.ShouldBeFalse();
    }

    [Test]
    public void ThenCanParseVersionRequestShortFormSuccessfully()
    {
      var args = new[] { @"-v" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      StringAssert.IsMatch(expectedVersionString.ComparisonNormalize(),
                           writer.GetStringBuilder().ToString().ComparisonNormalize());
      shouldContinue.ShouldBeFalse();
    }

    [Test]
    public void ThenInputAndOutputDirectoryAreSetToTheCurrentDirectory()
    {
      var args = new[] { @"-v" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      commandLineArgumentParser.Parse(args, configuration, writer);

      Assert.AreEqual(CurrentDirectory, configuration.FeatureFolder.FullName);
      Assert.AreEqual(CurrentDirectory, configuration.OutputFolder.FullName);
    }

    [Test]
    public void ThenCanParseResultsFormatCucumberJsonWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=cucumberjson" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.CucumberJson, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatCucumberJsonWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=cucumberjson" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.CucumberJson, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatSpecrunWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=specrun" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.SpecRun, configuration.TestResultsFormat);
    }

    [Test]
    public void ThenCanParseResultsFormatSpecrunWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=specrun" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      shouldContinue.ShouldBeTrue();
      Assert.AreEqual(TestResultsFormat.SpecRun, configuration.TestResultsFormat);
    }
  }
}