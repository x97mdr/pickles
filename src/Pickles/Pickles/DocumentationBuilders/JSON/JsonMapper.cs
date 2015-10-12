//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="JsonMapper.cs" company="PicklesDoc">
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
using AutoMapper;
using AutoMapper.Mappers;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.DocumentationBuilders.JSON
{
    public class JsonMapper
    {
        private readonly MappingEngine mapper;

        public JsonMapper()
        {
            var configurationStore = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.Mappers);

            this.mapper = new MappingEngine(configurationStore);

            configurationStore.CreateMap<Feature, JsonFeature>();
            configurationStore.CreateMap<Example, JsonExample>();
            configurationStore.CreateMap<Keyword, JsonKeyword>();
            configurationStore.CreateMap<Scenario, JsonScenario>();
            configurationStore.CreateMap<ScenarioOutline, JsonScenarioOutline>();
            configurationStore.CreateMap<Step, JsonStep>();
            configurationStore.CreateMap<Table, JsonTable>();
            configurationStore.CreateMap<TestResult, JsonTestResult>();

            configurationStore.CreateMap<TableRow, JsonTableRow>()
                .ConstructUsing(row => new JsonTableRow(row.Cells.ToArray()));

            configurationStore.CreateMap<IFeatureElement, IJsonFeatureElement>().ConvertUsing(
                sd =>
                {
                    var scenario = sd as Scenario;
                    if (scenario != null)
                    {
                        return this.mapper.Map<JsonScenario>(scenario);
                    }

                    var scenarioOutline = sd as ScenarioOutline;
                    if (scenarioOutline != null)
                    {
                        return this.mapper.Map<JsonScenarioOutline>(scenarioOutline);
                    }

                    throw new ArgumentException("Only arguments of type Scenario and ScenarioOutline are supported.");
                });
        }

        public JsonTableRow Map(TableRow tableRow)
        {
            return this.mapper.Map<JsonTableRow>(tableRow);
        }

        public JsonFeature Map(Feature feature)
        {
            return this.mapper.Map<JsonFeature>(feature);
        }

        public JsonTestResult Map(TestResult testResult)
        {
            return this.mapper.Map<JsonTestResult>(testResult);
        }
    }
}
