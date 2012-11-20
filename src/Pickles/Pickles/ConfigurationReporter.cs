using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles
{
    public class ConfigurationReporter
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
