using System;
using ClosedXML.Excel;
using NUnit.Framework;
using Autofac;
using Pickles.DocumentationBuilders.Excel;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders.Excel
{
    [TestFixture]
    public class WhenAddingAStepToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenStepAddedSuccessfully()
        {
            var excelStepFormatter = Container.Resolve<ExcelStepFormatter>();
            var step = new Step {NativeKeyword = "Given", Name = "I have some precondition"};

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                int row = 5;
                excelStepFormatter.Format(worksheet, step, ref row);

                worksheet.Cell("C5").Value.ShouldEqual(step.NativeKeyword);
                worksheet.Cell("D5").Value.ShouldEqual(step.Name);
            }
        }
    }
}