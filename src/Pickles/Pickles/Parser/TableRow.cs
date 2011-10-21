using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public class TableRow : List<string>
    {
        public TableRow() { }

        public TableRow(params string[] cells)
        {
            AddRange(cells);
        }
    }
}
