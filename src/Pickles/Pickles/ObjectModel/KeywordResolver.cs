//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="KeywordResolver.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

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
            string source = (string)context.SourceValue;
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
