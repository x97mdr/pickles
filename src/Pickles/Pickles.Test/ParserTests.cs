using System.Globalization;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;

namespace Pickles.Test.HtmlFormatterTestFiles
{
    [TestFixture]
    public class ParserTests
    {
        [Test, TestCaseSource(typeof(ParserFileFactory), "Files")]
        public void Can_Parse_Feature_Files_Successfully(string featureText, string xhtmlText)
        {
            var parser = new HtmlFeatureParser(new CultureInfo("en"));

            string expected;
            using (var reader = new StringReader(featureText))
            {
                expected = parser.Parse(reader).ToString(SaveOptions.DisableFormatting);
            }

            var actual = XDocument.Parse(xhtmlText).ToString(SaveOptions.DisableFormatting);

            StringAssert.AreEqualIgnoringCase(expected, actual);
        }
    }
}
