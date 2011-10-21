using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenParsingCommandLineArguments
    {
        private static readonly string expectedHelpString = @"Pickles version 1.0.0.0
  -f, --feature-directory=VALUE
                             directory to start scanning recursively for 
                               features
  -o, --output-directory=VALUE
                             directory where output files will be placed
  -v, --version              
  -h, -?, --help";

        private static readonly string expectedVersionString = @"Pickles version 1.0.0.0";

        [Test]
        public void Then_can_parse_short_form_arguments_successfully()
        {
            var args = new string[] { @"-f=c:\features", @"-o=c:\features-output" };

            var configuration = new Configuration();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

            Assert.AreEqual(true, shouldContinue);
            Assert.AreEqual(@"c:\features", configuration.FeatureFolder.FullName);
            Assert.AreEqual(@"c:\features-output", configuration.OutputFolder.FullName);
        }

        [Test]
        public void Then_can_parse_long_form_arguments_successfully()
        {
            var args = new string[] { @"--feature-directory=c:\features", @"--output-directory=c:\features-output" };

            var configuration = new Configuration();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, TextWriter.Null);

            Assert.AreEqual(true, shouldContinue);
            Assert.AreEqual(@"c:\features", configuration.FeatureFolder.FullName);
            Assert.AreEqual(@"c:\features-output", configuration.OutputFolder.FullName);
        }

        [Test]
        public void Then_can_parse_help_request_with_question_mark_successfully()
        {
            var args = new string[] { @"-?" };

            var configuration = new Configuration();
            var writer = new StringWriter();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

            StringAssert.AreEqualIgnoringCase(expectedHelpString, writer.GetStringBuilder().ToString().Trim());
            Assert.AreEqual(false, shouldContinue);
            Assert.AreEqual(null, configuration.FeatureFolder);
            Assert.AreEqual(null, configuration.OutputFolder);
        }

        [Test]
        public void Then_can_parse_help_request_with_short_form_successfully()
        {
            var args = new string[] { @"-h" };

            var configuration = new Configuration();
            var writer = new StringWriter();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

            StringAssert.AreEqualIgnoringCase(expectedHelpString, writer.GetStringBuilder().ToString().Trim());
            Assert.AreEqual(false, shouldContinue);
            Assert.AreEqual(null, configuration.FeatureFolder);
            Assert.AreEqual(null, configuration.OutputFolder);
        }

        [Test]
        public void Then_can_parse_help_request_with_long_form_successfully()
        {
            var args = new string[] { @"--help" };

            var configuration = new Configuration();
            var writer = new StringWriter();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

            StringAssert.AreEqualIgnoringCase(expectedHelpString, writer.GetStringBuilder().ToString().Trim());
            Assert.AreEqual(false, shouldContinue);
            Assert.AreEqual(null, configuration.FeatureFolder);
            Assert.AreEqual(null, configuration.OutputFolder);
        }

        [Test]
        public void Then_can_parse_version_request_short_form_successfully()
        {
            var args = new string[] { @"-v" };

            var configuration = new Configuration();
            var writer = new StringWriter();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

            StringAssert.AreEqualIgnoringCase(expectedVersionString, writer.GetStringBuilder().ToString().Trim());
            Assert.AreEqual(false, shouldContinue);
            Assert.AreEqual(null, configuration.FeatureFolder);
            Assert.AreEqual(null, configuration.OutputFolder);
        }

        [Test]
        public void Then_can_parse_version_request_long_form_successfully()
        {
            var args = new string[] { @"--version" };

            var configuration = new Configuration();
            var writer = new StringWriter();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, writer);

            StringAssert.AreEqualIgnoringCase(expectedVersionString, writer.GetStringBuilder().ToString().Trim());
            Assert.AreEqual(false, shouldContinue);
            Assert.AreEqual(null, configuration.FeatureFolder);
            Assert.AreEqual(null, configuration.OutputFolder);
        }
    }
}
