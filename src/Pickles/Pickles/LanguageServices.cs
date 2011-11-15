namespace Pickles
{
    using System;

    using gherkin;

    using Pickles.Parser;

    public class LanguageServices
    {
        private readonly Configuration configuration;

        public LanguageServices(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public I18n GetLanguage()
        {
            return new I18n(this.configuration.Language);
        }

        public string GetKeywordString(Keyword keyword)
        {
            var language = this.GetLanguage();
            return string.Format("{0}", language.keywords(keyword.ToString().ToLower()).get(1));
        }
    }
}
