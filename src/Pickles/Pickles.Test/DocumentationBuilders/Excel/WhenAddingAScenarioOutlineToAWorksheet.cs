using System.Collections.Generic;
using ClosedXML.Excel;
using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.Excel;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders.Excel
{
    [TestFixture]
    public class WhenAddingAScenarioOutlineToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenSingleScenarioOutlineAddedSuccessfully()
        {
            var excelScenarioFormatter = Kernel.Get<ExcelScenarioOutlineFormatter>();
            var exampleTable = new Table();
            exampleTable.HeaderRow = new TableRow("Var1", "Var2", "Var3", "Var4");
            exampleTable.DataRows =
                new List<TableRow>(new[] {new TableRow("1", "2", "3", "4"), new TableRow("5", "6", "7", "8")});
            var example = new Example {Name = "Examples", Description = string.Empty, TableArgument = exampleTable};
            var scenarioOutline = new ScenarioOutline
                                      {
                                          Name = "Test Feature",
                                          Description =
                                              "In order to test this feature,\nAs a developer\nI want to test this feature",
                                          Example = example
                                      };

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                int row = 3;
                excelScenarioFormatter.Format(worksheet, scenarioOutline, ref row);

                worksheet.Cell("B3").Value.ShouldEqual(scenarioOutline.Name);
                worksheet.Cell("C4").Value.ShouldEqual(scenarioOutline.Description);
                worksheet.Cell("B6").Value.ShouldEqual("Examples");
                worksheet.Cell("D7").Value.ShouldEqual("Var1");
                worksheet.Cell("E7").Value.ShouldEqual("Var2");
                worksheet.Cell("F7").Value.ShouldEqual("Var3");
                worksheet.Cell("G7").Value.ShouldEqual("Var4");
                worksheet.Cell("D8").Value.ShouldEqual(1.0);
                worksheet.Cell("E8").Value.ShouldEqual(2.0);
                worksheet.Cell("F8").Value.ShouldEqual(3.0);
                worksheet.Cell("G8").Value.ShouldEqual(4.0);
                worksheet.Cell("D9").Value.ShouldEqual(5.0);
                worksheet.Cell("E9").Value.ShouldEqual(6.0);
                worksheet.Cell("F9").Value.ShouldEqual(7.0);
                worksheet.Cell("G9").Value.ShouldEqual(8.0);
                row.ShouldEqual(10);
            }
        }

        [Test]
        public void ThenSingleScenarioOutlineWithStepsAddedSuccessfully()
        {
            var excelScenarioFormatter = Kernel.Get<ExcelScenarioOutlineFormatter>();
            var exampleTable = new Table();
            exampleTable.HeaderRow = new TableRow("Var1", "Var2", "Var3", "Var4");
            exampleTable.DataRows =
                new List<TableRow>(new[] {new TableRow("1", "2", "3", "4"), new TableRow("5", "6", "7", "8")});
            var example = new Example {Name = "Examples", Description = string.Empty, TableArgument = exampleTable};
            var scenarioOutline = new ScenarioOutline
                                      {
                                          Name = "Test Feature",
                                          Description =
                                              "In order to test this feature,\nAs a developer\nI want to test this feature",
                                          Example = example
                                      };
            var given = new Step {NativeKeyword = "Given", Name = "a precondition"};
            var when = new Step {NativeKeyword = "When", Name = "an event occurs"};
            var then = new Step {NativeKeyword = "Then", Name = "a postcondition"};
            scenarioOutline.Steps = new List<Step>(new[] {given, when, then});

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                int row = 3;
                excelScenarioFormatter.Format(worksheet, scenarioOutline, ref row);

                worksheet.Cell("B3").Value.ShouldEqual(scenarioOutline.Name);
                worksheet.Cell("C4").Value.ShouldEqual(scenarioOutline.Description);
                worksheet.Cell("B9").Value.ShouldEqual("Examples");
                worksheet.Cell("D10").Value.ShouldEqual("Var1");
                worksheet.Cell("E10").Value.ShouldEqual("Var2");
                worksheet.Cell("F10").Value.ShouldEqual("Var3");
                worksheet.Cell("G10").Value.ShouldEqual("Var4");
                worksheet.Cell("D11").Value.ShouldEqual(1.0);
                worksheet.Cell("E11").Value.ShouldEqual(2.0);
                worksheet.Cell("F11").Value.ShouldEqual(3.0);
                worksheet.Cell("G11").Value.ShouldEqual(4.0);
                worksheet.Cell("D12").Value.ShouldEqual(5.0);
                worksheet.Cell("E12").Value.ShouldEqual(6.0);
                worksheet.Cell("F12").Value.ShouldEqual(7.0);
                worksheet.Cell("G12").Value.ShouldEqual(8.0);
                row.ShouldEqual(13);
            }
        }
    }
}