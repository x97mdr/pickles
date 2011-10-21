using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Pickles.Formatters;
using Pickles.Parser;

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
        [Ignore("The expected results files need some modification based on the latest changes to the formatters")]
        public void Can_Parse_Feature_Files_Successfully(string featureText, string xhtmlText)
        {
            var parser = new FeatureParser();
            var htmlDocumentFormatter = BuildDocumentFormatter();

            string actual;
            using (var reader = new StringReader(featureText))
            {
                var feature = parser.Parse(reader);
                var document = htmlDocumentFormatter.Format(new FeatureNode
                {
                    Feature = feature
                });
                actual = document.ToString(SaveOptions.DisableFormatting);
            }

            var expected = XDocument.Parse(xhtmlText).ToString(SaveOptions.DisableFormatting);

            StringAssert.AreEqualIgnoringCase(expected, actual);
        }

    }
}
