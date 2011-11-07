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
using System.IO;
using System.Linq;
using System.Text;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using Pickles.Extensions;

namespace Pickles
{
    public class FeatureCrawler
    {
        private readonly FeatureParser featureParser;
        private readonly RelevantFileDetector relevantFileDetector;

        public FeatureCrawler(FeatureParser featureParser, RelevantFileDetector relevantFileDetector)
        {
            this.featureParser = featureParser;
            this.relevantFileDetector = relevantFileDetector;
        }

        public GeneralTree<FeatureNode> Crawl(string directory)
        {
            return Crawl(new DirectoryInfo(directory), null);
        }

        public GeneralTree<FeatureNode> Crawl(DirectoryInfo directory)
        {
            return Crawl(directory, null);
        }

        private GeneralTree<FeatureNode> Crawl(DirectoryInfo directory, FeatureNode rootNode)
        {
            var currentNode = new FeatureNode
            {
                Location = directory,
                Url = new Uri(directory.FullName),
                RelativePathFromRoot = rootNode == null ? @".\" : PathExtensions.MakeRelativePath(rootNode.Location, directory)
            };

            if (rootNode == null)
            {
                rootNode = currentNode;
            }

            var tree = new GeneralTree<FeatureNode>(currentNode);

            bool isRelevantFileFound = false;
            foreach (var file in directory.GetFiles().Where(file => this.relevantFileDetector.IsRelevant(file)))
            {
                isRelevantFileFound = true;
                var node = new FeatureNode
                {
                    Location = file,
                    Url = new Uri(file.FullName),
                    RelativePathFromRoot = PathExtensions.MakeRelativePath(rootNode.Location, file)
                };

                if (node.Type == FeatureNodeType.Feature) node.Feature = this.featureParser.Parse(file.FullName);
                tree.Add(node);
            }

            bool isRelevantDirectoryFound = false;
            foreach (var subDirectory in directory.GetDirectories())
            {
                var subTree = Crawl(subDirectory, rootNode);
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
