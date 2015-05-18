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

        public java.util.List GetKeywords(string key)
        {
            return this.GetLanguage().keywords(key);
        }

        public string GetKeyword(string key)
        {
            var keywords = this.GetKeywords("background");

            if (keywords != null && !keywords.isEmpty())
            {
                return keywords.get(0).ToString();
            }

            return null;
        }

        private I18n GetLanguage()
        {
            if (this.currentCulture == null)
                return new I18n("en");

            return new I18n(this.currentCulture.TwoLetterISOLanguageName);
        }
    }
}