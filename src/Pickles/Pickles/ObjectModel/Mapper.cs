using System;
using System.Collections.Generic;
using System.Linq;
using G = Gherkin3.Ast;

namespace PicklesDoc.Pickles.ObjectModel
{
    public class Mapper
    {
        private readonly string featureLanguage;

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

            AutoMapper.Mapper.CreateMap<G.Step, Step>()
                .ForMember(t => t.NativeKeyword, opt => opt.MapFrom(s => s.Keyword))
                .ForMember(t => t.Name, opt => opt.MapFrom(s => s.Text))
                .ForMember(t => t.Keyword, opt => opt.MapFrom(s => s.Keyword))
                .ForMember(t => t.DocStringArgument, opt => opt.MapFrom(s => s.Argument is G.DocString ? s.Argument : null))
                .ForMember(t => t.TableArgument, opt => opt.MapFrom(s => s.Argument is G.DataTable ? s.Argument : null));

            AutoMapper.Mapper.CreateMap<G.Tag, string>()
                .ConstructUsing(tag => tag.Name);

            AutoMapper.Mapper.CreateMap<G.Scenario, Scenario>()
                .ForMember(t => t.Description, opt => opt.NullSubstitute(string.Empty));

            AutoMapper.Mapper.CreateMap<IEnumerable<G.TableRow>, Table>()
                .ForMember(t => t.HeaderRow, opt => opt.MapFrom(s => s.Take(1).Single()))
                .ForMember(t => t.DataRows, opt => opt.MapFrom(s => s.Skip(1)));

            AutoMapper.Mapper.CreateMap<G.Examples, Example>()
                .ForMember(t => t.TableArgument, opt => opt.MapFrom(s => ((G.IHasRows) s).Rows));

            AutoMapper.Mapper.CreateMap<G.ScenarioOutline, ScenarioOutline>()
                .ForMember(t => t.Description, opt => opt.NullSubstitute(string.Empty));

            AutoMapper.Mapper.CreateMap<G.Background, Scenario>()
                .ForMember(t => t.Description, opt => opt.NullSubstitute(string.Empty));

            AutoMapper.Mapper.CreateMap<G.ScenarioDefinition, IFeatureElement>().ConvertUsing(
                sd =>
                {
                    var scenario = sd as G.Scenario;
                    if (scenario != null)
                    {
                        return AutoMapper.Mapper.Map<Scenario>(scenario);
                    }

                    var scenarioOutline = sd as G.ScenarioOutline;
                    if (scenarioOutline != null)
                    {
                        return AutoMapper.Mapper.Map<ScenarioOutline>(scenarioOutline);
                    }

                    throw new ArgumentException("Only arguments of type Scenario and ScenarioOutline are supported.");
                });

            AutoMapper.Mapper.CreateMap<G.Feature, Feature>()
                .ForMember(t => t.FeatureElements, opt => opt.ResolveUsing(s => s.ScenarioDefinitions));
        }

        public Mapper(string featureLanguage = "en")
        {
            this.featureLanguage = featureLanguage;
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
            var dialectProvider = new Gherkin3.GherkinDialectProvider(this.featureLanguage);

            if (dialectProvider.GetDialect(this.featureLanguage, null).WhenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.When;
            }
            if (dialectProvider.GetDialect(this.featureLanguage, null).GivenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.Given;
            }
            if (dialectProvider.GetDialect(this.featureLanguage, null).ThenStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.Then;
            }
            if (dialectProvider.GetDialect(this.featureLanguage, null).AndStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.And;
            }
            if (dialectProvider.GetDialect(this.featureLanguage, null).ButStepKeywords.Select(s => s.Trim()).Contains(keyword))
            {
                return Keyword.But;
            }

            return AutoMapper.Mapper.Map<Keyword>(keyword);
        }

        public string MapToString(G.Tag tag)
        {
            return AutoMapper.Mapper.Map<string>(tag);
        }

        public Scenario MapToScenario(G.Scenario scenario)
        {
            return AutoMapper.Mapper.Map<Scenario>(scenario);
        }

        public Example MapToExample(G.Examples examples)
        {
            return AutoMapper.Mapper.Map<Example>(examples);
        }

        public ScenarioOutline MapToScenarioOutline(G.ScenarioOutline scenarioOutline)
        {
            return AutoMapper.Mapper.Map<ScenarioOutline>(scenarioOutline);
        }

        public Scenario MapToScenario(G.Background background)
        {
            return AutoMapper.Mapper.Map<Scenario>(background);
        }

        public Feature MapToFeature(G.Feature feature)
        {
            return AutoMapper.Mapper.Map<Feature>(feature);
        }
    }
}
