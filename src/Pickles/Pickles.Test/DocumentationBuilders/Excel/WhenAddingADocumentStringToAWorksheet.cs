using ClosedXML.Excel;
using NUnit.Framework;
using Autofac;
using Pickles.DocumentationBuilders.Excel;
using Should;

namespace Pickles.Test.DocumentationBuilders.Excel
{
    [TestFixture]
    public class WhenAddingADocumentStringToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenDocumentStringAddedSuccessfully()
        {
            var excelDocumentStringFormatter = Container.Resolve<ExcelDocumentStringFormatter>();
            string documentString = @"This is an example
document string for use
in testing";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
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