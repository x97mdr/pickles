using System;
using System.Collections.Generic;
using ClosedXML.Excel;
using NFluent;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.Excel;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.Excel
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

                Check.That(worksheet.Cell("D6").Value).IsEqualTo("Var1");
                Check.That(worksheet.Cell("E6").Value).IsEqualTo("Var2");
                Check.That(worksheet.Cell("F6").Value).IsEqualTo("Var3");
                Check.That(worksheet.Cell("G6").Value).IsEqualTo("Var4");
                Check.That(worksheet.Cell("D7").Value).IsEqualTo(1.0);
                Check.That(worksheet.Cell("E7").Value).IsEqualTo(2.0);
                Check.That(worksheet.Cell("F7").Value).IsEqualTo(3.0);
                Check.That(worksheet.Cell("G7").Value).IsEqualTo(4.0);
                Check.That(worksheet.Cell("D8").Value).IsEqualTo(5.0);
                Check.That(worksheet.Cell("E8").Value).IsEqualTo(6.0);
                Check.That(worksheet.Cell("F8").Value).IsEqualTo(7.0);
                Check.That(worksheet.Cell("G8").Value).IsEqualTo(8.0);
                Check.That(row).IsEqualTo(9);
            }
        }
    }
}