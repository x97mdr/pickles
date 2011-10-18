using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace Pickles
{
    [DebuggerDisplay("Name = {Name}")]
    public class FeatureNode
    {
        public FileSystemInfo Location { get; set; }
        public string RelativePathFromRoot { get; set; }
        public Uri Url { get; set; }
        public string Name { get { return IsDirectory ? Location.Name : Location.Name.Replace(".feature", string.Empty); } }
        public Feature Feature { get; set; }
        public bool IsDirectory { get { return Location is DirectoryInfo; } }
    }
}
