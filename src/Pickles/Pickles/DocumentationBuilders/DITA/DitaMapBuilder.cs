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
using System.Xml.Linq;
using NGenerics.DataStructures.Trees;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.DocumentationBuilders.DITA
{
    public class DitaMapBuilder
    {
        private readonly Configuration configuration;
        private readonly DitaMapPathGenerator ditaMapPathGenerator;

        private readonly IFileSystem fileSystem;

        public DitaMapBuilder(Configuration configuration, DitaMapPathGenerator ditaMapPathGenerator, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.ditaMapPathGenerator = ditaMapPathGenerator;
            this.fileSystem = fileSystem;
        }

        private XElement BuildListItems(GeneralTree<INode> features)
        {
            XElement container;
            if (features.Data.NodeType == NodeType.Content)
            {
                container = new XElement("topicref",
                                         new XAttribute("href",
                                                        this.ditaMapPathGenerator.GeneratePathToFeature(features.Data)));
            }
            else if (features.Data.NodeType == NodeType.Structure)
            {
                container = new XElement("topichead", new XAttribute("navtitle", features.Data.Name));
            }
            else
            {
                return null;
            }

            foreach (var childNode in features.ChildNodes)
            {
                if (features.Data.NodeType == NodeType.Content)
                {
                    container.Add(new XElement("topicref",
                                               new XAttribute("href",
                                                              this.ditaMapPathGenerator.GeneratePathToFeature(childNode.Data))));
                }
                else if (features.Data.NodeType == NodeType.Structure)
                {
                    container.Add(this.BuildListItems(childNode));
                }
            }

            return container;
        }

        public void Build(GeneralTree<INode> features)
        {
            var map = new XElement("map", new XAttribute("title", "Features"), new XAttribute("id", "features"),
                                   this.BuildListItems(features));
            var document = new XDocument(
                new XDocumentType("map", "-//OASIS//DTD DITA Map//EN", "map.dtd", string.Empty), map);
            document.Save(this.fileSystem.Path.Combine(this.configuration.OutputFolder.FullName, "features.ditamap"));
        }
    }
}