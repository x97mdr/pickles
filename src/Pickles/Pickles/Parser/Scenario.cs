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
        public List<Step> Steps { get; set; }
        public List<string> Tags { get; set; }
    }
}
