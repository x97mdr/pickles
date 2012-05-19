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

namespace Pickles.Test.DocumentationBuilders
{
    [TestFixture]
    public class WhenAddingAStepToAWorksheet : BaseFixture
    {
        [Test]
        public void ThenStepAddedSuccessfully()
        {
            var excelStepFormatter = Kernel.Get<ExcelStepFormatter>();
            var step = new Step() { NativeKeyword = "Given", Name = "I have some precondition" };

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("SHEET1");
                int row = 5;
                excelStepFormatter.Format(worksheet, step, ref row);

                worksheet.Cell("C5").Value.ShouldEqual(step.NativeKeyword);
                worksheet.Cell("D5").Value.ShouldEqual(step.Name);
            }
        }
    }
}
