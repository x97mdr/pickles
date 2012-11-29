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
using NGenerics.DataStructures.Trees;
using Autofac;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders;

namespace PicklesDoc.Pickles
{
    public class Runner
    {
        public void Run(IContainer container)
        {
            var configuration = container.Resolve<Configuration>();
            if (!configuration.OutputFolder.Exists) configuration.OutputFolder.Create();

            var featureCrawler = container.Resolve<DirectoryTreeCrawler>();
            GeneralTree<IDirectoryTreeNode> features = featureCrawler.Crawl(configuration.FeatureFolder);

            var documentationBuilder = container.Resolve<IDocumentationBuilder>();
            documentationBuilder.Build(features);
        }
    }
}