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
using System.Xml.Linq;
using NGenerics.Patterns.Visitor;
using Pickles.DirectoryCrawler;
using NGenerics.DataStructures.Trees;
using System.IO;

namespace Pickles.DocumentationBuilders.DITA
{
    public class DitaMapBuilder
    {
        private readonly Configuration configuration;
        private readonly DitaMapPathGenerator ditaMapPathGenerator;

        public DitaMapBuilder(Configuration configuration, DitaMapPathGenerator ditaMapPathGenerator)
        {
            this.configuration = configuration;
            this.ditaMapPathGenerator = ditaMapPathGenerator;
        }

        private XElement BuildListItems(GeneralTree<IDirectoryTreeNode> features)
        {
            XElement container;
            if (features.Data.IsContent)
            {
                container = new XElement("topicref", new XAttribute("href", this.ditaMapPathGenerator.GeneratePathToFeature(features.Data)));
            }
            else
            {
                container = new XElement("topichead", new XAttribute("navtitle", features.Data.Name));
            }

            foreach (var childNode in features.ChildNodes)
            {
                if (childNode.Data.IsContent)
                {
                    container.Add(new XElement("topicref", new XAttribute("href", this.ditaMapPathGenerator.GeneratePathToFeature(childNode.Data))));
                }
                else
                {
                    container.Add(BuildListItems(childNode));
                }
            }

            return container;
        }

        public void Build(GeneralTree<DirectoryCrawler.IDirectoryTreeNode> features)
        {
            XElement map = new XElement("map", BuildListItems(features));
            XDocument document = new XDocument(map);
            document.Save(Path.Combine(this.configuration.OutputFolder.FullName, "features.ditamap"));
        }
    }
}
