#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

#if __MonoCS__
#else
using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using Autofac;

namespace PicklesDoc.Pickles.PowerShell
{
    [Cmdlet("Pickle", "Features")]
    public class Pickle_Features : PSCmdlet
    {
        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_FEATURE_DIR, Mandatory = true)]
        public string FeatureDirectory { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_OUTPUT_DIR, Mandatory = true)]
        public string OutputDirectory { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_LANGUAGE_FEATURE_FILES, Mandatory = false)]
        public string Language { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_TEST_RESULTS_FORMAT, Mandatory = false)]
        public string TestResultsFormat { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_TEST_RESULTS_FILE, Mandatory = false)]
        public string TestResultsFile { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_SUT_NAME, Mandatory = false)]
        public string SystemUnderTestName { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_SUT_VERSION, Mandatory = false)]
        public string SystemUnderTestVersion { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_DOCUMENTATION_FORMAT, Mandatory = false)]
        public string DocumentationFormat { get; set; }

        protected override void ProcessRecord()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(Runner).Assembly);
            builder.RegisterModule<PicklesModule>();
            var container = builder.Build();

            var configuration = container.Resolve<Configuration>();

            this.ParseParameters(configuration);

            WriteObject(string.Format("Pickles v.{0}{1}", Assembly.GetExecutingAssembly().GetName().Version,
                                      Environment.NewLine));
            new ConfigurationReporter().ReportOn(configuration, message => WriteObject(message));

            var runner = container.Resolve<Runner>();
            runner.Run(container);

            WriteObject(string.Format("Pickles completed successfully"));
        }

        private void ParseParameters(Configuration configuration)
        {
            configuration.FeatureFolder = new DirectoryInfo(this.FeatureDirectory);
            configuration.OutputFolder = new DirectoryInfo(this.OutputDirectory);
            if (!string.IsNullOrEmpty(this.TestResultsFormat))
            {
                configuration.TestResultsFormat =
                    (TestResultsFormat) Enum.Parse(typeof (TestResultsFormat), this.TestResultsFormat, true);
            }
            if (!string.IsNullOrEmpty(this.TestResultsFile))
            {
                configuration.TestResultsFile = new FileInfo(this.TestResultsFile);
            }
            configuration.SystemUnderTestName = this.SystemUnderTestName;
            configuration.SystemUnderTestVersion = this.SystemUnderTestVersion;
            if (!string.IsNullOrEmpty(this.DocumentationFormat))
                configuration.DocumentationFormat =
                    (DocumentationFormat) Enum.Parse(typeof (DocumentationFormat), this.DocumentationFormat, true);

            if (!string.IsNullOrEmpty(this.Language))
            {
                configuration.Language = this.Language;
             }
        }
    }
}
#endif