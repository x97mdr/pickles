using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Pickles.Extensions;

namespace Pickles
{
    public class MarkdownTreeNode : IDirectoryTreeNode
    {
        public XElement MarkdownContent
        {
            get;
            private set;
        }

        public MarkdownTreeNode(FileSystemInfo location, string relativePathFromRoot, XElement markdownContent)
        {
            this.OriginalLocation = location;
            this.OriginalLocationUrl = new Uri(location.FullName);
            this.RelativePathFromRoot = relativePathFromRoot;
            this.MarkdownContent = markdownContent;
        }

        #region IItemNode Members

        public string GetRelativeUriTo(Uri other, string newExtension)
        {
            return this.OriginalLocation.FullName != other.LocalPath ? other.MakeRelativeUri(this.OriginalLocationUrl).ToString().Replace(this.OriginalLocation.Extension, newExtension) : "#";
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
                var headerElement = this.MarkdownContent.Descendants().FirstOrDefault(element => element.Name.LocalName == "h1");
                return headerElement != null ? headerElement.Value : OriginalLocation.Name.Replace(OriginalLocation.Extension, string.Empty).ExpandWikiWord();
            }
        }

        public System.IO.FileSystemInfo OriginalLocation
        {
            get;
            private set;
        }

        public Uri OriginalLocationUrl
        {
            get;
            private set;
        }

        public string RelativePathFromRoot
        {
            get;
            private set;
        }

        #endregion
    }
}
