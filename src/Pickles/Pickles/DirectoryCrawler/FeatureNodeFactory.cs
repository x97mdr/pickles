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
using System.Text;
using System.IO;
using Pickles.Extensions;
using Pickles.DocumentationBuilders.HTML;

namespace Pickles.DirectoryCrawler
{
    public class FeatureNodeFactory
    {
        private readonly FeatureParser featureParser;
        private readonly RelevantFileDetector relevantFileDetector;
        private readonly HtmlMarkdownFormatter htmlMarkdownFormatter;

        public FeatureNodeFactory(RelevantFileDetector relevantFileDetector, FeatureParser featureParser, HtmlMarkdownFormatter htmlMarkdownFormatter)
        {
            this.relevantFileDetector = relevantFileDetector;
            this.featureParser = featureParser;
            this.htmlMarkdownFormatter = htmlMarkdownFormatter;
        }

        public IDirectoryTreeNode Create(FileSystemInfo root, FileSystemInfo location)
        {
            var relativePathFromRoot = root == null ? @".\" : PathExtensions.MakeRelativePath(root, location);

            var directory = location as DirectoryInfo;
            if (directory != null)
            {
                return new FolderDirectoryTreeNode(directory, relativePathFromRoot);
            }

            var file = location as FileInfo;
            if (this.relevantFileDetector.IsFeatureFile(file))
            {
                var feature = this.featureParser.Parse(file.FullName);
                return new FeatureDirectoryTreeNode(file, relativePathFromRoot, feature);
            }
            else if (this.relevantFileDetector.IsMarkdownFile(file))
            {
                var markdownContent = this.htmlMarkdownFormatter.Format(File.ReadAllText(file.FullName));
                return new MarkdownTreeNode(file, relativePathFromRoot, markdownContent);
            }

            throw new InvalidOperationException("Cannot create an IItemNode-derived object for " + file.FullName);
        }
    }
}
