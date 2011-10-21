using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    class ScenarioOutlineBuilder
    {
        private string name;
        private string description;
        private List<Step> steps;
        private List<string> tags;
        private string exampleName;
        private string exampleDescription;
        private TableBuilder tableBuilder;

        public ScenarioOutlineBuilder(TableBuilder tableBuilder)
        {
            this.steps = new List<Step>();
            this.tags = new List<string>();
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
            this.steps.Add(step);
        }

        public void AddTag(string tag)
        {
            this.tags.Add(tag);
        }

        public void SetExampleName(string name)
        {
            this.exampleName = name;
        }

        public void SetExampleDescription(string description)
        {
            this.exampleDescription = description;
        }

        public void SetExampleTableRow(IEnumerable<string> cells)
        {
            this.tableBuilder.AddRow(cells);
        }

        public ScenarioOutline GetResult()
        {
            return new ScenarioOutline
            {
                Name = this.name,
                Description = this.description,
                Steps = new List<Step>(this.steps),
                Example = new Example { Name = this.exampleName, TableArgument = this.tableBuilder.GetResult() },
                Tags = new List<string>(this.tags)
            };
        }
    }
}