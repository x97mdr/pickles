using System;
using ClosedXML.Excel;
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.Excel;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.Excel
{
    [TestFixture]
    public class WhenAddingADocumentStringToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenDocumentStringAddedSuccessfully()
        {
            var excelDocumentStringFormatter = new ExcelDocumentStringFormatter();
            string documentString = @"This is an example
document string for use
in testing";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                int row = 7;
                excelDocumentStringFormatter.Format(worksheet, documentString, ref row);

                Check.That(worksheet.Cell("D7").Value).IsEqualTo("This is an example");
                Check.That(worksheet.Cell("D8").Value).IsEqualTo("document string for use");
                Check.That(worksheet.Cell("D9").Value).IsEqualTo("in testing");
                Check.That(row).IsEqualTo(10);
            }
        }
    }
}