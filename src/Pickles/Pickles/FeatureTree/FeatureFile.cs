using System;
using Pickles.Parser;

namespace Pickles.FeatureTree
{
    public class FeatureFile : FileBase
    {
        private readonly Feature mContent;

        public FeatureFile(string fileName, Folder folder, Feature feature)
            : base(fileName, folder)
        {
            if (feature == null) throw new ArgumentNullException("feature");

            mContent = feature;
        }

        public Feature Content
        {
            get { return mContent; }
        }
    }
}