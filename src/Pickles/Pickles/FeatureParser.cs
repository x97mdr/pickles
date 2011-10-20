using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Pickles.Formatters;
using Pickles.Parser;

namespace Pickles
{
    public class FeatureParser
    {
        private readonly GherkinDialectServices dialectServices;

        public FeatureParser()
            : this(System.Threading.Thread.CurrentThread.CurrentCulture)
        {
        }

        public FeatureParser(CultureInfo defaultLanguage)
        {
            this.dialectServices = new GherkinDialectServices(defaultLanguage);
        }

        public Feature Parse(string filename)
        {
            Feature feature = null;
            using (var reader = new StreamReader(filename))
            {
                feature = Parse(reader);
                reader.Close();
            }

            return feature;
        }

        public Feature Parse(TextReader featureFileReader)
        {
            var fileContent = featureFileReader.ReadToEnd();

            var language = this.dialectServices.GetLanguage(fileContent);

            var gherkinDialect = dialectServices.GetGherkinDialect(language);
            var gherkinListener = new GherkinParserListener(".");

            GherkinScanner scanner = new GherkinScanner(gherkinDialect, fileContent);
            scanner.Scan(gherkinListener);

            return gherkinListener.GetResult();
        }
    }
}
