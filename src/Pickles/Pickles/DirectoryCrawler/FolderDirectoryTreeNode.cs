using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pickles.Extensions;

namespace Pickles
{
    public class FolderDirectoryTreeNode : IDirectoryTreeNode
    {
        public FolderDirectoryTreeNode(FileSystemInfo location, string relativePathFromRoot)
        {
            this.OriginalLocation = location;
            this.OriginalLocationUrl = new Uri(location.FullName);
            this.RelativePathFromRoot = relativePathFromRoot;
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
            get { return false; }
        }

        public string Name
        {
            get { return OriginalLocation.Name.ExpandWikiWord(); }
        }

        public FileSystemInfo OriginalLocation
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
