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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using System.IO;

namespace Pickles
{
    public class CommandLineArgumentParser
    {
        private readonly OptionSet options;
        private string featureDirectory;
        private string outputDirectory;
        private bool versionRequested;
        private bool helpRequested;
        private string resultsFile;
        private string systemUnderTestName;
        private string systemUnderTestVersion;

        public CommandLineArgumentParser()
        {
            this.options = new OptionSet
            {
   	            { "f|feature-directory=", "directory to start scanning recursively for features", v => this.featureDirectory = v },
   	            { "o|output-directory=", "directory where output files will be placed", v => this.outputDirectory = v },
   	            { "lr|link-results-file=", "a file containing the results of testing the features", v => this.resultsFile = v },
   	            { "sn|system-under-test-name=", "the name of the system under test", v => this.systemUnderTestName = v },
   	            { "sv|system-under-test-version=", "the version of the system under test", v => this.systemUnderTestVersion = v },
   	            { "v|version",  v => versionRequested = v != null },
   	            { "h|?|help",   v => helpRequested = v != null }
            };
        }

        private void DisplayVersion(TextWriter stdout)
        {
            stdout.WriteLine("Pickles version {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void DisplayHelp(TextWriter stdout)
        {
            DisplayVersion(stdout);
            this.options.WriteOptionDescriptions(stdout);
        }

        public bool Parse(string[] args, Configuration configuration, TextWriter stdout)
        {
            configuration.FeatureFolder = new DirectoryInfo(Directory.GetCurrentDirectory());
            configuration.OutputFolder = new DirectoryInfo(Environment.GetEnvironmentVariable("TEMP"));

            var extra = this.options.Parse(args);

            if (versionRequested)
            {
                DisplayVersion(stdout);
                return false;
            }
            else if (helpRequested)
            {
                DisplayHelp(stdout);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(this.featureDirectory)) configuration.FeatureFolder = new System.IO.DirectoryInfo(this.featureDirectory);
            if (!string.IsNullOrWhiteSpace(this.outputDirectory)) configuration.OutputFolder = new System.IO.DirectoryInfo(this.outputDirectory);
            if (!string.IsNullOrWhiteSpace(this.resultsFile)) configuration.LinkedTestFrameworkResultsFile = new FileInfo(this.resultsFile);
            if (!string.IsNullOrWhiteSpace(this.systemUnderTestName)) configuration.SystemUnderTestName = this.systemUnderTestName;
            if (!string.IsNullOrWhiteSpace(this.systemUnderTestVersion)) configuration.SystemUnderTestVersion = this.systemUnderTestVersion;

            return true;
        }
    }
}
