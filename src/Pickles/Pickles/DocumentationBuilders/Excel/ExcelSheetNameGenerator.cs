﻿#region License

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
    public class ExcelSheetNameGenerator
    {
        public string GenerateSheetName(XLWorkbook workbook, Feature feature)
        {
            string name = RemoveUnnecessaryAndIllegalCharacters(feature).ToUpperInvariant();
            if (name.Length > 31) name = name.Substring(0, 31);

            // check if the workbook contains any sheets with this name
            int nextIndex = 1;
            while (workbook.Worksheets.Any(sheet => sheet.Name == name))
            {
                name = name.Remove(name.Length - 3, 3);
                name = name + "(" + nextIndex++ + ")";
            }

            return name;
        }

        private static string RemoveUnnecessaryAndIllegalCharacters(Feature feature)
        {
            return feature.Name
                .Replace(" ", string.Empty)
                .Replace("\t", string.Empty)
                .Replace(":", string.Empty)
                .Replace(@"\", string.Empty)
                .Replace("/", string.Empty)
                .Replace("?", string.Empty)
                .Replace("*", string.Empty)
                .Replace("[", string.Empty)
                .Replace("]", string.Empty);
        }
    }
}