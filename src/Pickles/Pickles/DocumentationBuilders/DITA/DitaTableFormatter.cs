using System.Xml.Linq;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.DITA
{
    public class DitaTableFormatter
    {
        public void Format(XElement parentElement, Table table)
        {
            var simpletable = new XElement("simpletable");

            var headerRow = new XElement("sthead");
            foreach (string cell in table.HeaderRow)
            {
                headerRow.Add(new XElement("stentry", cell));
            }
            simpletable.Add(headerRow);

            foreach (TableRow row in table.DataRows)
            {
                var strow = new XElement("strow");
                foreach (string cell in row)
                {
                    strow.Add(new XElement("stentry", cell));
                }
                simpletable.Add(strow);
            }

            parentElement.Add(simpletable);
        }
    }
}