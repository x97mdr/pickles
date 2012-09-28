using System.Collections.Generic;
using ClosedXML.Excel;
using NUnit.Framework;
using Autofac;
using Pickles.DocumentationBuilders.Excel;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders
{
    [TestFixture]
    public class WhenAddingATableToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenTableAddedSuccessfully()
        {
            var excelTableFormatter = Container.Resolve<ExcelTableFormatter>();
            var table = new Table();
            table.HeaderRow = new TableRow("Var1", "Var2", "Var3", "Var4");
            table.DataRows =
                new List<TableRow>(new[] {new TableRow("1", "2", "3", "4"), new TableRow("5", "6", "7", "8")});

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                int row = 6;
                excelTableFormatter.Format(worksheet, table, ref row);

                worksheet.Cell("D6").Value.ShouldEqual("Var1");
                worksheet.Cell("E6").Value.ShouldEqual("Var2");
                worksheet.Cell("F6").Value.ShouldEqual("Var3");
                worksheet.Cell("G6").Value.ShouldEqual("Var4");
                worksheet.Cell("D7").Value.ShouldEqual(1.0);
                worksheet.Cell("E7").Value.ShouldEqual(2.0);
                worksheet.Cell("F7").Value.ShouldEqual(3.0);
                worksheet.Cell("G7").Value.ShouldEqual(4.0);
                worksheet.Cell("D8").Value.ShouldEqual(5.0);
                worksheet.Cell("E8").Value.ShouldEqual(6.0);
                worksheet.Cell("F8").Value.ShouldEqual(7.0);
                worksheet.Cell("G8").Value.ShouldEqual(8.0);
                row.ShouldEqual(9);
            }
        }
    }
}