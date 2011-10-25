using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Pickles.Parser;

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
        public bool IsEmpty { get { return IsDirectory ? !((Location as DirectoryInfo).GetFileSystemInfos().Any()) : true; } }

        public FeatureNodeType Type
        {
            get
            {
                if (IsDirectory) return FeatureNodeType.Directory;

                var file = Location as FileInfo;
                if (file.Extension == ".feature") return FeatureNodeType.Feature;
                else if (file.Extension == ".md") return FeatureNodeType.Markdown;
                else return FeatureNodeType.Unknown;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
