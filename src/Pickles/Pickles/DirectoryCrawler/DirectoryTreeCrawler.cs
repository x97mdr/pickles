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

using System.IO;
using System.Linq;
using NGenerics.DataStructures.Trees;

namespace Pickles.DirectoryCrawler
{
    public class DirectoryTreeCrawler
    {
        private readonly FeatureNodeFactory featureNodeFactory;
        private readonly FeatureParser featureParser;
        private readonly RelevantFileDetector relevantFileDetector;

        public DirectoryTreeCrawler(FeatureParser featureParser, RelevantFileDetector relevantFileDetector,
                                    FeatureNodeFactory featureNodeFactory)
        {
            this.featureParser = featureParser;
            this.relevantFileDetector = relevantFileDetector;
            this.featureNodeFactory = featureNodeFactory;
        }

        public GeneralTree<IDirectoryTreeNode> Crawl(string directory)
        {
            return Crawl(new DirectoryInfo(directory), null);
        }

        public GeneralTree<IDirectoryTreeNode> Crawl(DirectoryInfo directory)
        {
            return Crawl(directory, null);
        }

        private GeneralTree<IDirectoryTreeNode> Crawl(DirectoryInfo directory, IDirectoryTreeNode rootNode)
        {
            IDirectoryTreeNode currentNode =
                featureNodeFactory.Create(rootNode != null ? rootNode.OriginalLocation : null, directory);

            if (rootNode == null)
            {
                rootNode = currentNode;
            }

            var tree = new GeneralTree<IDirectoryTreeNode>(currentNode);

            bool isRelevantFileFound = false;
            foreach (FileInfo file in directory.GetFiles().Where(file => relevantFileDetector.IsRelevant(file)))
            {
                isRelevantFileFound = true;
                IDirectoryTreeNode node = featureNodeFactory.Create(rootNode.OriginalLocation, file);
                tree.Add(node);
            }

            bool isRelevantDirectoryFound = false;
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                GeneralTree<IDirectoryTreeNode> subTree = Crawl(subDirectory, rootNode);
                if (subTree != null)
                {
                    isRelevantDirectoryFound = true;
                    tree.Add(subTree);
                }
            }

            if (!isRelevantFileFound && !isRelevantDirectoryFound) return null;

            return tree;
        }
    }
}