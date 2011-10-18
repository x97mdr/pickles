using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace Pickles.Formatters
{
    public class HtmlFooterFormatter
    {
        public XElement Format()
        {
            return new XElement("p", "Produced by Pickles version " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }
    }
}
