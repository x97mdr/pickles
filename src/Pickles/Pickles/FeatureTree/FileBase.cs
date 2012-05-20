using System;
using System.IO;
using Pickles.Extensions;

namespace Pickles.FeatureTree
{
    public abstract class FileBase : ITreeItem
    {
        private readonly Folder mFolder;
        private readonly string mName;

        protected FileBase(string fileName, Folder folder)
        {
            if (fileName.IsNullOrWhiteSpace()) throw new ArgumentNullException("fileName");

            string name = Path.GetFileNameWithoutExtension(fileName);

            if (name.IsNullOrWhiteSpace())
                throw new ArgumentException("Filename must consist of more than only the extension.", "fileName");

            mName = name;

            if (folder == null) throw new ArgumentNullException("folder");

            mFolder = folder;
        }

        public Folder Folder
        {
            get { return mFolder; }
        }

        #region ITreeItem Members

        public string Name
        {
            get { return mName; }
        }

        public ITreeItem Parent
        {
            get { return Folder; }
        }

        public ITreeItem FindCommonAncestor(ITreeItem other)
        {
            if (other == null) throw new ArgumentNullException("other");

            return Folder;
        }

        public string GetRelativePathFromHereToThere(ITreeItem there)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}