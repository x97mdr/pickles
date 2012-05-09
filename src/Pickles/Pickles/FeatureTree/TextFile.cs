﻿#region License

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

namespace Pickles.FeatureTree
{
    public class TextFile : FileBase
    {
        private readonly string mContent;

        public TextFile(string fileName, Folder folder, string content)
            : base(fileName, folder)
        {
            if (content == null) throw new ArgumentNullException("content");

            this.mContent = content;
        }

        public string Content
        {
            get { return mContent; }
        }
    }
}
