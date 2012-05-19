using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClosedXML.Excel;
using Ninject;
using NUnit.Framework;
using Pickles.DocumentationBuilders.Excel;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders.Excel
{
    [TestFixture]
    public class WhenAddingADocumentStringToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenDocumentStringAddedSuccessfully()
        {
            var excelDocumentStringFormatter = Kernel.Get<ExcelDocumentStringFormatter>();
            var documentString = @"This is an example
document string for use
in testing";

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("SHEET1");
                int row = 7;
                excelDocumentStringFormatter.Format(worksheet, documentString, ref row);

                worksheet.Cell("D7").Value.ShouldEqual("This is an example");
                worksheet.Cell("D8").Value.ShouldEqual("document string for use");
                worksheet.Cell("D9").Value.ShouldEqual("in testing");
                row.ShouldEqual(10);
            }
        }
    }
}
