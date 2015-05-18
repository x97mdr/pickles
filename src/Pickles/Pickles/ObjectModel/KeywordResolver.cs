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

            if (dialectProvider.GetDialect(this.language, null).WhenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.When;
            }
            if (dialectProvider.GetDialect(this.language, null).GivenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.Given;
            }
            if (dialectProvider.GetDialect(this.language, null).ThenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.Then;
            }
            if (dialectProvider.GetDialect(this.language, null).AndStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.And;
            }
            if (dialectProvider.GetDialect(this.language, null).ButStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.But;
            }

            throw new ArgumentOutOfRangeException("keyword");
        }
    }
}