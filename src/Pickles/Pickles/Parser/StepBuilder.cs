using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    class StepBuilder
    {
        private Keyword keyword;
        private string name;
        private TableBuilder tableBuilder;
        private string docString;

        public StepBuilder(TableBuilder tableBuilder)
        {
            this.tableBuilder = tableBuilder;
        }

        public void SetKeyword(string keywordText)
        {
            Keyword keyword;
            if (Enum.TryParse<Keyword>(keywordText, out keyword))
            {
                this.keyword = keyword;
            }
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void AddTableRow(IEnumerable<string> cells)
        {
            this.tableBuilder.AddRow(cells);
        }

        public void SetDocString(string docString)
        {
            this.docString = docString;
        }

        public Step GetResult()
        {
            return new Step
            {
                Keyword = this.keyword,
                Name = this.name,
                DocStringArgument = this.docString,
                TableArgument = this.tableBuilder.GetResult()
            };
        }
    }
}
