#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Pickles.DocumentationBuilders.HTML;
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
