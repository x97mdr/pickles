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

        [Given(@"the feature file at (.*)")]
        public void GivenTheFeatureFileAt(string path)
        {
            this.context.FeatureFileContent = File.ReadAllText(path);
        }

        //[When(@"I generate documentation")]
        //public void WhenIGenerateDocumentation()
        //{
        //    var parser = new FeatureParser(new CultureInfo("en"));

        //    using (var reader = new StringReader(this.context.FeatureFileContent))
        //    {
        //        this.context.ParserOutput = parser.Parse(reader);
        //    }
        //}

        //[Then(@"I should see this XHTML file")]
        //public void ThenIShouldSeeThisXHTMLFile(string expectedHtmlOutput)
        //{
        //    var expected = XDocument.Parse(expectedHtmlOutput).ToString(SaveOptions.DisableFormatting);
        //    var actual = this.context.ParserOutput.ToString(SaveOptions.DisableFormatting);

        //    StringAssert.AreEqualIgnoringCase(expected, actual);
        //}

        //[Then(@"I should get the XHTML file at (.*)")]
        //public void ThenIShouldGetTheXHTMLFileAt(string path)
        //{
        //    var expected = XDocument.Load(path).ToString(SaveOptions.DisableFormatting);
        //    var actual = this.context.ParserOutput.ToString(SaveOptions.DisableFormatting);

        //    StringAssert.AreEqualIgnoringCase(expected, actual);
        //}
    }
}
