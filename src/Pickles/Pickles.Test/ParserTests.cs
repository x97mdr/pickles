using System;
using System.Xml.Linq;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class ParserTests : BaseFixture
    {
        [Test, TestCaseSource(typeof (ParserFileFactory), "Files")]
        [Ignore("The expected results files need some modification based on the latest changes to the formatters")]
        public void CanParseFeatureFilesSuccessfully(string featureText, string xhtmlText)
        {
            var parser = Container.Resolve<FeatureParser>();
            var htmlDocumentFormatter = Container.Resolve<HtmlFeatureFormatter>();

            string actual;
            using (var reader = new System.IO.StringReader(featureText))
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