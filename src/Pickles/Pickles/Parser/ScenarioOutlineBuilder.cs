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

using System.Collections.Generic;

namespace Pickles.Parser
{
    internal class ScenarioOutlineBuilder
    {
        private readonly List<Step> steps;
        private readonly TableBuilder tableBuilder;
        private readonly List<string> tags;
        private string description;
        private string exampleDescription;
        private string exampleName;
        private string name;

        public ScenarioOutlineBuilder(TableBuilder tableBuilder)
        {
            steps = new List<Step>();
            tags = new List<string>();
            this.tableBuilder = tableBuilder;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetDescription(string description)
        {
            this.description = description;
        }

        public void AddStep(Step step)
        {
            steps.Add(step);
        }

        public void AddTag(string tag)
        {
            tags.Add(tag);
        }

        public void SetExampleName(string name)
        {
            exampleName = name;
        }

        public void SetExampleDescription(string description)
        {
            exampleDescription = description;
        }

        public void SetExampleTableRow(IEnumerable<string> cells)
        {
            tableBuilder.AddRow(cells);
        }

        public ScenarioOutline GetResult()
        {
            return new ScenarioOutline
                       {
                           Name = name,
                           Description = description,
                           Steps = new List<Step>(steps),
                           Example = new Example {Name = exampleName, TableArgument = tableBuilder.GetResult()},
                           Tags = new List<string>(tags)
                       };
        }
    }
}