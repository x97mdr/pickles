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

using System;
using System.IO;
using Pickles.Parser;

namespace Pickles
{
    public class FeatureParser
    {
        private readonly LanguageServices languageService;

        public FeatureParser(LanguageServices languageService)
        {
            this.languageService = languageService;
        }

        public Feature Parse(string filename)
        {
            Feature feature = null;
            using (var reader = new StreamReader(filename))
            {
                try
                {
                    feature = Parse(reader);
                }
                catch (Exception e)
                {
                    throw new FeatureParseException("There was an error parsing the feature file here: " + Path.GetFullPath(filename), e);
                }

                reader.Close();
            }

            return feature;
        }

        public Feature Parse(TextReader featureFileReader)
        {
            var fileContent = featureFileReader.ReadToEnd();

            var parser = new PicklesParser(this.languageService.GetLanguage());
            var lexer = this.languageService.GetNativeLexer(parser);
            lexer.scan(fileContent);

            return parser.GetFeature();
        }
    }
}
