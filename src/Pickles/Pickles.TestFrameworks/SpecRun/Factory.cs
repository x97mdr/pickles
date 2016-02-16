//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Factory.cs" company="PicklesDoc">
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
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PicklesDoc.Pickles.TestFrameworks.SpecRun
{
    internal static class Factory
    {
        /*
                <features>
                    <feature>
                        <title>Addition</title>
                        <scenarios>
                            <scenario>
                                <title>Adding several numbers</title>
                                <result>Passed|Pending|Failed|Ignored</result>
                            </scenario>
                        </scenarios>
                    </feature>
                </features>
           */

        internal static SpecRunFeature ToSpecRunFeature(XElement featureXml)
        {
            var title = featureXml.Element("title");
            var scenarios = featureXml.Element("scenarios");

            return new SpecRunFeature
            {
                Title = title != null ? title.Value : string.Empty,
                Scenarios = scenarios != null ? scenarios.Elements("scenario").Select<XElement, SpecRunScenario>(ToSpecRunScenario).ToList() : new List<SpecRunScenario>()
            };
        }

        internal static SpecRunScenario ToSpecRunScenario(XElement scenarioXml)
        {
            var title = scenarioXml.Element("title");
            var result = scenarioXml.Element("result");

            return new SpecRunScenario
            {
                Title = title != null ? title.Value : string.Empty,
                Result = result != null ? result.Value : string.Empty
            };
        }
    }
}
