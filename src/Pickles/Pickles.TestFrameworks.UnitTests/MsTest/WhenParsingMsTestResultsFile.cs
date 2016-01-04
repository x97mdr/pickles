//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="WhenParsingMsTestResultsFile.cs" company="PicklesDoc">
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
using PicklesDoc.Pickles.TestFrameworks.MsTest;

namespace PicklesDoc.Pickles.TestFrameworks.UnitTests.MsTest
{
    [TestFixture]
    public class WhenParsingMsTestResultsFile : WhenParsingTestResultFiles<MsTestResults>
    {
        public WhenParsingMsTestResultsFile()
            : base("MsTest." + "results-example-mstest.trx")
        {
        }

        [Test]
        public void ThenCanReadBackgroundResultSuccessfully()
        {
            var background = new Scenario { Name = "Background", Feature = this.AdditionFeature() };
            var feature = this.AdditionFeature();
            feature.AddBackground(background);
            var results = ParseResultsFile();

            TestResult result = results.GetScenarioResult(background);

            Check.That(result).IsEqualTo(TestResult.Inconclusive);
        }

        [Test]
        public void ThenCanReadInconclusiveFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.InconclusiveFeature());

            Check.That(result).IsEqualTo(TestResult.Inconclusive);
        }

        [Test]
        public void ThenCanReadFailedFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.FailingFeature());

            Check.That(result).IsEqualTo(TestResult.Failed);
        }

        [Test]
        public void ThenCanReadPassedFeatureResultSuccessfully()
        {
            var results = ParseResultsFile();

            TestResult result = results.GetFeatureResult(this.PassingFeature());

            Check.That(result).IsEqualTo(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadScenarioOutlineResultSuccessfully()
        {
            var results = ParseResultsFile();
            var scenarioOutline = new ScenarioOutline { Name = "Adding several numbers", Feature = this.AdditionFeature() };

            TestResult result = results.GetScenarioOutlineResult(scenarioOutline);

            Check.That(result).IsEqualTo(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadSuccessfulScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var passedScenario = new Scenario { Name = "Add two numbers", Feature = this.AdditionFeature() };

            TestResult result = results.GetScenarioResult(passedScenario);

            Check.That(result).IsEqualTo(TestResult.Passed);
        }

        [Test]
        public void ThenCanReadFailedScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var scenario = new Scenario { Name = "Fail to add two numbers", Feature = this.AdditionFeature() };
            TestResult result = results.GetScenarioResult(scenario);

            Check.That(result).IsEqualTo(TestResult.Failed);
        }

        [Test]
        public void ThenCanReadIgnoredScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();
            var ignoredScenario = new Scenario { Name = "Ignored adding two numbers", Feature = this.AdditionFeature() };

            var result = results.GetScenarioResult(ignoredScenario);

            Check.That(result).IsEqualTo(TestResult.Inconclusive);
        }

        [Test]
        public void ThenCanReadInconclusiveScenarioResultSuccessfully()
        {
            var results = ParseResultsFile();

            var inconclusiveScenario = new Scenario
            {
                Name = "Not automated adding two numbers",
                Feature = this.AdditionFeature()
            };

            var result = results.GetScenarioResult(inconclusiveScenario);

            Check.That(result).IsEqualTo(TestResult.Inconclusive);
        }

        private Feature AdditionFeature()
        {
            return new Feature { Name = "Addition" };
        }

        private Feature InconclusiveFeature()
        {
            return new Feature { Name = "Inconclusive" };
        }

        private Feature FailingFeature()
        {
            return new Feature { Name = "Failing" };
        }

        private Feature PassingFeature()
        {
            return new Feature { Name = "Passing" };
        }
    }
}
