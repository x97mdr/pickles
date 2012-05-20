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

using System.IO;
using System.Reflection;
using ClosedXML.Excel;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using Pickles.DirectoryCrawler;
using log4net;

namespace Pickles.DocumentationBuilders.Excel
{
    public class ExcelDocumentationBuilder : IDocumentationBuilder
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Configuration configuration;
        private readonly ExcelFeatureFormatter excelFeatureFormatter;
        private readonly ExcelSheetNameGenerator excelSheetNameGenerator;
        private readonly ExcelTableOfContentsFormatter excelTableOfContentsFormatter;
        private readonly DirectoryTreeCrawler featureCrawler;

        public ExcelDocumentationBuilder(Configuration configuration, DirectoryTreeCrawler featureCrawler,
                                         ExcelFeatureFormatter excelFeatureFormatter,
                                         ExcelSheetNameGenerator excelSheetNameGenerator,
                                         ExcelTableOfContentsFormatter excelTableOfContentsFormatter)
        {
            this.configuration = configuration;
            this.featureCrawler = featureCrawler;
            this.excelFeatureFormatter = excelFeatureFormatter;
            this.excelSheetNameGenerator = excelSheetNameGenerator;
            this.excelTableOfContentsFormatter = excelTableOfContentsFormatter;
        }

        #region IDocumentationBuilder Members

        public void Build(GeneralTree<IDirectoryTreeNode> features)
        {
            if (log.IsInfoEnabled)
            {
                log.InfoFormat("Writing Excel workbook to {0}", configuration.OutputFolder.FullName);
            }

            string spreadsheetPath = Path.Combine(configuration.OutputFolder.FullName, "features.xlsx");
            using (var workbook = new XLWorkbook())
            {
                var actionVisitor = new ActionVisitor<IDirectoryTreeNode>(node =>
                                                                              {
                                                                                  var featureDirectoryTreeNode =
                                                                                      node as FeatureDirectoryTreeNode;
                                                                                  if (featureDirectoryTreeNode != null)
                                                                                  {
                                                                                      IXLWorksheet worksheet =
                                                                                          workbook.AddWorksheet(
                                                                                              excelSheetNameGenerator.
                                                                                                  GenerateSheetName(
                                                                                                      workbook,
                                                                                                      featureDirectoryTreeNode
                                                                                                          .Feature));
                                                                                      excelFeatureFormatter.Format(
                                                                                          worksheet,
                                                                                          featureDirectoryTreeNode.
                                                                                              Feature);
                                                                                  }
                                                                              });

                features.AcceptVisitor(actionVisitor);

                excelTableOfContentsFormatter.Format(workbook, features);

                workbook.SaveAs(spreadsheetPath);
            }
        }

        #endregion
    }
}