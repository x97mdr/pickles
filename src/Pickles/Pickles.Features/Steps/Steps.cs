using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;

namespace Pickles.Features.Steps
{
    [Binding]
    public class Steps
    {
        private readonly Context context;

        public Steps(Context context)
        {
            this.context = context;
        }

        [Given(@"this feature file")]
        public void GivenThisFeatureFile(string featureFile)
        {
            this.context.FeatureFileContent = featureFile;
        }

        [When(@"I generate documentation")]
        public void WhenIGenerateDocumentation()
        {
            var parser = new HtmlFeatureParser(new CultureInfo("en"));

            using (var reader = new StringReader(this.context.FeatureFileContent))
            {
                this.context.ParserOutput = parser.Parse(reader);
            }
        }

        [Then(@"I should see this XHTML file")]
        public void ThenIShouldSeeThisXHTMLFile(string expectedHtmlOutput)
        {
            var expected = XDocument.Parse(expectedHtmlOutput).ToString(SaveOptions.DisableFormatting);
            var actual = this.context.ParserOutput.ToString(SaveOptions.DisableFormatting);

            //Assert.IsTrue(XDocument.DeepEquals(expected, actual), "The actual XML was:\n" + actual.ToString(SaveOptions.OmitDuplicateNamespaces));
            StringAssert.AreEqualIgnoringCase(expected, actual);
        }
    }
}
