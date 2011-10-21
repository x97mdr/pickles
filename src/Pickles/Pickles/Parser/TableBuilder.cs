using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    class TableBuilder
    {
        private TableRow header;
        private List<TableRow> cells;
        private bool hasHeader;

        public TableBuilder()
        {
            this.hasHeader = false;
            this.header = new TableRow();
            this.cells = new List<TableRow>();
        }

        public void AddRow(IEnumerable<string> cells)
        {
            if (hasHeader)
            {
                this.cells.Add(new TableRow(cells.ToArray()));
            }
            else
            {
                this.hasHeader = true;
                this.header.AddRange(cells);
            }
        }

        public Table GetResult()
        {
            if (!this.hasHeader) return null;

            return new Table
            {
                HeaderRow = this.header,
                DataRows = this.cells
            };
        }
    }
}
