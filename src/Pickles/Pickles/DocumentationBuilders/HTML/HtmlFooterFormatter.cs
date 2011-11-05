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

namespace Pickles.Formatters
{
    public class HtmlFooterFormatter
    {
        private readonly Configuration configuration;
        private readonly XNamespace xmlns;

        public HtmlFooterFormatter(Configuration configuration)
        {
            this.configuration = configuration;
            this.xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        private XElement BuildVersionString()
        {
            if (!string.IsNullOrWhiteSpace(this.configuration.SystemUnderTestName) && !string.IsNullOrWhiteSpace(this.configuration.SystemUnderTestVersion))
            {
                return new XElement(this.xmlns + "p", string.Format("Test results generated for: {0}, version {1}", this.configuration.SystemUnderTestName, this.configuration.SystemUnderTestVersion));
            }
            else if (!string.IsNullOrWhiteSpace(this.configuration.SystemUnderTestName))
            {
                return new XElement(this.xmlns + "p", string.Format("Test results generated for: {0}", this.configuration.SystemUnderTestName));
            }
            else if (!string.IsNullOrWhiteSpace(this.configuration.SystemUnderTestVersion))
            {
                return new XElement(this.xmlns + "p", string.Format("Test results generated for: version {1}", this.configuration.SystemUnderTestVersion));
            }

            return null;
        }

        public XElement Format()
        {
            return new XElement(xmlns + "div",
                new XAttribute("id", "footer"),
                BuildVersionString(),
                new XElement(xmlns + "p", "Pickled on: " + DateTime.Now.ToString("d MMMM yyyy hh:mm:ss")),
                new XElement(xmlns + "p", "Produced by Pickles, version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString())
             );
        }
    }
}
