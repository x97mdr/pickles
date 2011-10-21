using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles.Parser
{
    public class Step
    {
        public Keyword Keyword { get; set; }
        public string Name { get; set; }
        public Table TableArgument { get; set; }
        public string DocStringArgument { get; set; }
    }
}
