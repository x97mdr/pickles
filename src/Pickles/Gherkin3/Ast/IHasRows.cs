using System.Collections.Generic;

namespace Gherkin3.Ast
{
    public interface IHasRows
    {
        IEnumerable<TableRow> Rows { get; }
    }
}