using System;
using System.Linq;
using AutoMapper;

namespace PicklesDoc.Pickles.ObjectModel
{
    public class KeywordResolver : ITypeConverter<string, Keyword>
    {
        private readonly string language;

        public KeywordResolver(string language)
        {
            this.language = language;
        }

        public Keyword Convert(ResolutionContext context)
        {
            string source = (string) context.SourceValue;
            return this.MapToKeyword(source);
        }

        private Keyword MapToKeyword(string keyword)
        {
            keyword = keyword.Trim();

            var dialectProvider = new Gherkin3.GherkinDialectProvider();

            var gherkinDialect = new LanguageServices(new Configuration() { Language = this.language });

            if (gherkinDialect.WhenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.When;
            }
            if (gherkinDialect.GivenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.Given;
            }
            if (gherkinDialect.ThenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.Then;
            }
            if (gherkinDialect.AndStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.And;
            }
            if (gherkinDialect.ButStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.But;
            }

            throw new ArgumentOutOfRangeException("keyword");
        }
    }
}