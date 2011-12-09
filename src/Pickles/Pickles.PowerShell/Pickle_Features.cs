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

using System;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using Ninject;

namespace Pickles.PowerShell
{
    [Cmdlet("Pickle", "Features")]
    public class Pickle_Features : PSCmdlet
    {

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_FEATURE_DIR, Mandatory = true, Position = 0)]
        public string FeatureDirectory { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_OUTPUT_DIR, Mandatory = true, Position = 1)]
        public string OutputDirectory { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_LANGUAGE_FEATURE_FILES, Mandatory = false)]
        public string Language { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_RESULT_FILE, Mandatory = false)]
        public string TestResultsFile { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_SUT_NAME, Mandatory = false)]
        public string SystemUnderTestName { get; set; }

        [Parameter(HelpMessage = CommandLineArgumentParser.HELP_SUT_VERSION, Mandatory = false)]
        public string SystemUnderTestVersion { get; set; }

        protected override void ProcessRecord()
        {
            var kernel = new StandardKernel(new PicklesModule());
            var configuration = kernel.Get<Configuration>();

            ParseParameters(configuration);

            WriteObject(string.Format("Pickles v.{0}{1}", Assembly.GetExecutingAssembly().GetName().Version, Environment.NewLine));
            WriteObject(string.Format(" - generating feature documentations for features in {0}", configuration.FeatureFolder));
            WriteObject(string.Format(" - output to {0}", configuration.OutputFolder));

            var runner = kernel.Get<Runner>();
            runner.Run(kernel);

            WriteObject(string.Format("Pickles completed successfully"));
        }

        private void ParseParameters(Configuration configuration)
        {
            configuration.FeatureFolder = new DirectoryInfo(FeatureDirectory);
            configuration.OutputFolder = new DirectoryInfo(OutputDirectory);
            if (!string.IsNullOrEmpty(TestResultsFile))
            {
                configuration.LinkedTestFrameworkResultsFile = new FileInfo(TestResultsFile);
            }
            configuration.SystemUnderTestName = SystemUnderTestName;
            configuration.SystemUnderTestVersion = SystemUnderTestVersion;
        }
    }
}
