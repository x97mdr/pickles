using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Pickles
{
    [DebuggerDisplay("Name = {Name}")]
    public class FeatureNode
    {
        public FileSystemInfo Location { get; set; }
        public Uri Url { get; set; }
        public string Name { get { return Location.Name; } }
    }
}
