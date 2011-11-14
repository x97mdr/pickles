using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pickles.Parser;
using System.IO;

namespace Pickles
{
    public class FeatureDirectoryTreeNode : IDirectoryTreeNode
    {
        public Feature Feature
        {
            get;
            private set;
        }

        public FeatureDirectoryTreeNode(FileSystemInfo location, string relativePathFromRoot, Feature feature)
        {
            this.OriginalLocation = location;
            this.OriginalLocationUrl = new Uri(location.FullName);
            this.RelativePathFromRoot = relativePathFromRoot;
            this.Feature = feature;
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
            get { return Feature.Name; }
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
