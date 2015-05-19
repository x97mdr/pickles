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
        private readonly Lazy<GherkinDialect> languageLazy;

        private readonly Lazy<string[]> givenStepKeywordsLazy;
        private readonly Lazy<string[]> whenStepKeywordsLazy;
        private readonly Lazy<string[]> thenStepKeywordsLazy;
        private readonly Lazy<string[]> andStepKeywordsLazy;
        private readonly Lazy<string[]> butStepKeywordsLazy;

        public LanguageServices(Configuration configuration)
            : this(configuration.Language)
        {
        }

        public LanguageServices(string language = "en")
        {
            this.languageLazy = new Lazy<GherkinDialect>(() => new GherkinDialectProvider().GetDialect(language, null));
            this.whenStepKeywordsLazy = new Lazy<string[]>(() => this.Language.WhenStepKeywords.Select(s => s.Trim()).ToArray());
            this.givenStepKeywordsLazy = new Lazy<string[]>(() => this.Language.GivenStepKeywords.Select(s => s.Trim()).ToArray());
            this.thenStepKeywordsLazy = new Lazy<string[]>(() => this.Language.ThenStepKeywords.Select(s => s.Trim()).ToArray());
            this.andStepKeywordsLazy = new Lazy<string[]>(() => this.Language.AndStepKeywords.Select(s => s.Trim()).ToArray());
            this.butStepKeywordsLazy = new Lazy<string[]>(() => this.Language.ButStepKeywords.Select(s => s.Trim()).ToArray());
        }

        public string[] GivenStepKeywords { get { return this.givenStepKeywordsLazy.Value; } }
        public string[] WhenStepKeywords { get { return this.whenStepKeywordsLazy.Value; } }
        public string[] ThenStepKeywords { get { return this.thenStepKeywordsLazy.Value; } }
        public string[] AndStepKeywords { get { return this.andStepKeywordsLazy.Value; } }
        public string[] ButStepKeywords { get { return this.butStepKeywordsLazy.Value; } }

        private GherkinDialect Language
        {
            get { return this.languageLazy.Value; }
        }

        private string[] GetBackgroundKeywords()
        {
            return this.Language.BackgroundKeywords;
        }

        public string GetKeyword(string key)
        {
            var keywords = this.GetBackgroundKeywords();

            return keywords.FirstOrDefault();
        }
    }
}