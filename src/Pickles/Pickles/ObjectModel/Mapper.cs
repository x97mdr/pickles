using System;
using System.Collections.Generic;
using System.Linq;
using G = Gherkin3.Ast;

namespace PicklesDoc.Pickles.ObjectModel
{
    public class Mapper
    {
        static Mapper()
        {
            AutoMapper.Mapper.CreateMap<G.TableCell, string>().ConstructUsing(cell => cell.Value);
            AutoMapper.Mapper.CreateMap<G.TableRow, TableRow>()
                .ConstructUsing(
                    row =>
                        new TableRow(
                            row.Cells.Select(AutoMapper.Mapper.Map<string>)));
            AutoMapper.Mapper.CreateMap<G.DataTable, Table>()
                .ConstructUsing(
                    dataTable =>
                        new Table
                        {
                            HeaderRow = AutoMapper.Mapper.Map<TableRow>(dataTable.Rows.Take(1).Single()),
                            DataRows = new List<TableRow>(dataTable.Rows.Skip(1).Select(AutoMapper.Mapper.Map<TableRow>))
                        });
        }

        public string MapToString(G.TableCell cell)
        {
            return AutoMapper.Mapper.Map<string>(cell);
        }

        public TableRow MapToTableRow(G.TableRow tableRow)
        {
            return AutoMapper.Mapper.Map<TableRow>(tableRow);
        }

        public Table MapToTable(G.DataTable dataTable)
        {
            return AutoMapper.Mapper.Map<Table>(dataTable);
        }

        public string MapToString(G.DocString docString)
        {
            return AutoMapper.Mapper.Map<string>(docString);
        }
    }
}
