using ClosedXML.Excel;
using NGenerics.DataStructures.Trees;
using Ninject;
using NUnit.Framework;
using Pickles.DocumentationBuilders.Excel;
using Pickles.DirectoryCrawler;

namespace Pickles.Test.DocumentationBuilders.Excel
{
    [TestFixture]
    public class WhenAddingATableOfContentsToAWorksheet : BaseFixture
    {
        [Test]
        [Ignore("The table of contents needs a tree of features to function and there is no easy facility at this time to build it")]
        public void ThenCanAddTableOfContentsWorksheetSuccessfully()
        {
            var excelTableOfContentsFormatter = Kernel.Get<ExcelTableOfContentsFormatter>();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.AddWorksheet("SHEET1");
                excelTableOfContentsFormatter.Format(workbook, null);
            }
        }
    }
}
