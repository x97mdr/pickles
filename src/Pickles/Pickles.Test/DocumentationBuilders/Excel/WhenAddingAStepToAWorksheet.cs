using System;
using ClosedXML.Excel;
using NFluent;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.Excel;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.Excel
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

                Check.That(worksheet.Cell("C5").Value).IsEqualTo(step.NativeKeyword);
                Check.That(worksheet.Cell("D5").Value).IsEqualTo(step.Name);
            }
        }
    }
}