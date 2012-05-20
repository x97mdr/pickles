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
using System.IO;
using Pickles.Extensions;

namespace Pickles.DirectoryCrawler
{
    public class FolderDirectoryTreeNode : IDirectoryTreeNode
    {
        public FolderDirectoryTreeNode(FileSystemInfo location, string relativePathFromRoot)
        {
            OriginalLocation = location;
            OriginalLocationUrl = location.ToUri();
            RelativePathFromRoot = relativePathFromRoot;
        }

        #region IDirectoryTreeNode Members

        public string GetRelativeUriTo(Uri other, string newExtension)
        {
            bool areSameLocation = OriginalLocation.FullName == other.LocalPath;

            if (areSameLocation)
            {
                return "#";
            }

            string result = other.MakeRelativeUri(OriginalLocationUrl).ToString();

            string oldExtension = OriginalLocation.Extension;

            if (!string.IsNullOrEmpty(oldExtension))
            {
                result = result.Replace(oldExtension, newExtension);
            }

            return result;
        }

        public string GetRelativeUriTo(Uri other)
        {
            return GetRelativeUriTo(other, ".html");
        }

        public bool IsContent
        {
            get { return false; }
        }

        public string Name
        {
            get { return OriginalLocation.Name.ExpandWikiWord(); }
        }

        public FileSystemInfo OriginalLocation { get; private set; }

        public Uri OriginalLocationUrl { get; private set; }

        public string RelativePathFromRoot { get; private set; }

        #endregion
    }
}