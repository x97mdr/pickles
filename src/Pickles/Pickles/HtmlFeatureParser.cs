using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Pickles.Formatters;
using TechTalk.SpecFlow.Parser;
using TechTalk.SpecFlow.Parser.Gherkin;
using TechTalk.SpecFlow.Parser.GherkinBuilder;

namespace Pickles
{
    public class HtmlFeatureParser
    {
        private readonly GherkinDialectServices dialectServices;

        public HtmlFeatureParser(CultureInfo defaultLanguage)
        {
            this.dialectServices = new GherkinDialectServices(defaultLanguage);
        }

        public XDocument Parse(TextReader featureFileReader)
        {
            var fileContent = featureFileReader.ReadToEnd();

            var language = this.dialectServices.GetLanguage(fileContent);

            var gherkinDialect = dialectServices.GetGherkinDialect(language);
            var gherkinListener = new GherkinParserListener(".");

            GherkinScanner scanner = new GherkinScanner(gherkinDialect, fileContent);
            scanner.Scan(gherkinListener);

            var feature = gherkinListener.GetResult();

            var featureFormatter = new HtmlFeatureFormatter(new HtmlScenarioFormatter(new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter())));
            return featureFormatter.Format(feature);
        }
    }
}
