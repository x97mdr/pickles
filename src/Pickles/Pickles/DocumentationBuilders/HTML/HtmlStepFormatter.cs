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
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlStepFormatter
    {
        private readonly HtmlTableFormatter htmlTableFormatter;
        private readonly HtmlMultilineStringFormatter htmlMultilineStringFormatter;
        private readonly XNamespace xmlns;

        public HtmlStepFormatter(HtmlTableFormatter htmlTableFormatter, HtmlMultilineStringFormatter htmlMultilineStringFormatter)
        {
            this.htmlTableFormatter = htmlTableFormatter;
            this.htmlMultilineStringFormatter = htmlMultilineStringFormatter;
            xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        public XElement Format(Step step)
        {
            var li =  new XElement(xmlns + "li",
                          new XAttribute("class", "step"),
                          new XElement(xmlns + "span", new XAttribute("class", "keyword"), step.Keyword + " "),
                          step.Name
                      );

            if (step.TableArgument != null)
            {
                li.Add(this.htmlTableFormatter.Format(step.TableArgument));
            }

            if (!string.IsNullOrEmpty(step.DocStringArgument))
            {
                li.Add(this.htmlMultilineStringFormatter.Format(step.DocStringArgument));
            }

            return li;
        }
    }
}
