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

using System.Collections.Generic;
using System.Linq;
using gherkin;
using gherkin.lexer;
using java.util;

namespace Pickles.Parser
{
    public class PicklesParser : Listener
    {
        private readonly List<string> featureTags;
        private readonly I18n nativeLanguageService;
        private readonly List<string> scenarioTags;
        private ScenarioBuilder backgroundBuilder;

        private FeatureElementState featureElementState;
        private bool isFeatureFound;
        private bool isInExample;
        private ScenarioBuilder scenarioBuilder;
        private ScenarioOutlineBuilder scenarioOutlineBuilder;
        private StepBuilder stepBuilder;
        private Feature theFeature;

        public PicklesParser(I18n nativeLanguageService)
        {
            this.nativeLanguageService = nativeLanguageService;
            featureTags = new List<string>();
            scenarioTags = new List<string>();
        }

        #region Listener Members

        public void comment(string comment, int line)
        {
            // TODO - implement search for metatags here in the future
        }

        public void tag(string tag, int line)
        {
            if (!isFeatureFound)
            {
                featureTags.Add(tag);
            }
            else
            {
                scenarioTags.Add(tag);
            }
        }

        public void feature(string keyword, string name, string description, int line)
        {
            isInExample = false;
            isFeatureFound = true;
            theFeature = new Feature();
            theFeature.Name = name;
            theFeature.Description = description;
            theFeature.Tags = new List<string>(featureTags);
            featureTags.Clear();
        }

        public void background(string keyword, string name, string description, int line)
        {
            isInExample = false;
            featureElementState.SetBackgroundActive();
            backgroundBuilder = new ScenarioBuilder();
            backgroundBuilder.SetName(name);
            backgroundBuilder.SetDescription(description);
        }

        public void scenario(string keyword, string name, string description, int line)
        {
            CaptureAndStoreRemainingElements();

            isInExample = false;
            featureElementState.SetScenarioActive();
            scenarioBuilder = new ScenarioBuilder();
            scenarioBuilder.SetName(name);
            scenarioBuilder.SetDescription(description);
            scenarioBuilder.AddTags(scenarioTags);
            scenarioTags.Clear();
        }

        public void scenarioOutline(string keyword, string name, string description, int line)
        {
            CaptureAndStoreRemainingElements();

            isInExample = false;
            featureElementState.SetScenarioOutlineActive();
            scenarioOutlineBuilder = new ScenarioOutlineBuilder(new TableBuilder());
            scenarioOutlineBuilder.SetName(name);
            scenarioOutlineBuilder.SetDescription(description);
        }

        public void examples(string keyword, string name, string description, int line)
        {
            isInExample = true;
            scenarioOutlineBuilder.SetExampleName(name);
            scenarioOutlineBuilder.SetExampleDescription(description);
        }

        public void step(string keyword, string name, int line)
        {
            if (stepBuilder != null)
            {
                AddStepToElement(stepBuilder.GetResult());
            }

            stepBuilder = new StepBuilder(new TableBuilder(), nativeLanguageService);
            stepBuilder.SetKeyword(keyword);
            stepBuilder.SetName(name);
        }

        public void row(List cells, int line)
        {
            if (isInExample)
            {
                scenarioOutlineBuilder.SetExampleTableRow(cells.toArray().Select(cell => cell.ToString()).ToList());
            }
            else
            {
                stepBuilder.AddTableRow(cells.toArray().Select(cell => cell.ToString()).ToList());
            }
        }

        public void docString(string contentType, string content, int line)
        {
            stepBuilder.SetDocString(content);
        }

        public void eof()
        {
            CaptureAndStoreRemainingElements();
        }

        #endregion

        public Feature GetFeature()
        {
            return theFeature;
        }

        private void AddStepToElement(Step step)
        {
            if (featureElementState.IsBackgroundActive)
            {
                backgroundBuilder.AddStep(step);
            }
            else if (featureElementState.IsScenarioActive)
            {
                scenarioBuilder.AddStep(step);
            }
            else if (featureElementState.IsScenarioOutlineActive)
            {
                scenarioOutlineBuilder.AddStep(step);
            }
        }

        private void CaptureAndStoreRemainingElements()
        {
            if (featureElementState.IsBackgroundActive)
            {
                backgroundBuilder.AddStep(stepBuilder.GetResult());
                theFeature.AddBackground(backgroundBuilder.GetResult());
            }
            else if (featureElementState.IsScenarioActive)
            {
                if (stepBuilder != null) scenarioBuilder.AddStep(stepBuilder.GetResult());
                theFeature.AddFeatureElement(scenarioBuilder.GetResult());
            }
            else if (featureElementState.IsScenarioOutlineActive)
            {
                if (stepBuilder != null) scenarioOutlineBuilder.AddStep(stepBuilder.GetResult());
                theFeature.AddFeatureElement(scenarioOutlineBuilder.GetResult());
            }

            stepBuilder = null;
            scenarioBuilder = null;
            scenarioOutlineBuilder = null;
            backgroundBuilder = null;
        }
    }
}