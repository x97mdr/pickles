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

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Parser.Builders
{
    internal class TableBuilder
    {
        private readonly List<TableRow> cells;
        private readonly TableRow header;
        private bool hasHeader;

        public TableBuilder()
        {
            this.hasHeader = false;
            this.header = new TableRow();
            this.cells = new List<TableRow>();
        }

        public void AddRow(IEnumerable<string> cells)
        {
            if (this.hasHeader)
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