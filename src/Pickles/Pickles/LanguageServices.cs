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
using System.Globalization;
using gherkin;
using gherkin.lexer;

namespace PicklesDoc.Pickles
{
    public class LanguageServices
    {
        private readonly CultureInfo currentCulture;

        public LanguageServices(Configuration configuration)
        {
            if (!string.IsNullOrEmpty(configuration.Language))
                this.currentCulture = CultureInfo.GetCultureInfo(configuration.Language);
        }

        public I18n GetLanguage()
        {
            if (this.currentCulture == null)
                return new I18n("en");

            return new I18n(this.currentCulture.TwoLetterISOLanguageName);
        }

        public Lexer GetNativeLexer(Listener parser)
        {
            if (this.currentCulture == null)
                return new I18nLexer(parser);

            string typeName = string.Format("gherkin.lexer.i18n.{0}, {1}",
                                            this.currentCulture.TwoLetterISOLanguageName.ToUpper(),
                                            typeof (I18nLexer).Assembly.FullName);

            Type lexerType = Type.GetType(typeName);

            if (lexerType == null)
                throw new ApplicationException(
                    string.Format("The specified language '{1}' with language code '{0}' is not supported!",
                                  this.currentCulture.TwoLetterISOLanguageName.ToUpper(), this.currentCulture.NativeName));

            return Activator.CreateInstance(lexerType, parser) as Lexer;
        }
    }
}