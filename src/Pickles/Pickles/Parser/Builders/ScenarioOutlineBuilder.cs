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

using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Parser.Builders
{
    internal class ScenarioOutlineBuilder
    {
        private readonly List<Step> steps;
        private readonly List<string> tags;
        private string description;
		private List<Example> examples;
        private string name;

        public ScenarioOutlineBuilder(TableBuilder tableBuilder)
        {
            this.steps = new List<Step>();
            this.tags = new List<string>();
			this.examples = new List<Example>();
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
            this.steps.Add(step);
        }

        public void AddTags(List<string> tags)
        {
            this.tags.AddRange(tags);
        }

        public void AddExample(Example example)
		{
			this.examples.Add(example);
		}

        public ScenarioOutline GetResult()
        {
            return new ScenarioOutline
                       {
                           Name = this.name,
                           Description = this.description,
                           Steps = new List<Step>(this.steps),
                           Examples = new List<Example>(this.examples),
                           Tags = new List<string>(this.tags)
                       };
        }
    }
}