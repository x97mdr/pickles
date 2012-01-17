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
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Ninject;
using Pickles.DocumentationBuilders.HTML;
using Pickles.TestFrameworks;

namespace Pickles.MSBuild
{
    public class Pickles : Task
    {
        [Required]
        public string FeatureDirectory { get; set; }

        [Required]
        public string OutputDirectory { get; set; }

        public string Language { get; set; }

        public string ResultsFormat { get; set; }

        public string ResultsFile { get; set; }

        public string SystemUnderTestName { get; set; }

        public string SystemUnderTestVersion { get; set; }

        public string DocumentationFormat { get; set; }

        private void CaptureConfiguration(Configuration configuration)
        {
            configuration.FeatureFolder = new DirectoryInfo(FeatureDirectory);
            configuration.OutputFolder = new DirectoryInfo(OutputDirectory);
            if (!string.IsNullOrEmpty(Language)) configuration.Language = Language;
            if (!string.IsNullOrEmpty(ResultsFormat)) configuration.TestResultsFormat = (TestResultsFormat)Enum.Parse(typeof(TestResultsFormat), this.ResultsFormat, true);
            if (!string.IsNullOrEmpty(ResultsFile)) configuration.TestResultsFile = new FileInfo(ResultsFile);
            if (!string.IsNullOrEmpty(SystemUnderTestName)) configuration.SystemUnderTestName = SystemUnderTestName;
            if (!string.IsNullOrEmpty(SystemUnderTestVersion)) configuration.SystemUnderTestVersion = SystemUnderTestVersion;
            if (!string.IsNullOrEmpty(DocumentationFormat)) configuration.DocumentationFormat = (DocumentationFormat)Enum.Parse(typeof(DocumentationFormat), this.DocumentationFormat, true);
        }

        public override bool Execute()
        {
            try
            {
                Log.LogMessage("Pickles v.{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Log.LogMessage("Reading features from {0}", FeatureDirectory ?? string.Empty);
                Log.LogMessage("Writing output to {0}", OutputDirectory ?? string.Empty);

                var kernel = new StandardKernel(new PicklesModule());
                Configuration configuration = kernel.Get<Configuration>();
                CaptureConfiguration(configuration);

                var runner = kernel.Get<Runner>();
                runner.Run(kernel);
            }
            catch (Exception e)
            {
                Log.LogWarningFromException(e, false);
            }

            return true; // HACK - since this is merely producing documentation we do not want it to cause a build to fail if something goes wrong
        }
    }
}
