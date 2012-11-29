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
    public class WhenAddingAFeatureToAWorksheet : BaseFixture
    {
        [Test]
        public void Then_feature_is_added_successfully()
        {
            var excelFeatureFormatter = Container.Resolve<ExcelFeatureFormatter>();
            var feature = new Feature
                              {
                                  Name = "Test Feature",
                                  Description =
                                      "In order to test this feature,\nAs a developer\nI want to test this feature"
                              };

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                excelFeatureFormatter.Format(worksheet, feature);

                worksheet.Cell("A1").Value.ShouldEqual(feature.Name);
                worksheet.Cell("B2").Value.ShouldEqual(feature.Description);
            }
        }
    }
}