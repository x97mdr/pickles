//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ConfigurationReporter.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace PicklesDoc.Pickles
{
    public class ConfigurationReporter
    {
        public void ReportOn(Configuration configuration, Action<String> writeToLog)
        {
            writeToLog("Generating documentation based on the following parameters");
            writeToLog("----------------------------------------------------------");
            writeToLog(string.Format("Feature Directory         : {0}", configuration.FeatureFolder.FullName));
            writeToLog(string.Format("Output Directory          : {0}", configuration.OutputFolder.FullName));
            writeToLog(string.Format("Project Name              : {0}", configuration.SystemUnderTestName));
            writeToLog(string.Format("Project Version           : {0}", configuration.SystemUnderTestVersion));
            writeToLog(string.Format("Language                  : {0}", configuration.Language));
            writeToLog(string.Format("Incorporate Test Results? : {0}", configuration.HasTestResults ? "Yes" : "No"));

            if (configuration.HasTestResults)
            {
                writeToLog(string.Format("Test Result Format        : {0}", configuration.TestResultsFormat));
                writeToLog(string.Format("Test Result File          : {0}", configuration.TestResultsFile.FullName));
            }
        }
    }
}
