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
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using Pickles.DirectoryCrawler;

namespace Pickles.DocumentationBuilders.JSON
{
    public class JSONDocumentationBuilder : IDocumentationBuilder
    {

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Configuration configuration;

        public const string JS_FILE_NAME = @"pickledFeatures.json";
        

        public JSONDocumentationBuilder(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void Build(GeneralTree<IDirectoryTreeNode> features)
        {
            if (log.IsInfoEnabled)
            {
                log.InfoFormat("Writing JSON to {0}", this.configuration.OutputFolder.FullName);
            }

            var featuresToFormat = new List<FeatureWithMetaInfo>();

            var actionVisitor = new ActionVisitor<IDirectoryTreeNode>(node =>
            {
                var featureTreeNode = node as FeatureDirectoryTreeNode;
                if (featureTreeNode != null)
                {
                    featuresToFormat.Add(new FeatureWithMetaInfo(featureTreeNode));
                }
            });

            features.AcceptVisitor(actionVisitor);

            CreateFile(OutputFilePath, GenerateJSON(featuresToFormat));
        }

        private string OutputFilePath
        {
            get { return Path.Combine(configuration.OutputFolder.FullName, JS_FILE_NAME); }
        }

        private static string GenerateJSON(List<FeatureWithMetaInfo> features)
        {
            var settings = new JsonSerializerSettings
                               {
                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                   NullValueHandling = NullValueHandling.Ignore,
                                   Converters = new List<JsonConverter> { new StringEnumConverter() }
                               };

            return JsonConvert.SerializeObject(features, Formatting.Indented, settings);
        }

        private static void CreateFile(string outputFolderName, string jsonToWrite)
        {
            using (var writer = new StreamWriter(outputFolderName, false, Encoding.UTF8))
            {
                writer.Write(jsonToWrite);
                writer.Close();
            }
        }
    }
}
