using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pickles.Parser;

namespace Pickles.TestFrameworks
{
    public class ResultsKeyGenerator
    {
        public string GetFeatureKey(string featureName)
        {
            return featureName;
        }

        public string GetScenarioKey(Scenario scenario)
        {
            return GetScenarioKey(scenario.Feature.Name, scenario.Name);
        }

        public string GetScenarioKey(string featureName, string scenarioName)
        {
            return featureName + "+" + scenarioName;
        }

        public string GetScenarioOutlineKey(ScenarioOutline scenarioOutline)
        {
            return GetScenarioOutlineKey(scenarioOutline.Feature.Name, scenarioOutline.Name);
        }

        public string GetScenarioOutlineKey(string featureName, string scenarioOutlineName)
        {
            return featureName + "+" + scenarioOutlineName;
        }
    }
}
