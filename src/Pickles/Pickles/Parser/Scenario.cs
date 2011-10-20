using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public class Scenario
    {
        private readonly List<Step> steps;

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Step> Steps { get { return this.steps; } }

        public Scenario()
        {
            this.steps = new List<Step>();
        }

        public void AddStep(Step step)
        {
            this.steps.Add(step);
        }
    }
}
