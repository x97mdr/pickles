//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="JSONDocumentationBuilder.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NGenerics.DataStructures.Trees;
using NGenerics.Patterns.Visitor;
using NLog;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.TestFrameworks;

namespace PicklesDoc.Pickles.DocumentationBuilders.JSON
{
    public class JsonDocumentationBuilder : IDocumentationBuilder
    {
        public const string JsonFileName = @"pickledFeatures.json";
        private static readonly Logger Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType.Name);

        private readonly Configuration configuration;
        private readonly ITestResults testResults;

        private readonly IFileSystem fileSystem;

        public JsonDocumentationBuilder(Configuration configuration, ITestResults testResults, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.testResults = testResults;
            this.fileSystem = fileSystem;
        }

        public string OutputFilePath
        {
            get { return this.fileSystem.Path.Combine(this.configuration.OutputFolder.FullName, JsonFileName); }
        }

        #region IDocumentationBuilder Members

        public void Build(GeneralTree<INode> features)
        {
            if (Log.IsInfoEnabled)
            {
                Log.Info("Writing JSON to {0}", this.configuration.OutputFolder.FullName);
            }

            var featuresToFormat = new List<JsonFeatureWithMetaInfo>();

            var actionVisitor = new ActionVisitor<INode>(node =>
            {
                var featureTreeNode =
                    node as FeatureNode;
                if (featureTreeNode != null)
                {
                    if (this.configuration.HasTestResults)
                    {
                        featuresToFormat.Add(
                            new JsonFeatureWithMetaInfo(
                                featureTreeNode,
                                this.testResults.GetFeatureResult(
                                    featureTreeNode.Feature)));
                    }
                    else
                    {
                        featuresToFormat.Add(
                            new JsonFeatureWithMetaInfo(
                                featureTreeNode));
                    }
                }
            });

            features.AcceptVisitor(actionVisitor);

            this.CreateFile(this.OutputFilePath, this.GenerateJson(featuresToFormat));
        }

        #endregion

        private string GenerateJson(List<JsonFeatureWithMetaInfo> features)
        {
            var data = new
            {
                Features = features,
                Configuration = new
                {
                    SutName = this.configuration.SystemUnderTestName,
                    SutVersion = this.configuration.SystemUnderTestVersion,
                    GeneratedOn = DateTime.Now.ToString("d MMMM yyyy HH:mm:ss")
                }
            };

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            };

            return JsonConvert.SerializeObject(data, Formatting.Indented, settings);
        }

        private void CreateFile(string outputFolderName, string jsonToWrite)
        {
            using (var writer = this.fileSystem.File.CreateText(outputFolderName))
            {
                writer.Write(jsonToWrite);
                writer.Close();
            }
        }
    }
}
