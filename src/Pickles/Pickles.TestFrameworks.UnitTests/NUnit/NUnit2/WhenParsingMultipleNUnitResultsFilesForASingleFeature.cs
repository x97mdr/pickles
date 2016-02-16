//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenParsingMultipleNUnitResultsFilesForASingleFeature.cs" company="PicklesDoc">
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

using NFluent;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks.NUnit.NUnit2;

namespace PicklesDoc.Pickles.TestFrameworks.UnitTests.NUnit.NUnit2
{
    [TestFixture]
    public class WhenParsingMultipleNUnitResultsFilesForASingleFeature : WhenParsingTestResultFiles<NUnit2Results>
    {
        public WhenParsingMultipleNUnitResultsFilesForASingleFeature()
            : base("NUnit.NUnit2." + @"results-example-nunit - Run 1.xml;" + "NUnit.NUnit2." + "results-example-nunit - Run 2.xml")
        {
        }

        [Test]
        public void ThenCanReadFeatureResultSuccessfully()
        {
            // Write out the embedded test results file
            var results = ParseResultsFile();

            var feature = new Feature { Name = "FeatureWithMultipleResultsFiles" };
            TestResult result = results.GetFeatureResult(feature);

            Check.That(result).IsEqualTo(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "FeatureWithMultipleResultsFiles" };
            var scenarioOutline = new ScenarioOutline { Name = "Some scenario outline", Feature = feature };

            TestResult result = results.GetScenarioOutlineResult(scenarioOutline);

            Check.That(result).IsEqualTo(TestResult.Passed);

            TestResult exampleResult1 = results.GetExampleResult(scenarioOutline, new[] { "false" });
            Check.That(exampleResult1).IsEqualTo(TestResult.Passed);

            TestResult exampleResult2 = results.GetExampleResult(scenarioOutline, new[] { "true" });
            Check.That(exampleResult2).IsEqualTo(TestResult.Passed);
        }
    }
}
