using System;
using Pickles.Parser;

namespace Pickles.FeatureTree
{
    public class FeatureFile : FileBase, ITreeItem
    {
        private readonly Feature mContent;

        public FeatureFile(string fileName, Folder folder, Feature feature)
            : base(fileName, folder)
        {
            if (feature == null) throw new ArgumentNullException("feature");

            this.mContent = feature;
        }

        public Feature Content
        {
            get { return mContent; }
        }

        public ITreeItem FindCommonAncestor()
        {
            throw new NotImplementedException();
        }

        public string GetRelativePathFromHereToThere(ITreeItem there)
        {
            throw new NotImplementedException();
        }
    }
}