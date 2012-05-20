using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;

namespace Pickles.Test.HtmlFormatterTestFiles
{
    [TestFixture]
    public class ParserTests : BaseFixture
    {
        [Test, TestCaseSource(typeof (ParserFileFactory), "Files")]
        [Ignore("The expected results files need some modification based on the latest changes to the formatters")]
        public void Can_Parse_Feature_Files_Successfully(string featureText, string xhtmlText)
        {
            var parser = Kernel.Get<FeatureParser>();
            var htmlDocumentFormatter = Kernel.Get<HtmlFeatureFormatter>();

            string actual;
            using (var reader = new StringReader(featureText))
            {
                Feature feature = parser.Parse(reader);
                XElement document = htmlDocumentFormatter.Format(feature);
                actual = document.ToString(SaveOptions.DisableFormatting);
            }

            string expected = XDocument.Parse(xhtmlText).ToString(SaveOptions.DisableFormatting);

            StringAssert.AreEqualIgnoringCase(expected, actual);
        }
    }
}