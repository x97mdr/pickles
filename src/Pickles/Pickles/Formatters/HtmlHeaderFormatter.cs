using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Pickles.Formatters
{
    public class HtmlHeaderFormatter
    {
        public XElement Format()
        {
            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            return new XElement(xmlns + "div",
                new XAttribute("id", "top"));
        }
    }
}
