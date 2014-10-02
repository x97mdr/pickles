using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;
using NFluent;

using TextWriter = System.IO.TextWriter;
using StringWriter = System.IO.StringWriter;

namespace PicklesDoc.Pickles.Test
{
  [TestFixture]
  public class WhenParsingCommandLineArguments : BaseFixture
  {
    private const string ExpectedHelpString = @"  -f, --feature-directory=VALUE
                             directory to start scanning recursively for 
                               features
  -o, --output-directory=VALUE
                             directory where output files will be placed
      --trfmt, --test-results-format=VALUE
                             the format of the linked test results 
                               (nunit|xunit)
      --lr, --link-results-file=VALUE
                             the path to the linked test results file (can be 
                               a semicolon-separated list of files)
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

    private const string CurrentDirectory = @"C:\Users\drombauts\AppData\Local\Temp"; // this is the default current directory of MockFileSystem

    private static readonly string ExpectedVersionString =
        string.Format(@"Pickles version {0}", Assembly.GetExecutingAssembly().GetName().Version);

    [Test]
    public void ThenCanParseExcelDocumentationFormatWithLongFormSuccessfully()
    {
      var args = new[] { @"-documentation-format=excel" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.DocumentationFormat).IsEqualTo(DocumentationFormat.Excel);
    }

    [Test]
    public void ThenCanParseExcelDocumentationFormatWithShortFormSuccessfully()
    {
      var args = new[] { @"-df=excel" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.DocumentationFormat).IsEqualTo(DocumentationFormat.Excel);
    }

    [Test]
    public void ThenCanParseHelpRequestWithLongFormSuccessfully()
    {
      var args = new[] { @"--help" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);


      var actual = RetrieveString(writer);
      Check.That(actual).Contains(ExpectedHelpString.ComparisonNormalize());
      Check.That(shouldContinue).IsFalse();
    }

    private static string RetrieveString(StringWriter writer)
    {
      return writer.GetStringBuilder().ToString().ComparisonNormalize();
    }

    [Test]
    public void ThenCanParseHelpRequestWithQuestionMarkSuccessfully()
    {
      var args = new[] { @"-?" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      var actual = RetrieveString(writer);
      Check.That(actual).Contains(ExpectedHelpString.ComparisonNormalize());
      Check.That(shouldContinue).IsFalse();
    }

    [Test]
    public void ThenCanParseHelpRequestWithShortFormSuccessfully()
    {
      var args = new[] { @"-h" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      var actual = RetrieveString(writer);
      Check.That(actual).Contains(ExpectedHelpString.ComparisonNormalize());
      Check.That(shouldContinue).IsFalse();
    }

    [Test]
    public void ThenCanParseLongFormArgumentsSuccessfully()
    {
      var args = new[] { @"--feature-directory=c:\features", @"--output-directory=c:\features-output" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.FeatureFolder.FullName).IsEqualTo(@"c:\features");
      Check.That(configuration.OutputFolder.FullName).IsEqualTo(@"c:\features-output");
    }

    [Test]
    public void ThenCanParseResultsFileWithLongFormSuccessfully()
    {
      FileSystem.AddFile(@"c:\results.xml", "<xml />");
      var args = new[] { @"-link-results-file=c:\results.xml" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.HasTestResults).IsTrue();
      Check.That(configuration.TestResultsFile.FullName).IsEqualTo(@"c:\results.xml");
    }

    [Test]
    public void ThenCanParseResultsFileAsSemicolonSeparatedList()
    {
      FileSystem.AddFile(@"c:\results1.xml", "<xml />");
      FileSystem.AddFile(@"c:\results2.xml", "<xml />");
      var args = new[] { @"-link-results-file=c:\results1.xml;c:\results2.xml" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.HasTestResults).IsTrue();
      Check.That(configuration.TestResultsFiles.First().FullName).IsEqualTo(@"c:\results1.xml");
      Check.That(configuration.TestResultsFiles.Skip(1).First().FullName).IsEqualTo(@"c:\results2.xml");
    }

    [Test]
    public void ThenCanParseResultsFileAsSemicolonSeparatedListAndTestResultsFileContainsTheFirstElementOfTestResultsFiles()
    {
      FileSystem.AddFile(@"c:\results1.xml", "<xml />");
      FileSystem.AddFile(@"c:\results2.xml", "<xml />");
      var args = new[] { @"-link-results-file=c:\results1.xml;c:\results2.xml" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.HasTestResults).IsTrue();
      Check.That(configuration.TestResultsFile.FullName).IsEqualTo(@"c:\results1.xml");
      Check.That(configuration.TestResultsFiles.First().FullName).IsEqualTo(@"c:\results1.xml");
    }

    [Test]
    public void ThenCanParseResultsFileAsSemicolonSeparatedListThatStartsWithASemicolon()
    {
      FileSystem.AddFile(@"c:\results1.xml", "<xml />");
      var args = new[] { @"-link-results-file=;c:\results1.xml" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.HasTestResults).IsTrue();
      Check.That(configuration.TestResultsFiles.Count()).IsEqualTo(1);
      Check.That(configuration.TestResultsFiles.First().FullName).IsEqualTo(@"c:\results1.xml");
    }

    [Test]
    public void ThenCanParseResultsFileAsSemicolonSeparatedListThatEndsWithASemicolon()
    {
      FileSystem.AddFile(@"c:\results1.xml", "<xml />");
      var args = new[] { @"-link-results-file=c:\results1.xml;" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.HasTestResults).IsTrue();
      Check.That(configuration.TestResultsFiles.Count()).IsEqualTo(1);
      Check.That(configuration.TestResultsFiles.First().FullName).IsEqualTo(@"c:\results1.xml");
    }

    [Test]
    public void ThenCanParseResultsFileWithShortFormSuccessfully()
    {
      FileSystem.AddFile(@"c:\results.xml", "<xml />");
      var args = new[] { @"-lr=c:\results.xml" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.HasTestResults).IsTrue();
      Check.That(configuration.TestResultsFile.FullName).IsEqualTo(@"c:\results.xml");
    }

    [Test]
    public void ThenCanParseResultsFormatMstestWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=mstest" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.MsTest);
    }

    [Test]
    public void ThenCanParseResultsFormatMstestWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=mstest" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.MsTest);
    }

    [Test]
    public void ThenCanParseResultsFormatNunitWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=nunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.NUnit);
    }

    [Test]
    public void ThenCanParseResultsFormatNunitWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=nunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.NUnit);
    }

    [Test]
    public void ThenCanParseResultsFormatXunitWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=xunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.xUnit);
    }

    [Test]
    public void ThenCanParseResultsFormatXunitWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=xunit" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.xUnit);
    }

    [Test]
    public void ThenCanParseShortFormArgumentsSuccessfully()
    {
      var args = new[] { @"-f=c:\features", @"-o=c:\features-output" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.FeatureFolder.FullName).IsEqualTo(@"c:\features");
      Check.That(configuration.OutputFolder.FullName).IsEqualTo(@"c:\features-output");
    }

    [Test]
    public void ThenCanParseVersionRequestLongFormSuccessfully()
    {
      var args = new[] { @"--version" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      var actual = RetrieveString(writer);
      Check.That(actual).Contains(ExpectedVersionString.ComparisonNormalize());
      Check.That(shouldContinue).IsFalse();
    }

    [Test]
    public void ThenCanParseVersionRequestShortFormSuccessfully()
    {
      var args = new[] { @"-v" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

      var actual = RetrieveString(writer);
      Check.That(actual).Contains(ExpectedVersionString.ComparisonNormalize());
      Check.That(shouldContinue).IsFalse();
    }

    [Test]
    public void ThenInputAndOutputDirectoryAreSetToTheCurrentDirectory()
    {
      var args = new[] { @"-v" };

      var configuration = new Configuration();
      var writer = new StringWriter();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      commandLineArgumentParser.Parse(args, configuration, writer);

      Check.That(configuration.FeatureFolder.FullName).IsEqualTo(CurrentDirectory);
      Check.That(configuration.OutputFolder.FullName).IsEqualTo(CurrentDirectory);
    }

    [Test]
    public void ThenCanParseResultsFormatCucumberJsonWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=cucumberjson" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.CucumberJson);
    }

    [Test]
    public void ThenCanParseResultsFormatCucumberJsonWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=cucumberjson" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.CucumberJson);
    }

    [Test]
    public void ThenCanParseResultsFormatSpecrunWithLongFormSuccessfully()
    {
      var args = new[] { @"-test-results-format=specrun" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.SpecRun);
    }

    [Test]
    public void ThenCanParseResultsFormatSpecrunWithShortFormSuccessfully()
    {
      var args = new[] { @"-trfmt=specrun" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.TestResultsFormat).IsEqualTo(TestResultsFormat.SpecRun);
    }

    [Test]
    public void ThenCanFilterOutNonExistingTestResultFiles()
    {
      var args = new[] { @"-link-results-file=c:\DoesNotExist.xml;" };

      var configuration = new Configuration();
      var commandLineArgumentParser = new CommandLineArgumentParser(FileSystem);
      bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

      Check.That(shouldContinue).IsTrue();
      Check.That(configuration.HasTestResults).IsFalse();
      Check.That(configuration.TestResultsFiles).IsEmpty();
    }
  }
}