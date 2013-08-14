using System;
using System.Collections.Generic;

namespace PicklesDoc.Pickles.Parser.SpecRun
{
    internal class Feature
    {
        public string Title { get; set; }

        public List<Scenario> Scenarios { get; set; }
    }
}
