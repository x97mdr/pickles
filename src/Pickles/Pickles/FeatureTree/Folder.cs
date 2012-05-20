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
using System.Collections.Generic;
using System.Linq;
using Pickles.Extensions;

namespace Pickles.FeatureTree
{
    public class Folder : ITreeItem
    {
        private readonly string folderName;

        private readonly Folder parentFolder;

        public Folder(string folderName)
            : this(folderName, null)
        {
        }

        public Folder(string folderName, Folder parentFolder)
        {
            if (folderName.IsNullOrWhiteSpace()) throw new ArgumentNullException("folderName");

            this.folderName = folderName;
            this.parentFolder = parentFolder;
        }

        public Folder ParentFolder
        {
            get { return parentFolder; }
        }

        #region ITreeItem Members

        public string Name
        {
            get { return folderName; }
        }

        public ITreeItem Parent
        {
            get { return ParentFolder; }
        }

        public ITreeItem FindCommonAncestor(ITreeItem other)
        {
            if (other == null) throw new ArgumentNullException("other");

            if (Parent == other)
            {
                return Parent;
            }
            else
            {
                List<ITreeItem> myHierarchy = TreeItemHelper.CreateHierarchy(this);

                List<ITreeItem> othersHierarchy = TreeItemHelper.CreateHierarchy(other);

                IEnumerable<ITreeItem> intersection = myHierarchy.Intersect(othersHierarchy);

                return intersection.FirstOrDefault();
            }
        }

        public string GetRelativePathFromHereToThere(ITreeItem there)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}