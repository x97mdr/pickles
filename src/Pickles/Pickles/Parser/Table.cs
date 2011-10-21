using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public class Table
    {
        public TableRow HeaderRow { get; set; }
        public List<TableRow> DataRows { get; set; }
    }
}
