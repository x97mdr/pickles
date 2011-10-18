using System.Globalization;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using Pickles.Formatters;

namespace Pickles.Test.HtmlFormatterTestFiles
{
    [TestFixture]
    public class ParserTests
    {
        public HtmlDocumentFormatter BuildDocumentFormatter()
        {
            return new HtmlDocumentFormatter(new HtmlTableOfContentsFormatter(), new HtmlFeatureFormatter(new HtmlScenarioFormatter(new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter()))), new HtmlFooterFormatter());
        }

        [Test, TestCaseSource(typeof(ParserFileFactory), "Files")]
        public void Can_Parse_Feature_Files_Successfully(string featureText, string xhtmlText)
        {
            var parser = new FeatureParser(new CultureInfo("en"));
            var htmlDocumentFormatter = BuildDocumentFormatter();

            string actual;
            using (var reader = new StringReader(featureText))
            {
                var feature = parser.Parse(reader);
                var document = htmlDocumentFormatter.Format(new FeatureNode
                {
                    Feature = feature
                }, null);
                actual = document.ToString(SaveOptions.DisableFormatting);
            }

            var expected = XDocument.Parse(xhtmlText).ToString(SaveOptions.DisableFormatting);

            StringAssert.AreEqualIgnoringCase(expected, actual);
        }
    }
}
