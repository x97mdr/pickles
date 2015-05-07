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
using System.IO.Abstractions;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;
using gherkin.lexer;

using TextReader = System.IO.TextReader;

namespace PicklesDoc.Pickles
{
    public class FeatureParser
    {
        private readonly LanguageServices languageService;

        private readonly IFileSystem fileSystem;

        public FeatureParser(LanguageServices languageService, IFileSystem fileSystem)
        {
            this.languageService = languageService;
            this.fileSystem = fileSystem;
        }

        public Feature Parse(string filename)
        {
            Feature feature = null;
            using (var reader = this.fileSystem.FileInfo.FromFileName(filename).OpenText())
            {
                try
                {
                    feature = this.Parse(reader);
                }
                catch (Exception e)
                {
                    string message =
                        string.Format("There was an error parsing the feature file here: {0}{1}Errormessage was:'{2}'",
                                      this.fileSystem.Path.GetFullPath(filename),
                                      Environment.NewLine,
                                      e.Message);
                    throw new FeatureParseException(message, e);
                }

                reader.Close();
            }

            return feature;
        }

        public Feature Parse(TextReader featureFileReader)
        {
            var gherkinParser = new Gherkin3.Parser();
            Gherkin3.Ast.Feature feature = gherkinParser.Parse(featureFileReader);
            Feature result = new Mapper().MapToFeature(feature);

            return result;
        }
    }
}