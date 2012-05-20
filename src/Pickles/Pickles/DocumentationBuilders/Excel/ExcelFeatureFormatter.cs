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

using System.Linq;
using ClosedXML.Excel;
using Pickles.Parser;

namespace Pickles.DocumentationBuilders.Excel
{
    public class ExcelFeatureFormatter
    {
        private readonly ExcelScenarioFormatter excelScenarioFormatter;
        private readonly ExcelScenarioOutlineFormatter excelScenarioOutlineFormatter;
        private uint nextId = 1;

        public ExcelFeatureFormatter(ExcelScenarioFormatter excelScenarioFormatter,
                                     ExcelScenarioOutlineFormatter excelScenarioOutlineFormatter)
        {
            this.excelScenarioFormatter = excelScenarioFormatter;
            this.excelScenarioOutlineFormatter = excelScenarioOutlineFormatter;
        }

        private string ConvertFeatureNameToSheetName(string featureName)
        {
            return new string(featureName.Take(31).ToArray());
        }

        public void Format(IXLWorksheet worksheet, Feature feature)
        {
            worksheet.Cell("A1").Style.Font.SetBold();
            worksheet.Cell("A1").Value = feature.Name;
            worksheet.Cell("B2").Value = feature.Description;
            worksheet.Cell("B2").Style.Alignment.WrapText = false;

            int row = 4;
            foreach (IFeatureElement featureElement in feature.FeatureElements)
            {
                var scenario = featureElement as Scenario;
                if (scenario != null)
                {
                    excelScenarioFormatter.Format(worksheet, scenario, ref row);
                }

                var scenarioOutline = featureElement as ScenarioOutline;
                if (scenarioOutline != null)
                {
                    excelScenarioOutlineFormatter.Format(worksheet, scenarioOutline, ref row);
                }

                row++;
            }
        }
    }
}