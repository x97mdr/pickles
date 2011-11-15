namespace Pickles
{
    using System;
    using System.Diagnostics;

    using gherkin;
    using gherkin.lexer;

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

        public Lexer GetNativeLexer(Listener parser)
        {
            var typeName = string.Format("gherkin.lexer.i18n.{0}, {1}", configuration.Language.ToUpper(), typeof(I18nLexer).Assembly.FullName);
            
            var lexerType = Type.GetType(typeName);
            
            return Activator.CreateInstance(lexerType, parser) as Lexer;
        }
    }
}
