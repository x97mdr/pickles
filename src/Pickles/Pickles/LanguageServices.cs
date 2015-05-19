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
using System.Linq;
using Gherkin3;

namespace PicklesDoc.Pickles
{
    public class LanguageServices
    {
        private readonly string language;
        private readonly GherkinDialectProvider dialectProvider;
        private readonly Lazy<GherkinDialect> languageLazy;

        public LanguageServices(Configuration configuration)
            : this(configuration.Language)
        {
        }

        public LanguageServices(string language = "en")
        {
            this.language = string.IsNullOrWhiteSpace(language) ? "en" : language;

            this.dialectProvider = new GherkinDialectProvider();
            this.languageLazy = new Lazy<GherkinDialect>(() => this.dialectProvider.GetDialect(this.language, null));
            this.whenStepKeywordsLazy = new Lazy<string[]>(() => this.Language().WhenStepKeywords.Select(s => s.Trim()).ToArray());
        }

        private readonly Lazy<string[]> whenStepKeywordsLazy;

        public string[] WhenStepKeywords
        {
            get
            {
                return this.whenStepKeywordsLazy.Value;
            }
        }

        public string[] GivenStepKeywords { get { return this.Language().GivenStepKeywords; } }
        public string[] ThenStepKeywords { get { return this.Language().ThenStepKeywords; } }
        public string[] AndStepKeywords { get { return this.Language().AndStepKeywords; } }
        public string[] ButStepKeywords { get { return this.Language().ButStepKeywords; } }

        private string[] GetBackgroundKeywords()
        {
            return this.Language().BackgroundKeywords;
        }

        public string GetKeyword(string key)
        {
            var keywords = this.GetBackgroundKeywords();

            return keywords.FirstOrDefault();
        }

        private GherkinDialect Language()
        {
            return this.languageLazy.Value;
        }
    }
}