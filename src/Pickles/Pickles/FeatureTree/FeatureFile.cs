using System;
using System.IO.Abstractions;
using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.FeatureTree
{
    public class FeatureFile : FileBase
    {
        private readonly Feature mContent;

        public FeatureFile(string fileName, Folder folder, Feature feature, IFileSystem fileSystem)
            : base(fileName, folder, fileSystem)
        {
            if (feature == null) throw new ArgumentNullException("feature");

            this.mContent = feature;
        }

        public Feature Content
        {
            get { return this.mContent; }
        }
    }
}