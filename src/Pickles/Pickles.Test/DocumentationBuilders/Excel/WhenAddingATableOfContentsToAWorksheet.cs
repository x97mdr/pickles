using System;
using ClosedXML.Excel;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.Excel;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.Excel
{
    //[TestFixture]
    public class WhenAddingATableOfContentsToAWorksheet : BaseFixture
    {
        //[Test]
        [Ignore(
            "The table of contents needs a tree of features to function and there is no easy facility at this time to build it"
            )]
        public void ThenCanAddTableOfContentsWorksheetSuccessfully()
        {
            var excelTableOfContentsFormatter = Container.Resolve<ExcelTableOfContentsFormatter>();
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                excelTableOfContentsFormatter.Format(workbook, null);
            }
        }
    }
}