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
using System.Linq;
using System.Xml.Linq;
using Pickles.Extensions;

namespace Pickles.DirectoryCrawler
{
    public class MarkdownTreeNode : IDirectoryTreeNode
    {
        public MarkdownTreeNode(FileSystemInfo location, string relativePathFromRoot, XElement markdownContent)
        {
            OriginalLocation = location;
            OriginalLocationUrl = location.ToUri();
            RelativePathFromRoot = relativePathFromRoot;
            MarkdownContent = markdownContent;
        }

        public XElement MarkdownContent { get; private set; }

        #region IDirectoryTreeNode Members

        public string GetRelativeUriTo(Uri other, string newExtension)
        {
            return other.GetUriForTargetRelativeToMe(OriginalLocation, newExtension);
        }

        public string GetRelativeUriTo(Uri other)
        {
            return GetRelativeUriTo(other, ".html");
        }

        public bool IsContent
        {
            get { return true; }
        }

        public string Name
        {
            get
            {
                XElement headerElement =
                    MarkdownContent.Descendants().FirstOrDefault(element => element.Name.LocalName == "h1");
                return headerElement != null
                           ? headerElement.Value
                           : OriginalLocation.Name.Replace(OriginalLocation.Extension, string.Empty).ExpandWikiWord();
            }
        }

        public FileSystemInfo OriginalLocation { get; private set; }

        public Uri OriginalLocationUrl { get; private set; }

        public string RelativePathFromRoot { get; private set; }

        #endregion
    }
}