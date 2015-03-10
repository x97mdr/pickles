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

                Check.That(worksheet.Cell("A1").Value).IsEqualTo(feature.Name);
                Check.That(worksheet.Cell("B2").Value).IsEqualTo(feature.Description);
            }
        }

        [Test]
        public void Then_feature_with_background_is_added_successfully()
        {
            var excelFeatureFormatter = Container.Resolve<ExcelFeatureFormatter>();

            var feature = new Feature
                              {
                                  Name = "Test Feature",
                                  Description =
                                      "In order to test this feature,\nAs a developer\nI want to test this feature",
                              };
            var background = new Scenario
            {
                Name = "Test Background Scenario",
                Description =
                    "In order to test this background,\nAs a developer\nI want to test this background"
            };
            var given = new Step { NativeKeyword = "Given", Name = "a precondition" };
            background.Steps = new List<Step>(new[] { given });
            feature.AddBackground(background);

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                excelFeatureFormatter.Format(worksheet, feature);

                Check.That(worksheet.Cell("B4").Value).IsEqualTo(background.Name);
                Check.That(worksheet.Cell("C5").Value).IsEqualTo(background.Description);
                Check.That(worksheet.Cell("D6").Value).IsEqualTo(given.Name);
            }
        }

        [Test]
        public void Then_feature_without_background_adds_first_scenario_on_correct_row()
        {
            var excelFeatureFormatter = Container.Resolve<ExcelFeatureFormatter>();

            var feature = new Feature
                              {
                                  Name = "Test Feature",
                                  Description =
                                      "In order to test this feature,\nAs a developer\nI want to test this feature",
                              };
            var scenario = new Scenario
            {
                Name = "Test Scenario",
                Description =
                    "In order to test this scenario,\nAs a developer\nI want to test this scenario"
            };
            var given = new Step { NativeKeyword = "Given", Name = "a precondition" };
            scenario.Steps = new List<Step>(new[] { given });
            feature.AddFeatureElement(scenario);

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.AddWorksheet("SHEET1");
                excelFeatureFormatter.Format(worksheet, feature);

                Check.That(worksheet.Cell("B4").Value).IsEqualTo(scenario.Name);
                Check.That(worksheet.Cell("C5").Value).IsEqualTo(scenario.Description);
                Check.That(worksheet.Cell("D6").Value).IsEqualTo(given.Name);
            }
        }
    }
}