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
            AutoMapper.Mapper.CreateMap<G.TableCell, string>()
                .ConstructUsing(cell => cell.Value);

            AutoMapper.Mapper.CreateMap<G.TableRow, TableRow>()
                .ConstructUsing(row => new TableRow(row.Cells.Select(AutoMapper.Mapper.Map<string>)));

            AutoMapper.Mapper.CreateMap<G.DataTable, Table>()
                .ForMember(t => t.HeaderRow, opt => opt.MapFrom(dt => dt.Rows.Take(1).Single()))
                .ForMember(t => t.DataRows, opt => opt.MapFrom(dt => dt.Rows.Skip(1)));

            AutoMapper.Mapper.CreateMap<G.DocString, string>().ConstructUsing(docString => docString.Content);

            AutoMapper.Mapper.CreateMap<G.Step, Step>().ConstructUsing(step =>
                new Step
                {
                    DocStringArgument = step.Argument is G.DocString ? AutoMapper.Mapper.Map<string>(step.Argument) : null,
                    NativeKeyword = step.Keyword,
                    Name = step.Text,
                    Keyword = Keyword.Given,
                    TableArgument = step.Argument is G.DataTable ? AutoMapper.Mapper.Map<Table>(step.Argument) : null
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

        public Step MapToStep(G.Step step)
        {
            return AutoMapper.Mapper.Map<Step>(step);
        }

        public Keyword MapToKeyword(string keyword)
        {
            try
            {
                return AutoMapper.Mapper.Map<Keyword>(keyword);
            }
            catch (AutoMapper.AutoMapperMappingException)
            {
                throw new ArgumentOutOfRangeException("keyword", keyword, "The keyword should correspond to the Keyword enumeration");
            } 
        }
    }
}
