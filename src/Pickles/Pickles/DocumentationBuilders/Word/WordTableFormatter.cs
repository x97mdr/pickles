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
using DocumentFormat.OpenXml.Wordprocessing;
using WordTable = DocumentFormat.OpenXml.Wordprocessing.Table;

namespace Pickles.DocumentationBuilders.Word
{
    public class WordTableFormatter
    {
        private static TableProperties GenerateTableProperties()
        {
            TableProperties tableProperties1 = new TableProperties();
            TableStyle tableStyle1 = new TableStyle() { Val = "TableGrid" };
            TableWidth tableWidth1 = new TableWidth() { Width = "0", Type = TableWidthUnitValues.Auto };
            TableLook tableLook1 = new TableLook() { Val = "04A0" };

            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableLook1);
            return tableProperties1;
        }
        
        public void Format(Body body, Pickles.Parser.Table table)
        {
            WordTable wordTable = new WordTable();
            wordTable.Append(GenerateTableProperties());
            var headerRow = new DocumentFormat.OpenXml.Wordprocessing.TableRow();
            foreach (var cell in table.HeaderRow)
            {
                var wordCell = new DocumentFormat.OpenXml.Wordprocessing.TableCell();
                wordCell.Append(new Paragraph(new Run(new Text(cell))));
                headerRow.Append(wordCell);
            }
            wordTable.Append(headerRow);

            foreach (var row in table.DataRows)
            {
                var wordRow = new DocumentFormat.OpenXml.Wordprocessing.TableRow();

                foreach (var cell in row)
                {
                    var wordCell = new DocumentFormat.OpenXml.Wordprocessing.TableCell();
                    wordCell.Append(new Paragraph(new Run(new Text(cell))));
                    wordRow.Append(wordCell);
                }

                wordTable.Append(wordRow);
            }

            body.Append(wordTable);
        }
    }
}
