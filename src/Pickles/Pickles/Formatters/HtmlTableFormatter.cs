using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace Pickles.Formatters
{
    public class HtmlTableFormatter
    {
        private readonly XNamespace xmlns;

        public HtmlTableFormatter()
        {
            xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
        }

        public XElement Format(GherkinTable table)
        {
            return new XElement(xmlns + "table",
                            new XElement(xmlns + "thead",
                                new XElement(xmlns + "tr",
                                    table.Header.Cells.Select(cell => new XElement(xmlns + "th", cell.Value))
                                )
                            ),
                            new XElement(xmlns + "tbody",
                                table.Body.Select(row => new XElement(xmlns + "tr",
                                    row.Cells.Select(cell => new XElement(xmlns + "td", cell.Value)))
                                )
                            )
                        );
        }
    }
}
