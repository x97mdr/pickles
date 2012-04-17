using System;
using Pickles.Extensions;

namespace Pickles.FeatureTree
{
    public abstract class FileBase : ITreeItem
    {
        private readonly string mName;

        private readonly Folder mFolder;

        protected FileBase(string fileName, Folder folder)
        {
            if (fileName.IsNullOrWhiteSpace()) throw new ArgumentNullException("fileName");

            string name = System.IO.Path.GetFileNameWithoutExtension(fileName);

            if (name.IsNullOrWhiteSpace()) throw new ArgumentException("Filename must consist of more than only the extension.", "fileName");

            this.mName = name;

            if (folder == null) throw new ArgumentNullException("folder");

            this.mFolder = folder;
        }

        public string Name
        {
            get { return this.mName; }
        }

        public Folder Folder
        {
            get { return mFolder; }
        }

        public ITreeItem FindCommonAncestor(ITreeItem other)
        {
          throw new NotImplementedException();
        }

        public string GetRelativePathFromHereToThere(ITreeItem there)
        {
          throw new NotImplementedException();
        }
    }
}