namespace Pickles
{
    using System;
    using System.Globalization;

    using gherkin;
    using gherkin.lexer;

    using Pickles.Parser;

    public class LanguageServices
    {
        private readonly CultureInfo currentCulture; 

        public LanguageServices(Configuration configuration)
        {
            currentCulture = CultureInfo.GetCultureInfo(configuration.Language);
        }

        public I18n GetLanguage()
        {
            return new I18n(currentCulture.TwoLetterISOLanguageName);
        }

        public string GetKeywordString(Keyword keyword)
        {
            var language = this.GetLanguage();
            return string.Format("{0}", language.keywords(keyword.ToString().ToLower()).get(1));
        }

        public Lexer GetNativeLexer(Listener parser)
        {
            var typeName = string.Format("gherkin.lexer.i18n.{0}, {1}", currentCulture.TwoLetterISOLanguageName.ToUpper(), typeof(I18nLexer).Assembly.FullName);

            var lexerType = Type.GetType(typeName);

            if (lexerType == null)
                throw new ApplicationException(string.Format("The specified language '{1}' with language code '{0}' is not supported!", currentCulture.TwoLetterISOLanguageName.ToUpper(), currentCulture.NativeName));
            
            return Activator.CreateInstance(lexerType, parser) as Lexer;
        }
    }
}
