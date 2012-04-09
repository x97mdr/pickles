﻿#region License

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
using Pickles.DirectoryCrawler;
using Pickles.Extensions;
using System.IO;

namespace Pickles.DocumentationBuilders.DITA
{
    public class DitaMapPathGenerator
    {
        private readonly Configuration configuration;

        public DitaMapPathGenerator(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public Uri GeneratePathToFeature(IDirectoryTreeNode directoryTreeNode)
        {
            var fileInfo = directoryTreeNode.OriginalLocation as FileInfo;
            if (fileInfo != null)
            {
                var nodeFilename = directoryTreeNode.OriginalLocation.Name.Replace(directoryTreeNode.OriginalLocation.Extension, string.Empty);
                var nodeDitaName = nodeFilename.ToDitaName() + ".dita";
                var newUri = fileInfo.Directory.ToFileUriCombined(nodeDitaName);
                
                return this.configuration.FeatureFolder.ToUri().MakeRelativeUri(newUri);
            }

            throw new InvalidOperationException("Cannot Generate Path to a file that is not a feature");
        }
    }
}
