using System;
using System.IO;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.FeatureTree
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

            this.mName = name;

            if (folder == null) throw new ArgumentNullException("folder");

            this.mFolder = folder;
        }

        public Folder Folder
        {
            get { return this.mFolder; }
        }

        #region ITreeItem Members

        public string Name
        {
            get { return this.mName; }
        }

        public ITreeItem Parent
        {
            get { return this.Folder; }
        }

        public ITreeItem FindCommonAncestor(ITreeItem other)
        {
            if (other == null) throw new ArgumentNullException("other");

            return this.Folder;
        }

        public string GetRelativePathFromHereToThere(ITreeItem there)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}