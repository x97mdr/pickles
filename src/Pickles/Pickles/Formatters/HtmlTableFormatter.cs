using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.Formatters
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
