using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using G = Gherkin3.Ast;

namespace PicklesDoc.Pickles.ObjectModel
{
    public class Mapper
    {
        private readonly string featureLanguage;
        private MappingEngine mapper;

        static Mapper()
        {
        }

        public Mapper(string featureLanguage = "en")
        {
            this.featureLanguage = featureLanguage;

            var configurationStore = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);

            this.mapper = new MappingEngine(configurationStore);

            configurationStore.CreateMap<string, Keyword>().ConvertUsing(new KeywordResolver(this.featureLanguage));

            configurationStore.CreateMap<G.TableCell, string>()
        .ConstructUsing(cell => cell.Value);

            configurationStore.CreateMap<G.TableRow, TableRow>()
                .ConstructUsing(row => new TableRow(row.Cells.Select(this.mapper.Map<string>)));

            configurationStore.CreateMap<G.DataTable, Table>()
                .ForMember(t => t.HeaderRow, opt => opt.MapFrom(dt => dt.Rows.Take(1).Single()))
                .ForMember(t => t.DataRows, opt => opt.MapFrom(dt => dt.Rows.Skip(1)));

            configurationStore.CreateMap<G.DocString, string>().ConstructUsing(docString => docString.Content);

            configurationStore.CreateMap<G.Step, Step>()
                .ForMember(t => t.NativeKeyword, opt => opt.MapFrom(s => s.Keyword))
                .ForMember(t => t.Name, opt => opt.MapFrom(s => s.Text))
                .ForMember(t => t.DocStringArgument, opt => opt.MapFrom(s => s.Argument is G.DocString ? s.Argument : null))
                .ForMember(t => t.TableArgument, opt => opt.MapFrom(s => s.Argument is G.DataTable ? s.Argument : null));

            configurationStore.CreateMap<G.Tag, string>()
                .ConstructUsing(tag => tag.Name);

            configurationStore.CreateMap<G.Scenario, Scenario>()
                .ForMember(t => t.Description, opt => opt.NullSubstitute(string.Empty));

            configurationStore.CreateMap<IEnumerable<G.TableRow>, Table>()
                .ForMember(t => t.HeaderRow, opt => opt.MapFrom(s => s.Take(1).Single()))
                .ForMember(t => t.DataRows, opt => opt.MapFrom(s => s.Skip(1)));

            configurationStore.CreateMap<G.Examples, Example>()
                .ForMember(t => t.TableArgument, opt => opt.MapFrom(s => ((G.IHasRows)s).Rows));

            configurationStore.CreateMap<G.ScenarioOutline, ScenarioOutline>()
                .ForMember(t => t.Description, opt => opt.NullSubstitute(string.Empty));

            configurationStore.CreateMap<G.Background, Scenario>()
                .ForMember(t => t.Description, opt => opt.NullSubstitute(string.Empty));

            configurationStore.CreateMap<G.ScenarioDefinition, IFeatureElement>().ConvertUsing(
                sd =>
                {
                    var scenario = sd as G.Scenario;
                    if (scenario != null)
                    {
                        return this.mapper.Map<Scenario>(scenario);
                    }

                    var scenarioOutline = sd as G.ScenarioOutline;
                    if (scenarioOutline != null)
                    {
                        return this.mapper.Map<ScenarioOutline>(scenarioOutline);
                    }

                    throw new ArgumentException("Only arguments of type Scenario and ScenarioOutline are supported.");
                });

            configurationStore.CreateMap<G.Feature, Feature>()
                .ForMember(t => t.FeatureElements, opt => opt.ResolveUsing(s => s.ScenarioDefinitions));
        }

        public string MapToString(G.TableCell cell)
        {
            return this.mapper.Map<string>(cell);
        }

        public TableRow MapToTableRow(G.TableRow tableRow)
        {
            return this.mapper.Map<TableRow>(tableRow);
        }

        public Table MapToTable(G.DataTable dataTable)
        {
            return this.mapper.Map<Table>(dataTable);
        }

        public string MapToString(G.DocString docString)
        {
            return this.mapper.Map<string>(docString);
        }

        public Step MapToStep(G.Step step)
        {
            return this.mapper.Map<Step>(step);
        }

        public Keyword MapToKeyword(string keyword)
        {
            return this.mapper.Map<Keyword>(keyword);
        }

        public string MapToString(G.Tag tag)
        {
            return this.mapper.Map<string>(tag);
        }

        public Scenario MapToScenario(G.Scenario scenario)
        {
            return this.mapper.Map<Scenario>(scenario);
        }

        public Example MapToExample(G.Examples examples)
        {
            return this.mapper.Map<Example>(examples);
        }

        public ScenarioOutline MapToScenarioOutline(G.ScenarioOutline scenarioOutline)
        {
            return this.mapper.Map<ScenarioOutline>(scenarioOutline);
        }

        public Scenario MapToScenario(G.Background background)
        {
            return this.mapper.Map<Scenario>(background);
        }

        public Feature MapToFeature(G.Feature feature)
        {
            return this.mapper.Map<Feature>(feature);
        }
    }
}
