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
    public class HtmlTableFormatter
    {
        private readonly XNamespace xmlns;

        public HtmlTableFormatter()
        {
            xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        public XElement Format(Table table)
        {
            return new XElement(xmlns + "table",
                            new XElement(xmlns + "thead",
                                new XElement(xmlns + "tr",
                                    table.HeaderRow.Select(cell => new XElement(xmlns + "th", cell))
                                )
                            ),
                            new XElement(xmlns + "tbody",
                                table.DataRows.Select(row => new XElement(xmlns + "tr",
                                    row.Select(cell => new XElement(xmlns + "td", cell)))
                                )
                            )
                        );
        }
    }
}
