using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public class Feature
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Scenario> Scenarios { get; private set; }
        public Scenario Background { get; set; }
        public List<ScenarioOutline> ScenarioOutlines { get; private set; }
        public List<string> Tags { get; set; }

        public Feature()
	    {
            this.Scenarios = new List<Scenario>();
            this.ScenarioOutlines = new List<ScenarioOutline>();
            this.Tags = new List<string>();
	    }

        public void AddTag(string tag)
        {
            this.Tags.Add(tag);
        }

        public void AddScenario(Scenario scenario)
        {
            this.Scenarios.Add(scenario);
        }

        public void AddScenarioOutline(ScenarioOutline scenarioOutline)
        {
            this.ScenarioOutlines.Add(scenarioOutline);
        }
    }
}
