#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Pickles.Extensions;
using Pickles.Parser;
using System.IO;

namespace Pickles.DocumentationBuilders.DITA
{
    public class DitaFeatureFormatter
    {
        private readonly Configuration configuration;
        private readonly DitaScenarioFormatter ditaScenarioFormatter;
        private readonly DitaScenarioOutlineFormatter ditaScenarioOutlineFormatter;

        public DitaFeatureFormatter(Configuration configuration, DitaScenarioFormatter ditaScenarioFormatter, DitaScenarioOutlineFormatter ditaScenarioOutlineFormatter)
        {
            this.configuration = configuration;
            this.ditaScenarioFormatter = ditaScenarioFormatter;
            this.ditaScenarioOutlineFormatter = ditaScenarioOutlineFormatter;
        }

        public void Format(Feature feature)
        {
            var topic = new XElement("topic", new XAttribute("id", feature.Name.ToDitaName()));
            topic.Add(new XElement("title", feature.Name));
            topic.Add(new XElement("shortdesc", feature.Description));

            var body = new XElement("body");
            topic.Add(body);

            foreach (var featureElement in feature.FeatureElements)
            {
                var scenario = featureElement as Scenario;
                if (scenario != null)
                {
                    this.ditaScenarioFormatter.Format(body, scenario);
                }

                var scenarioOutline = featureElement as ScenarioOutline;
                if (scenarioOutline != null)
                {
                    this.ditaScenarioOutlineFormatter.Format(body, scenarioOutline);
                }
            }

            var filename = Path.Combine(this.configuration.OutputFolder.FullName, feature.Name.ToDitaName() + ".dita");
            var document = new XDocument(topic);
            document.Save(filename);
        }
    }
}
