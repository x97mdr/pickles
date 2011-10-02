using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Pickles.Formatters
{
    public class HtmlMultilineStringFormatter
    {
        private readonly XNamespace xmlns;

        public HtmlMultilineStringFormatter()
        {
            xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        public XElement Format(string multilineText)
        {
            return new XElement(xmlns + "div",
                       new XAttribute("class", "pre"),
                       new XElement(xmlns + "pre",
                           new XElement(xmlns + "code",
                               new XAttribute("class", "no-highlight"),
                               new XText(multilineText)
                            )
                        )
                    );
        }
    }
}
