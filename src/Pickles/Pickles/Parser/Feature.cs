using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public class Feature
    {
        private readonly List<Scenario> scenarios;
        private readonly List<ScenarioOutline> scenarioOutlines;

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Scenario> Scenarios { get { return this.scenarios; } }
        public IEnumerable<ScenarioOutline> ScenarioOutlines { get { return this.scenarioOutlines; } }

        public Feature()
	    {
            this.scenarios = new List<Scenario>();
            this.scenarioOutlines = new List<ScenarioOutline>();
	    }
    }
}
