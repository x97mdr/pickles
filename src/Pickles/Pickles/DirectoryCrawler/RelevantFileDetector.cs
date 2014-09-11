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
using System.IO.Abstractions;

namespace PicklesDoc.Pickles.DirectoryCrawler
{
    public class RelevantFileDetector
    {
        private readonly ImageFileDetector imageFileDetector;

        public RelevantFileDetector()
        {
            this.imageFileDetector = new ImageFileDetector();
        }

        public bool IsFeatureFile(FileInfoBase file)
        {
            return file.Extension.Equals(".feature", StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsMarkdownFile(FileInfoBase file)
        {
            if (file.Name.EndsWith("csproj.FileListAbsolute.txt"))
            {
              return false;
            }

            switch (file.Extension.ToLowerInvariant())
            {
                case ".markdown":
                case ".mdown":
                case ".mkdn":
                case ".md":
                case ".mdwn":
                case ".mdtxt":
                case ".mdtext":
                case ".text":
                case ".txt":
                    return true;
            }

            return false;
        }

        public bool IsRelevant(FileInfoBase file)
        {
            return this.IsFeatureFile(file) || this.IsMarkdownFile(file) || this.imageFileDetector.IsRelevant(file);
        }

        public bool IsImageFile(FileInfoBase file)
        {
            return this.imageFileDetector.IsRelevant(file);
        }
    }
}