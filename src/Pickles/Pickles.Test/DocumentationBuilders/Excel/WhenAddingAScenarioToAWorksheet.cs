using System.Collections.Generic;
using ClosedXML.Excel;
using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.Excel;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders
{
    [TestFixture]
    public class WhenAddingAScenarioToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenSingleScenarioAddedSuccessfully()
        {
            var excelScenarioFormatter = Kernel.Get<ExcelScenarioFormatter>();
            var scenario = new Scenario
                               {
                                   Name = "Test Feature",
                                   Description =
                                       "In order to test this feature,\nAs a developer\nI want to test this feature"
                               };

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                int row = 3;
                excelScenarioFormatter.Format(worksheet, scenario, ref row);

                worksheet.Cell("B3").Value.ShouldEqual(scenario.Name);
                worksheet.Cell("C4").Value.ShouldEqual(scenario.Description);
                row.ShouldEqual(5);
            }
        }

        [Test]
        public void ThenSingleScenarioWithStepsAddedSuccessfully()
        {
            var excelScenarioFormatter = Kernel.Get<ExcelScenarioFormatter>();
            var scenario = new Scenario
                               {
                                   Name = "Test Feature",
                                   Description =
                                       "In order to test this feature,\nAs a developer\nI want to test this feature"
                               };
            var given = new Step {NativeKeyword = "Given", Name = "a precondition"};
            var when = new Step {NativeKeyword = "When", Name = "an event occurs"};
            var then = new Step {NativeKeyword = "Then", Name = "a postcondition"};
            scenario.Steps = new List<Step>(new[] {given, when, then});

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                int row = 3;
                excelScenarioFormatter.Format(worksheet, scenario, ref row);

                worksheet.Cell("B3").Value.ShouldEqual(scenario.Name);
                worksheet.Cell("C4").Value.ShouldEqual(scenario.Description);
                row.ShouldEqual(8);
            }
        }
    }
}