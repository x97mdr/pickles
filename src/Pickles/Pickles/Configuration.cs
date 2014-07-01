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
using System.IO.Abstractions;

namespace PicklesDoc.Pickles
{
    public class Configuration
    {
        private readonly List<FileInfoBase> testResultsFiles;

        public Configuration()
        {
            this.testResultsFiles = new List<FileInfoBase>();
        }

        public DirectoryInfoBase FeatureFolder { get; set; }

        public DirectoryInfoBase OutputFolder { get; set; }

        public DocumentationFormat DocumentationFormat { get; set; }

        public string Language { get; set; }

        public TestResultsFormat TestResultsFormat { get; set; }

        public bool HasTestResults
        {
            get { return this.TestResultsFiles != null && testResultsFiles.Count > 0; }
        }

        public FileInfoBase TestResultsFile
        {
            get
            {
                return testResultsFiles[0];
            }
        }

        public IEnumerable<FileInfoBase> TestResultsFiles
        {
            get
            {
                return this.testResultsFiles;
            }
        }

        public string SystemUnderTestName { get; set; }

        public string SystemUnderTestVersion { get; set; }

        public void AddTestResultFile(FileInfoBase fileInfoBase)
        {
            this.AddTestResultFileIfItExists(fileInfoBase);
        }

        public void AddTestResultFiles(IEnumerable<FileInfoBase> fileInfoBases)
        {
            foreach (var fileInfoBase in fileInfoBases ?? new FileInfoBase[0])
            {
                this.AddTestResultFileIfItExists(fileInfoBase);
          }
        }

        private void AddTestResultFileIfItExists(FileInfoBase fileInfoBase)
        {
            if (fileInfoBase.Exists)
            {
                this.testResultsFiles.Add(fileInfoBase);
            }
        }
    }
}