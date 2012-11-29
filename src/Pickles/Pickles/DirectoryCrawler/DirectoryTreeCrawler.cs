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
using System.IO;
using System.Linq;
using NGenerics.DataStructures.Trees;

namespace PicklesDoc.Pickles.DirectoryCrawler
{
    public class DirectoryTreeCrawler
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            return this.Crawl(new DirectoryInfo(directory), null);
        }

        public GeneralTree<IDirectoryTreeNode> Crawl(DirectoryInfo directory)
        {
            return this.Crawl(directory, null);
        }

        private GeneralTree<IDirectoryTreeNode> Crawl(DirectoryInfo directory, IDirectoryTreeNode rootNode)
        {
            IDirectoryTreeNode currentNode =
                this.featureNodeFactory.Create(rootNode != null ? rootNode.OriginalLocation : null, directory);

            if (rootNode == null)
            {
                rootNode = currentNode;
            }

            var tree = new GeneralTree<IDirectoryTreeNode>(currentNode);

            bool isRelevantFileFound = false;
            foreach (FileInfo file in directory.GetFiles().Where(file => this.relevantFileDetector.IsRelevant(file)))
            {
                isRelevantFileFound = true;

                IDirectoryTreeNode node = null;
                try
                {
                    node = this.featureNodeFactory.Create(rootNode.OriginalLocation, file);
                }
                catch (Exception)
                {     
                    if (log.IsWarnEnabled) log.WarnFormat("The file, {0}, will be ignored because it could not be read in properly", file.FullName);
                }

                if (node != null) tree.Add(node);
            }

            bool isRelevantDirectoryFound = false;
            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                GeneralTree<IDirectoryTreeNode> subTree = this.Crawl(subDirectory, rootNode);
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