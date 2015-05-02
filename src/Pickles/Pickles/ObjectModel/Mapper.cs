using System;
using System.Linq;

namespace PicklesDoc.Pickles.ObjectModel
{
    public class Mapper
    {
        static Mapper()
        {
            AutoMapper.Mapper.CreateMap<Gherkin3.Ast.TableCell, string>().ConstructUsing(cell => cell.Value);
            AutoMapper.Mapper.CreateMap<Gherkin3.Ast.TableRow, TableRow>()
                .ConstructUsing(
                    row =>
                        new TableRow(
                            row.Cells.Select(AutoMapper.Mapper.Map<string>)));
        }

        public string MapToString(Gherkin3.Ast.TableCell cell)
        {
            return AutoMapper.Mapper.Map<string>(cell);
        }

        public TableRow MapToTableRow(Gherkin3.Ast.TableRow tableRow)
        {
            return AutoMapper.Mapper.Map<TableRow>(tableRow);
        }

        public Table MapToTable(Gherkin3.Ast.DataTable dataTable)
        {
            return AutoMapper.Mapper.Map<Table>(dataTable);
        }
    }
}
