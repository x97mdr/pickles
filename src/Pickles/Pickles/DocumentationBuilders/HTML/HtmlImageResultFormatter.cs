//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="HtmlImageResultFormatter.cs" company="PicklesDoc">
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
using System.Text;
using System.Xml.Linq;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.DocumentationBuilders.HTML
{
    public class HtmlImageResultFormatter
    {
        private readonly Configuration configuration;

      private readonly ITestResults results;
        private readonly XNamespace xmlns;

        public HtmlImageResultFormatter(Configuration configuration, ITestResults results)
        {
            this.configuration = configuration;
            this.results = results;
            this.xmlns = HtmlNamespace.Xhtml;
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

            if (!string.IsNullOrEmpty(this.configuration.SystemUnderTestName) &&
                !string.IsNullOrEmpty(this.configuration.SystemUnderTestVersion))
            {
                sb.AppendFormat(" with {0} version {1}", this.configuration.SystemUnderTestName,
                                this.configuration.SystemUnderTestVersion);
            }
            else if (!string.IsNullOrEmpty(this.configuration.SystemUnderTestName))
            {
                sb.AppendFormat(" with {0}", this.configuration.SystemUnderTestName);
            }
            else if (!string.IsNullOrEmpty(this.configuration.SystemUnderTestVersion))
            {
                sb.AppendFormat(" with version {0}", this.configuration.SystemUnderTestVersion);
            }
            return sb.ToString();
        }


      private XElement BuildImageElement(TestResult result, string elementName = "div")
      {
        return new XElement(this.xmlns + elementName,
                            new XAttribute("class", "float-right"),
                            new XElement(this.xmlns + "i",
                                         new XAttribute("class", this.DetermineClass(result)),
                                         new XAttribute("title", this.BuildTitle(result)),
                                         " "
                                )
            );
      }

      private string DetermineClass(TestResult result)
      {
        if (!result.WasExecuted)
        {
          return "icon-warning-sign inconclusive";
        }
        else
        {
          return result.WasSuccessful
            ? "icon-ok passed"
            : "icon-minus-sign failed";
        }
      }


      public XElement Format(Feature feature)
      {
        if (this.configuration.HasTestResults)
        {
          TestResult scenarioResult = this.results.GetFeatureResult(feature);

          return this.BuildImageElement(scenarioResult);
        }

        return null;
      }

      public XElement FormatForToC(Feature feature)
      {
        if (this.configuration.HasTestResults)
        {
          TestResult scenarioResult = this.results.GetFeatureResult(feature);

          return this.BuildImageElement(scenarioResult, "span");
        }

        return null;
      }

        public XElement Format(Scenario scenario)
        {
          if (this.configuration.HasTestResults)
          {
            TestResult scenarioResult = this.results.GetScenarioResult(scenario);

            return this.BuildImageElement(scenarioResult);
          }

          return null;
        }

        public XElement Format(ScenarioOutline scenarioOutline)
        {
          if (this.configuration.HasTestResults)
          {
            TestResult scenarioResult = this.results.GetScenarioOutlineResult(scenarioOutline);

            return this.BuildImageElement(scenarioResult);
          }

          return null;
        }

        public XElement Format(ScenarioOutline scenarioOutline, params string[] exampleValues)
        {
          if (this.configuration.HasTestResults && this.results.SupportsExampleResults)
          {
            TestResult exampleResult = this.results.GetExampleResult(scenarioOutline, exampleValues);

            return this.BuildImageElement(exampleResult);
          }

          return null;
        }
    }
}