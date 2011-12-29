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
using DocumentFormat.OpenXml.Wordprocessing;
using Pickles.Extensions;
using Pickles.Parser;
using Pickles.TestFrameworks;

namespace Pickles.DocumentationBuilders.Word
{
    public class WordScenarioFormatter
    {
        private readonly WordStepFormatter wordStepFormatter;
        private readonly ITestResults nunitResults;
        private readonly Configuration configuration;

        public WordScenarioFormatter(WordStepFormatter wordStepFormatter, Configuration configuration, ITestResults nunitResults)
        {
            this.wordStepFormatter = wordStepFormatter;
            this.configuration = configuration;
            this.nunitResults = nunitResults;
        }

        public void Format(Body body, Scenario scenario)
        {
            if (this.configuration.HasTestFrameworkResults)
            {
                var testResult = this.nunitResults.GetScenarioResult(scenario);
                if (testResult.WasExecuted && testResult.WasSuccessful)
                {
                    body.GenerateParagraph("Passed", "Passed");
                }
                else if (testResult.WasExecuted && !testResult.WasSuccessful)
                {
                    body.GenerateParagraph("Failed", "Failed");
                }
            }

            body.GenerateParagraph(scenario.Name, "Heading2");
            if (!string.IsNullOrEmpty(scenario.Description)) body.GenerateParagraph(scenario.Description, "Normal");

            foreach (var step in scenario.Steps)
            {
                this.wordStepFormatter.Format(body, step);
            }
        }
    }
}
