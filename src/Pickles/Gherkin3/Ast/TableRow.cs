using System.Collections.Generic;

namespace Gherkin3.Ast
{
    public class TableRow : IHasLocation
    {
        public Location Location { get; private set; }
        public IEnumerable<TableCell> Cells { get; private set; }

        public TableRow(Location location, TableCell[] cells)
        {
            this.Location = location;
            this.Cells = cells;
        }
    }
}