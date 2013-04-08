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
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.DITA
{
    public class DitaMapPathGenerator
    {
        private readonly Configuration configuration;

        private readonly IFileSystem fileSystem;

        public DitaMapPathGenerator(Configuration configuration, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
        }

        public Uri GeneratePathToFeature(INode node)
        {
            var fileInfo = node.OriginalLocation as FileInfoBase;
            if (fileInfo != null)
            {
                string nodeFilename =
                    node.OriginalLocation.Name.Replace(node.OriginalLocation.Extension,
                                                                    string.Empty);
                string nodeDitaName = nodeFilename.ToDitaName() + ".dita";
                Uri newUri = fileInfo.Directory.ToFileUriCombined(nodeDitaName, this.fileSystem);

                return this.configuration.FeatureFolder.ToUri().MakeRelativeUri(newUri);
            }

            throw new InvalidOperationException("Cannot Generate Path to a file that is not a feature");
        }
    }
}