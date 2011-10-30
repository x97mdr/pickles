#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

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
