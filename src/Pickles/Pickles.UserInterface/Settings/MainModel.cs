﻿// #region License
// 
// 
// /*
//     Copyright [2011] [Jeffrey Cameron]
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//        http://www.apache.org/licenses/LICENSE-2.0
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// */
// #endregion

using System;
using System.Runtime.Serialization;

namespace Pickles.UserInterface.Settings
{
    [DataContract(Name = "pickles", Namespace = "")]
    public class MainModel
    {
        [DataMember(Name = "featureDirectory")]
        public string FeatureDirectory { get; set; }

        [DataMember(Name = "outputDirectory")]
        public string OutputDirectory { get; set; }

        [DataMember(Name = "projectName")]
        public string ProjectName { get; set; }

        [DataMember(Name = "projectVersion")]
        public string ProjectVersion { get; set; }

        [DataMember(Name = "includeTestResults")]
        public bool IncludeTestResults { get; set; }

        [DataMember(Name = "testResultsFile")]
        public string TestResultsFile { get; set; }

        [DataMember(Name = "testResultsFormat")]
        public TestResultsFormat TestResultsFormat { get; set; }

        [DataMember(Name = "documentationFormats")]
        public DocumentationFormat[] DocumentationFormats { get; set; }

        [DataMember(Name = "selectedLanguageLcid")]
        public int SelectedLanguageLcid { get; set; }
    }
}
