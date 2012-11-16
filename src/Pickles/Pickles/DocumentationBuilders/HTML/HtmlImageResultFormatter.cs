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
using System.Text;
using System.Xml.Linq;
using Pickles.Parser;
using Pickles.TestFrameworks;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlImageResultFormatter
    {
        private readonly Configuration configuration;
        private readonly HtmlResourceSet htmlResourceSet;
        private readonly ITestResults results;
        private readonly XNamespace xmlns;

        public HtmlImageResultFormatter(Configuration configuration, ITestResults results,
                                        HtmlResourceSet htmlResourceSet)
        {
            this.configuration = configuration;
            this.results = results;
            this.htmlResourceSet = htmlResourceSet;
            xmlns = HtmlNamespace.Xhtml;
        }

        private string BuildTitle(TestResult successful)
        {
            var sb = new StringBuilder();

            if (!successful.WasExecuted)
            {
              sb.AppendFormat("{0}", "Inconclusive");
            }
            else
            {
              sb.AppendFormat("{0}", successful.WasSuccessful ? "Successful" : "Failed");
            }

            if (!string.IsNullOrEmpty(configuration.SystemUnderTestName) &&
                !string.IsNullOrEmpty(configuration.SystemUnderTestVersion))
            {
                sb.AppendFormat(" with {0} version {1}", configuration.SystemUnderTestName,
                                configuration.SystemUnderTestVersion);
            }
            else if (!string.IsNullOrEmpty(configuration.SystemUnderTestName))
            {
                sb.AppendFormat(" with {0}", configuration.SystemUnderTestName);
            }
            else if (!string.IsNullOrEmpty(configuration.SystemUnderTestVersion))
            {
                sb.AppendFormat(" with version {0}", configuration.SystemUnderTestVersion);
            }
            return sb.ToString();
        }

        private XElement BuildImageElement(TestResult result)
        {
            return new XElement(xmlns + "div",
                                new XAttribute("class", "float-right"),
                                new XElement(xmlns + "img",
                                             new XAttribute("src", this.DetermineImage(result)),
                                             new XAttribute("title", this.BuildTitle(result)),
                                             new XAttribute("alt", this.BuildTitle(result))
                                    )
                );
        }

      private Uri DetermineImage(TestResult result)
      {
        if (!result.WasExecuted)
        {
          return this.htmlResourceSet.InconclusiveImage;
        }
        else
        {
          return result.WasSuccessful 
            ? this.htmlResourceSet.SuccessImage 
            : this.htmlResourceSet.FailureImage;
        }
      }

        public XElement Format(Feature feature)
        {
            TestResult scenarioResult = this.configuration.HasTestResults ? this.results.GetFeatureResult(feature) : TestResult.Inconclusive();
            return BuildImageElement(scenarioResult);
        }

        public XElement Format(Scenario scenario)
        {
            TestResult scenarioResult = this.configuration.HasTestResults ? this.results.GetScenarioResult(scenario) : TestResult.Inconclusive();

            return BuildImageElement(scenarioResult);
        }

        public XElement Format(ScenarioOutline scenarioOutline)
        {
            TestResult scenarioResult = this.configuration.HasTestResults ? this.results.GetScenarioOutlineResult(scenarioOutline) : TestResult.Inconclusive();

            return BuildImageElement(scenarioResult);
        }
    }
}