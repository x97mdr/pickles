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

using ClosedXML.Excel;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.Excel
{
    public class ExcelScenarioFormatter
    {
        private readonly ExcelStepFormatter excelStepFormatter;

        public ExcelScenarioFormatter(ExcelStepFormatter excelStepFormatter)
        {
            this.excelStepFormatter = excelStepFormatter;
        }

        public void Format(IXLWorksheet worksheet, Scenario scenario, ref int row)
        {
            worksheet.Cell(row, "B").Style.Font.SetBold();
            worksheet.Cell(row++, "B").Value = scenario.Name;
            worksheet.Cell(row++, "C").Value = scenario.Description;

            foreach (Step step in scenario.Steps)
            {
                excelStepFormatter.Format(worksheet, step, ref row);
            }
        }
    }
}