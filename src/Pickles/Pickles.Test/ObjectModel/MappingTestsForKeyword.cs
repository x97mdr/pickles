//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MappingTestsForKeyword.cs" company="PicklesDoc">
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
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.ObjectModel;
using G = Gherkin3.Ast;
namespace PicklesDoc.Pickles.Test.ObjectModel
{
    [TestFixture]
    public class MappingTestsForKeyword
    {
        private readonly Factory factory = new Factory();

        [Test]
        public void MapToKeyword_NullString_ReturnsDefaultKeyword()
        {
            var mapper = this.factory.CreateMapper();

            var result = mapper.MapToKeyword(null);

            Check.That(result).IsEqualTo(default(Keyword));
        }

        [Test]
        public void MapToKeyword_GivenInSwedish_ReturnsGiven()
        {
            var mapper = this.factory.CreateMapper("sv");

            var result = mapper.MapToKeyword("Givet");

            Check.That(result).IsEqualTo(Keyword.Given);
        }

        [Test]
        public void MapToKeyword_WhenInSwedish_ReturnsWhen()
        {
            var mapper = this.factory.CreateMapper("sv");

            var result = mapper.MapToKeyword("När");

            Check.That(result).IsEqualTo(Keyword.When);
        }

        [Test]
        public void MapToKeyword_ThenInSwedish_ReturnsThen()
        {
            var mapper = this.factory.CreateMapper("sv");

            var result = mapper.MapToKeyword("Så");

            Check.That(result).IsEqualTo(Keyword.Then);
        }

        [Test]
        public void MapToKeyword_AndInSwedish_ReturnsAnd()
        {
            var mapper = this.factory.CreateMapper("sv");

            var result = mapper.MapToKeyword("Och");

            Check.That(result).IsEqualTo(Keyword.And);
        }

        [Test]
        public void MapToKeyword_ButInSwedish_ReturnsBut()
        {
            var mapper = this.factory.CreateMapper("sv");

            var result = mapper.MapToKeyword("Men");

            Check.That(result).IsEqualTo(Keyword.But);
        }

        [Test]
        public void MapToKeyword_KeywordIsProvidedWithBlank_ReturnsKeyword()
        {
            var mapper = this.factory.CreateMapper();

            var result = mapper.MapToKeyword("Then ");

            Check.That(result).IsEqualTo(Keyword.Then);
        }
    }
}