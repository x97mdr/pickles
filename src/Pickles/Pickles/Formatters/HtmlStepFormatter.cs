using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.Formatters
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
