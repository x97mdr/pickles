using ClosedXML.Excel;
using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.Excel;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders
{
    [TestFixture]
    public class WhenCreatingSheetNamesFromFeatures : BaseFixture
    {
        [Test]
        public void ThenCanCreateSimpleNameSuccessfully()
        {
            var excelSheetNameGenerator = Kernel.Get<ExcelSheetNameGenerator>();
            var feature = new Feature {Name = "A short feature name"};

            string name;
            using (var wb = new XLWorkbook())
            {
                name = excelSheetNameGenerator.GenerateSheetName(wb, feature);
            }

            name.ShouldEqual("ASHORTFEATURENAME");
        }

        [Test]
        public void ThenCanShortenLongDuplicatedNameSuccessfully()
        {
            var excelSheetNameGenerator = Kernel.Get<ExcelSheetNameGenerator>();
            var feature1 = new Feature {Name = "This is a really really long feature name that needs to be shortened A"};
            var feature2 = new Feature {Name = "This is a really really long feature name that needs to be shortened B"};

            string name1;
            string name2;
            using (var wb = new XLWorkbook())
            {
                name1 = excelSheetNameGenerator.GenerateSheetName(wb, feature1);
                wb.AddWorksheet(name1);
                name2 = excelSheetNameGenerator.GenerateSheetName(wb, feature2);
            }

            name2.ShouldEqual("THISISAREALLYREALLYLONGFEATU(1)");
        }

        [Test]
        public void ThenCanShortenLongMultipleDuplicatedNameSuccessfully()
        {
            var excelSheetNameGenerator = Kernel.Get<ExcelSheetNameGenerator>();
            var feature1 = new Feature {Name = "This is a really really long feature name that needs to be shortened A"};
            var feature2 = new Feature {Name = "This is a really really long feature name that needs to be shortened B"};
            var feature3 = new Feature {Name = "This is a really really long feature name that needs to be shortened C"};

            string name1;
            string name2;
            string name3;
            using (var wb = new XLWorkbook())
            {
                name1 = excelSheetNameGenerator.GenerateSheetName(wb, feature1);
                wb.AddWorksheet(name1);
                name2 = excelSheetNameGenerator.GenerateSheetName(wb, feature2);
                wb.AddWorksheet(name2);
                name3 = excelSheetNameGenerator.GenerateSheetName(wb, feature3);
            }

            name3.ShouldEqual("THISISAREALLYREALLYLONGFEATU(2)");
        }

        [Test]
        public void ThenCanShortenLongNameSuccessfully()
        {
            var excelSheetNameGenerator = Kernel.Get<ExcelSheetNameGenerator>();
            var feature = new Feature {Name = "This is a really really long feature name that needs to be shortened"};

            string name;
            using (var wb = new XLWorkbook())
            {
                name = excelSheetNameGenerator.GenerateSheetName(wb, feature);
            }

            name.ShouldEqual("THISISAREALLYREALLYLONGFEATUREN");
        }

        [Test]
        public void ThenItWillRemoveUnnecessaryAndInvalidCharacters()
        {
            var excelSheetNameGenerator = Kernel.Get<ExcelSheetNameGenerator>();
            var feature = new Feature { Name = @"This Had invalid characters: :\/?*[]" };
            
            string name;
            using (var wb = new XLWorkbook())
            {
                name = excelSheetNameGenerator.GenerateSheetName(wb, feature);
            }

            name.ShouldEqual("THISHADINVALIDCHARACTERS");
        }
    }
}