//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenParsingXUnitResultsFileWithIndividualResults.cs" company="PicklesDoc">
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

using NFluent;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks.XUnit.XUnit1;

namespace PicklesDoc.Pickles.TestFrameworks.UnitTests.XUnit1
{
    [TestFixture]
    public class WhenParsingXUnitResultsFileWithIndividualResults : WhenParsingTestResultFiles<XUnit1Results>
    {
        public WhenParsingXUnitResultsFileWithIndividualResults()
            : base("XUnit1." + "results-example-xunit.xml")
        {
        }


        [Test]
        public void ThenCanReadIndividualResultsFromScenarioOutline_AllPass_ShouldBeTestResultPassed()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Scenario Outlines" };

            var scenarioOutline = new ScenarioOutline { Name = "This is a scenario outline where all scenarios pass", Feature = feature };

            TestResult exampleResultOutline = results.GetScenarioOutlineResult(scenarioOutline);
            Check.That(exampleResultOutline).IsEqualTo(TestResult.Passed);

            TestResult exampleResult1 = results.GetExampleResult(scenarioOutline, new[] { "pass_1" });
            Check.That(exampleResult1).IsEqualTo(TestResult.Passed);

            TestResult exampleResult2 = results.GetExampleResult(scenarioOutline, new[] { "pass_2" });
            Check.That(exampleResult2).IsEqualTo(TestResult.Passed);

            TestResult exampleResult3 = results.GetExampleResult(scenarioOutline, new[] { "pass_3" });
            Check.That(exampleResult3).IsEqualTo(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadIndividualResultsFromScenarioOutline_OneFailed_ShouldBeTestResultFailed_xUnitDoesNotSupportInconclusive()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Scenario Outlines" };

            var scenarioOutline = new ScenarioOutline { Name = "This is a scenario outline where one scenario is inconclusive", Feature = feature };

            TestResult exampleResultOutline = results.GetScenarioOutlineResult(scenarioOutline);
            Check.That(exampleResultOutline).IsEqualTo(TestResult.Failed);

            TestResult exampleResult1 = results.GetExampleResult(scenarioOutline, new[] { "pass_1" });
            Check.That(exampleResult1).IsEqualTo(TestResult.Passed);

            TestResult exampleResult2 = results.GetExampleResult(scenarioOutline, new[] { "pass_2" });
            Check.That(exampleResult2).IsEqualTo(TestResult.Passed);

            TestResult exampleResult3 = results.GetExampleResult(scenarioOutline, new[] { "inconclusive_1" });
            Check.That(exampleResult3).IsEqualTo(TestResult.Failed);
        }

        [Test]
        public void ThenCanReadIndividualResultsFromScenarioOutline_OneFailed_ShouldBeTestResultFailed()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Scenario Outlines" };

            var scenarioOutline = new ScenarioOutline { Name = "This is a scenario outline where one scenario fails", Feature = feature };

            TestResult exampleResultOutline = results.GetScenarioOutlineResult(scenarioOutline);
            Check.That(exampleResultOutline).IsEqualTo(TestResult.Failed);

            TestResult exampleResult1 = results.GetExampleResult(scenarioOutline, new[] { "pass_1" });
            Check.That(exampleResult1).IsEqualTo(TestResult.Passed);

            TestResult exampleResult2 = results.GetExampleResult(scenarioOutline, new[] { "pass_2" });
            Check.That(exampleResult2).IsEqualTo(TestResult.Passed);

            TestResult exampleResult3 = results.GetExampleResult(scenarioOutline, new[] { "fail_1" });
            Check.That(exampleResult3).IsEqualTo(TestResult.Failed);
        }

        [Test]
        public void ThenCanReadIndividualResultsFromScenarioOutline_MultipleExampleSections_ShouldBeTestResultFailed()
        {
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Scenario Outlines" };

            var scenarioOutline = new ScenarioOutline { Name = "And we can go totally bonkers with multiple example sections.", Feature = feature };

            TestResult exampleResultOutline = results.GetScenarioOutlineResult(scenarioOutline);
            Check.That(exampleResultOutline).IsEqualTo(TestResult.Failed);

            TestResult exampleResult1 = results.GetExampleResult(scenarioOutline, new[] { "pass_1" });
            Check.That(exampleResult1).IsEqualTo(TestResult.Passed);

            TestResult exampleResult2 = results.GetExampleResult(scenarioOutline, new[] { "pass_2" });
            Check.That(exampleResult2).IsEqualTo(TestResult.Passed);

            TestResult exampleResult3 = results.GetExampleResult(scenarioOutline, new[] { "inconclusive_1" });
            Check.That(exampleResult3).IsEqualTo(TestResult.Failed);

            TestResult exampleResult4 = results.GetExampleResult(scenarioOutline, new[] { "inconclusive_2" });
            Check.That(exampleResult4).IsEqualTo(TestResult.Failed);

            TestResult exampleResult5 = results.GetExampleResult(scenarioOutline, new[] { "fail_1" });
            Check.That(exampleResult5).IsEqualTo(TestResult.Failed);

            TestResult exampleResult6 = results.GetExampleResult(scenarioOutline, new[] { "fail_2" });
            Check.That(exampleResult6).IsEqualTo(TestResult.Failed);
        }
    }
}