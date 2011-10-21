using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Pickles.Formatters;
using Pickles.Parser;
using gherkin.lexer;

namespace Pickles
{
    public class FeatureParser
    {
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

            var parser = new PicklesParser();
            var listener = new I18nLexer(parser);
            listener.scan(fileContent);

            return parser.GetFeature();
        }
    }
}
