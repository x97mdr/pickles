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
using System.Text;
using Pickles.Parser;
using gherkin.lexer;

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
            var encoding = GetEncoding(filename);
            using (var reader = new StreamReader(filename, encoding))
            {
                try
                {
                    feature = Parse(reader);
                }
                catch (Exception e)
                {
                    string message =
                        string.Format("There was an error parsing the feature file here: {0}{1}Errormessage was:'{2}'",
                                      Path.GetFullPath(filename),
                                      Environment.NewLine,
                                      e.Message);
                    throw new FeatureParseException(message, e);
                }

                reader.Close();
            }

            return feature;
        }

        private Encoding GetEncoding(string filename)
        {
            // Read the BOM            
            using (var file = File.OpenRead(filename))
            {
                var bom = new byte[4];
                file?.Read(bom, 0, 4);
                if (file != null)
                {
                    if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
                    if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
                    if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
                    if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
                    if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
                }
            }
            return Encoding.Default;
        }

        public Feature Parse(TextReader featureFileReader)
        {
            string fileContent = featureFileReader.ReadToEnd();

            var parser = new PicklesParser(languageService.GetLanguage());
            Lexer lexer = languageService.GetNativeLexer(parser);
            lexer.scan(fileContent);

            return parser.GetFeature();
        }
    }
}