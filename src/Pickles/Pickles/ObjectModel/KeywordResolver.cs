using System;
using System.Linq;
using AutoMapper;

namespace PicklesDoc.Pickles.ObjectModel
{
    public class KeywordResolver : ITypeConverter<string, Keyword>
    {
        private readonly LanguageServices languageServices;

        public KeywordResolver(string language)
        {
            this.languageServices = new LanguageServices(language);
        }

        public Keyword Convert(ResolutionContext context)
        {
            string source = (string) context.SourceValue;
            return this.MapToKeyword(source);
        }

        private Keyword MapToKeyword(string keyword)
        {
            keyword = keyword.Trim();

            if (this.languageServices.WhenStepKeywords.Contains(keyword))
            {
                return Keyword.When;
            }
            if (this.languageServices.GivenStepKeywords.Contains(keyword))
            {
                return Keyword.Given;
            }
            if (this.languageServices.ThenStepKeywords.Contains(keyword))
            {
                return Keyword.Then;
            }
            if (this.languageServices.AndStepKeywords.Contains(keyword))
            {
                return Keyword.And;
            }
            if (this.languageServices.ButStepKeywords.Contains(keyword))
            {
                return Keyword.But;
            }

            throw new ArgumentOutOfRangeException("keyword");
        }
    }
}