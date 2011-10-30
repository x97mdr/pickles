#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

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
        public FileSystemInfo Location 
        { 
            get; 
            set; 
        }

        public Uri Url 
        { 
            get; 
            set; 
        }

        public string RelativePathFromRoot 
        { 
            get; 
            set; 
        }

        public string Name
        {
            get
            {
                if (IsDirectory) return Location.Name;
                return Location.Name.Replace(Location.Extension, string.Empty);
            }
        }

        public Feature Feature 
        { 
            get; 
            set; 
        }

        public bool IsDirectory 
        { 
            get 
            { 
                return Location is DirectoryInfo; 
            } 
        }

        public bool IsContent
        {
            get
            {
                return !IsDirectory;
            }
        }

        public bool IsEmpty 
        { 
            get 
            { 
                return IsDirectory ? !((Location as DirectoryInfo).GetFileSystemInfos().Any()) : true; 
            } 
        }

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

        public string GetRelativeUriTo(Uri other, string newExtension)
        {
            return this.Location.FullName != other.LocalPath ? other.MakeRelativeUri(this.Url).ToString().Replace(this.Location.Extension, newExtension) : "#";
        }

        public string GetRelativeUriTo(Uri other)
        {
            return GetRelativeUriTo(other, ".xhtml");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
