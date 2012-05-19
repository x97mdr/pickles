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
    public class WhenAddingAFeatureToAWorksheet : BaseFixture
    {
        [Test]
        public void Then_feature_is_added_successfully()
        {
            var excelFeatureFormatter = Kernel.Get<ExcelFeatureFormatter>();
            var feature = new Feature() { Name = "Test Feature", Description = "In order to test this feature,\nAs a developer\nI want to test this feature" }; 

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("SHEET1");
                excelFeatureFormatter.Format(worksheet, feature);

                worksheet.Cell("A1").Value.ShouldEqual(feature.Name);
                worksheet.Cell("B2").Value.ShouldEqual(feature.Description);
            }
        }
    }
}
