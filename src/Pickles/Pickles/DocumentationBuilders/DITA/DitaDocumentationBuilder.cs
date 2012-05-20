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

using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using Pickles.DirectoryCrawler;

namespace Pickles.DocumentationBuilders.DITA
{
    public class DitaDocumentationBuilder : IDocumentationBuilder
    {
        private readonly DitaFeatureFormatter ditaFeatureFormatter;
        private readonly DitaMapBuilder ditaMapBuilder;

        public DitaDocumentationBuilder(DitaFeatureFormatter ditaFeatureFormatter, DitaMapBuilder ditaMapBuilder)
        {
            this.ditaFeatureFormatter = ditaFeatureFormatter;
            this.ditaMapBuilder = ditaMapBuilder;
        }

        #region IDocumentationBuilder Members

        public void Build(GeneralTree<IDirectoryTreeNode> features)
        {
            ditaMapBuilder.Build(features);

            var actionVisitor = new ActionVisitor<IDirectoryTreeNode>(node =>
                                                                          {
                                                                              var featureDirectoryTreeNode =
                                                                                  node as FeatureDirectoryTreeNode;
                                                                              if (featureDirectoryTreeNode != null)
                                                                              {
                                                                                  ditaFeatureFormatter.Format(
                                                                                      featureDirectoryTreeNode);
                                                                              }
                                                                          });

            features.AcceptVisitor(actionVisitor);
        }

        #endregion
    }
}