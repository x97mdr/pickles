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
using NGenerics.DataStructures.Trees;
using Pickles.DirectoryCrawler;

namespace Pickles.DocumentationBuilders.Excel
{
    public class ExcelTableOfContentsFormatter
    {
        private void WriteFileCell(IXLWorksheet worksheet, ref int row, int column, IXLWorksheet featureWorksheet)
        {
            worksheet.Cell(row, column).Value = featureWorksheet.Cell("A1").Value;
            worksheet.Cell(row++, column).Hyperlink = new XLHyperlink(featureWorksheet.Name + "!A1");
        }

        private void WriteDirectoryCell(IXLWorksheet worksheet, ref int row, int column, string directoryName)
        {
            worksheet.Cell(row, column).Style.Font.Bold = true;
            worksheet.Cell(row++, column).Value = directoryName;
        }

        public void BuildTableOfContents(XLWorkbook workbook, IXLWorksheet worksheet, ref int row, int column, GeneralTree<IDirectoryTreeNode> features)
        {
            foreach (var childNode in features.ChildNodes)
            {
                var featureChildNode = childNode.Data as FeatureDirectoryTreeNode;
                if (featureChildNode != null)
                {
                    var featureWorksheet =
                        workbook.Worksheets.SingleOrDefault(
                            sheet => sheet.Cell("A1").Value.ToString() == featureChildNode.Feature.Name);
                    WriteFileCell(worksheet, ref row, column, featureWorksheet);
                }
                else if (!childNode.Data.IsContent)
                {
                    WriteDirectoryCell(worksheet, ref row, column, childNode.Data.Name);
                    BuildTableOfContents(workbook, worksheet, ref row, column + 1, childNode);
                }
            }
        }

        public void Format(XLWorkbook workbook, GeneralTree<IDirectoryTreeNode> features)
        {
            var tocWorksheet = workbook.AddWorksheet("TOC", 0);

            int startRow = 1;
            BuildTableOfContents(workbook, tocWorksheet, ref startRow, 1, features);
        }
    }
}
