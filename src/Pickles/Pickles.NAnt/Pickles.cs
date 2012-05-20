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
using System.Reflection;
using NAnt.Core;
using NAnt.Core.Attributes;
using Ninject;

namespace Pickles.NAnt
{
    [TaskName("pickles")]
    public class Pickles : Task
    {
        [TaskAttribute("features", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string FeatureDirectory { get; set; }

        [TaskAttribute("output", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string OutputDirectory { get; set; }

        [TaskAttribute("language")]
        [StringValidator(AllowEmpty = true)]
        public string Language { get; set; }

        [TaskAttribute("resultsFormat", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string ResultsFormat { get; set; }

        [TaskAttribute("resultsFile", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string ResultsFile { get; set; }

        [TaskAttribute("systemUnderTestName", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string SystemUnderTestName { get; set; }

        [TaskAttribute("systemUnderTestVersion", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string SystemUnderTestVersion { get; set; }

        [TaskAttribute("documentationFormat", Required = false)]
        [StringValidator(AllowEmpty = true)]
        public string DocumentationFormat { get; set; }

        private void CaptureConfiguration(Configuration configuration)
        {
            configuration.FeatureFolder = new DirectoryInfo(FeatureDirectory);
            configuration.OutputFolder = new DirectoryInfo(OutputDirectory);
            if (!string.IsNullOrEmpty(Language)) configuration.Language = Language;
            if (!string.IsNullOrEmpty(ResultsFormat))
                configuration.TestResultsFormat =
                    (TestResultsFormat) Enum.Parse(typeof (TestResultsFormat), ResultsFormat, true);
            if (!string.IsNullOrEmpty(ResultsFile)) configuration.TestResultsFile = new FileInfo(ResultsFile);
            if (!string.IsNullOrEmpty(SystemUnderTestName)) configuration.SystemUnderTestName = SystemUnderTestName;
            if (!string.IsNullOrEmpty(SystemUnderTestVersion))
                configuration.SystemUnderTestVersion = SystemUnderTestVersion;
            if (!string.IsNullOrEmpty(DocumentationFormat))
                configuration.DocumentationFormat =
                    (DocumentationFormat) Enum.Parse(typeof (DocumentationFormat), DocumentationFormat, true);
        }

        protected override void ExecuteTask()
        {
            try
            {
                Project.Log(Level.Info, "Pickles v.{0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Project.Log(Level.Info, "Reading features from {0}", FeatureDirectory ?? string.Empty);
                Project.Log(Level.Info, "Writing output to {0}", OutputDirectory ?? string.Empty);

                var kernel = new StandardKernel(new PicklesModule());

                var configuration = kernel.Get<Configuration>();
                CaptureConfiguration(configuration);

                var runner = kernel.Get<Runner>();
                runner.Run(kernel);
            }
            catch (Exception e)
            {
                Project.Log(Level.Warning, e.Message);
            }
        }
    }
}