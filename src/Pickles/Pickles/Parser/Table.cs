using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public class Table
    {
        private TableRow headerRow;
        private readonly List<TableRow> dataRows;

        public TableRow HeaderRow { get { return this.headerRow; } }
        public IEnumerable<TableRow> DataRows { get { return this.dataRows; } }

        public Table()
        {
            this.headerRow = new TableRow();
            this.dataRows = new List<TableRow>();
        }

        public void SetHeaderRow(TableRow row)
        {
            this.headerRow = row;
        }

        public void AddDataRow(TableRow row)
        {
            this.dataRows.Add(row);
        }
    }
}
