using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Pickles.Features.Steps
{
    public class Context
    {
        public string FeatureFileContent { get; set; }
        public XDocument ParserOutput { get; set; }
    }
}
