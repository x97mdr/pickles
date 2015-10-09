//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ParserFileFactory.cs" company="PicklesDoc">
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
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace PicklesDoc.Pickles.Test
{
    public class ParserFileFactory
    {
        public static IEnumerable<TestCaseData> Files
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                IEnumerable<string> features = resources.Where(name => name.EndsWith(".feature"));
                IEnumerable<string> xhtmls = resources.Where(name => name.EndsWith(".html"));

                foreach (string feature in features)
                {
                    string featureName = feature.Replace(".feature", string.Empty);
                    string associatedXhtml =
                        xhtmls.SingleOrDefault(
                            name =>
                            string.Equals(name.Replace(".html", string.Empty), featureName, StringComparison.InvariantCultureIgnoreCase));

                    if (associatedXhtml != null)
                    {
                        using (
                            var featureStreamReader =
                                new System.IO.StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(feature)))
                        using (
                            var xhtmlStreamReader =
                                new System.IO.StreamReader(
                                    Assembly.GetExecutingAssembly().GetManifestResourceStream(associatedXhtml)))
                        {
                            string featureText = featureStreamReader.ReadToEnd();
                            string associatedXhtmlText = xhtmlStreamReader.ReadToEnd();

                            yield return new TestCaseData(featureText, associatedXhtmlText);
                        }
                    }
                }
            }
        }
    }
}