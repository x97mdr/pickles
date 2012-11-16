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

        private string BuildTitle(bool wasSuccessful)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}", wasSuccessful ? "Successful" : "Failed");
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
                                             new XAttribute("title", BuildTitle(result.WasSuccessful)),
                                             new XAttribute("alt", BuildTitle(result.WasSuccessful))
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
            if (configuration.HasTestResults)
            {
                TestResult scenarioResult = results.GetFeatureResult(feature);

                return BuildImageElement(scenarioResult);
            }

            return null;
        }

        public XElement Format(Scenario scenario)
        {
            if (configuration.HasTestResults)
            {
                TestResult scenarioResult = results.GetScenarioResult(scenario);

                return BuildImageElement(scenarioResult);
            }

            return null;
        }

        public XElement Format(ScenarioOutline scenarioOutline)
        {
            if (configuration.HasTestResults)
            {
                TestResult scenarioResult = results.GetScenarioOutlineResult(scenarioOutline);

                return BuildImageElement(scenarioResult);
            }

            return null;
        }
    }
}