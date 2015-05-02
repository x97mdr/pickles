//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="MapperTests.cs" company="PicklesDoc">
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

using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Test.ObjectModel
{
    [TestFixture]
    public class MapperTests
    {
        private const Gherkin3.Ast.Location AnyLocation = null;

        [Test]
        public void MapToStringTableCell_TableCellWithValue_ReturnsThatValue()
        {
            var cell = CreateGherkinTableCell("My cell value");

            var mapper = CreateMapper();

            string result = mapper.MapToString(cell);

            Check.That(result).IsEqualTo("My cell value");
        }

        private static Gherkin3.Ast.TableCell CreateGherkinTableCell(string cellValue)
        {
            return new Gherkin3.Ast.TableCell(AnyLocation, cellValue);
        }

        private static Mapper CreateMapper()
        {
            var mapper = new Mapper();
            return mapper;
        }

        [Test]
        public void MapToStringTableCell_NullTableCell_ReturnsNull()
        {
            var mapper = new Mapper();

            string result = mapper.MapToString(null);

            Check.That(result).IsNull();
        }

        [Test]
        public void MapToTableRow_NullTableRow_ReturnsNull()
        {
            var mapper = new Mapper();

            TableRow result = mapper.MapToTableRow(null);

            Check.That(result).IsNull();
        }

        [Test]
        public void MapToTableRow_RowWithCellValues_ReturnsRowContainingThoseValues()
        {
            Gherkin3.Ast.TableRow row = new Gherkin3.Ast.TableRow(
                AnyLocation,
                new[]
                {
                    CreateGherkinTableCell("first cell"),
                    CreateGherkinTableCell("second cell")
                });

            var mapper = new Mapper();

            var result = mapper.MapToTableRow(row);

            Check.That(result).ContainsExactly("first cell", "second cell");
        }

        [Test]
        public void MapToTable_NullDataTable_ReturnsNullTable()
        {
            var mapper = new Mapper();

            Table result = mapper.MapToTable(null);

            Check.That(result).IsNull();
        }
    }
}