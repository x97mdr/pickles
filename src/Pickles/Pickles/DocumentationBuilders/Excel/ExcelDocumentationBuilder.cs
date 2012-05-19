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
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using NGenerics.Patterns.Visitor;
using Pickles.DirectoryCrawler;
using ClosedXML.Excel;

namespace Pickles.DocumentationBuilders.Excel
{
    public class ExcelDocumentationBuilder : IDocumentationBuilder
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Configuration configuration;
        private readonly DirectoryTreeCrawler featureCrawler;
        private readonly ExcelFeatureFormatter excelFeatureFormatter;
        private readonly ExcelSheetNameGenerator excelSheetNameGenerator;
        private readonly ExcelTableOfContentsFormatter excelTableOfContentsFormatter;

        public ExcelDocumentationBuilder(Configuration configuration, DirectoryTreeCrawler featureCrawler, ExcelFeatureFormatter excelFeatureFormatter, ExcelSheetNameGenerator excelSheetNameGenerator, ExcelTableOfContentsFormatter excelTableOfContentsFormatter)
        {
            this.configuration = configuration;
            this.featureCrawler = featureCrawler;
            this.excelFeatureFormatter = excelFeatureFormatter;
            this.excelSheetNameGenerator = excelSheetNameGenerator;
            this.excelTableOfContentsFormatter = excelTableOfContentsFormatter;
        }

        #region IDocumentationBuilder Members

        public void Build(NGenerics.DataStructures.Trees.GeneralTree<DirectoryCrawler.IDirectoryTreeNode> features)
        {
            if (log.IsInfoEnabled)
            {
                log.InfoFormat("Writing Excel workbook to {0}", this.configuration.OutputFolder.FullName);
            }

            var spreadsheetPath = System.IO.Path.Combine(this.configuration.OutputFolder.FullName, "features.xlsx");
            using (var workbook = new XLWorkbook())
            {
                var actionVisitor = new ActionVisitor<IDirectoryTreeNode>(node =>
                {

                    var featureDirectoryTreeNode = node as FeatureDirectoryTreeNode;
                    if (featureDirectoryTreeNode != null)
                    {
                        var worksheet = workbook.AddWorksheet(this.excelSheetNameGenerator.GenerateSheetName(workbook, featureDirectoryTreeNode.Feature));
                        this.excelFeatureFormatter.Format(worksheet, featureDirectoryTreeNode.Feature);
                    }
                });

                features.AcceptVisitor(actionVisitor);

                this.excelTableOfContentsFormatter.Format(workbook, features);

                workbook.SaveAs(spreadsheetPath);
            }
        }

        #endregion
    }
}
