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

      public string Name
      {
        get { return this.folderName; }
      }

      public Folder ParentFolder
      {
        get { return this.parentFolder; }
      }

      public ITreeItem FindCommonAncestor(ITreeItem other)
      {
        if (other == null) throw new ArgumentNullException("other");

        if (this.ParentFolder == other)
        {
          return this.ParentFolder;
        }
        else
        {
          return this.ParentFolder.FindCommonAncestor(other);
        }
      }

      public string GetRelativePathFromHereToThere(ITreeItem there)
      {
        throw new NotImplementedException();
      }
    }
}
