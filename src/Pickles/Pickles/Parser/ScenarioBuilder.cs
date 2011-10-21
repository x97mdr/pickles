using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    class ScenarioBuilder
    {
        private string name;
        private string description;
        private List<Step> steps;
        private List<string> tags;

        public ScenarioBuilder()
        {
            this.steps = new List<Step>();
            this.tags = new List<string>();
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

        public Scenario GetResult()
        {
            return new Scenario
            {
                Name = this.name,
                Description = this.description,
                Steps = new List<Step>(this.steps),
                Tags = new List<string>(this.tags)
            };
        }
    }
}
