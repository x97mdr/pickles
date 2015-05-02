using System;

namespace PicklesDoc.Pickles.ObjectModel
{
    public class Mapper
    {
        static Mapper()
        {
            AutoMapper.Mapper.CreateMap<Gherkin3.Ast.TableCell, string>().ConstructUsing(cell => cell.Value);
        }

        public string MapToString(Gherkin3.Ast.TableCell cell)
        {
            return AutoMapper.Mapper.Map<Gherkin3.Ast.TableCell, string>(cell);
        }
    }
}
