using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gherkin.lexer;

namespace Pickles.Parser
{
    public class PicklesParser : Listener
    {
        private readonly List<string> featureTags;
        private readonly List<string> scenarioTags;
        private Feature theFeature;
        private ScenarioBuilder scenarioBuilder;
        private ScenarioBuilder backgroundBuilder;
        private ScenarioOutlineBuilder scenarioOutlineBuilder;
        private StepBuilder stepBuilder;

        private FeatureElementState featureElementState;
        private bool isFeatureFound = false;
        private bool isInExample = false;

        public PicklesParser()
        {
            this.featureTags = new List<string>();
            this.scenarioTags = new List<string>();
        }

        public Feature GetFeature()
        {
            return this.theFeature;
        }

        private void AddStepToElement(Step step)
        {
            if (this.featureElementState.IsBackgroundActive)
            {
                this.backgroundBuilder.AddStep(step);
            }
            else if (this.featureElementState.IsScenarioActive)
            {
                this.scenarioBuilder.AddStep(step);
            }
            else if (this.featureElementState.IsScenarioOutlineActive)
            {
                this.scenarioOutlineBuilder.AddStep(step);
            }
        }

        private void CaptureAndStoreRemainingElements()
        {
            if (this.featureElementState.IsBackgroundActive)
            {
                this.backgroundBuilder.AddStep(this.stepBuilder.GetResult());
                this.theFeature.Background = this.backgroundBuilder.GetResult();
            }
            else if (this.featureElementState.IsScenarioActive)
            {
                if (this.stepBuilder != null) this.scenarioBuilder.AddStep(this.stepBuilder.GetResult());
                this.theFeature.AddScenario(this.scenarioBuilder.GetResult());
            }
            else if (this.featureElementState.IsScenarioOutlineActive)
            {
                if (this.stepBuilder != null) this.scenarioOutlineBuilder.AddStep(this.stepBuilder.GetResult());
                this.theFeature.AddScenarioOutline(this.scenarioOutlineBuilder.GetResult());
            }

            this.stepBuilder = null;
            this.scenarioBuilder = null;
            this.scenarioOutlineBuilder = null;
            this.backgroundBuilder = null;
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
                this.featureTags.Add(tag);
            }
            else
            {
                this.scenarioTags.Add(tag);
            }
        }

        public void feature(string keyword, string name, string description, int line)
        {
            this.isInExample = false;
            this.isFeatureFound = true;
            this.theFeature = new Feature();
            this.theFeature.Name = name;
            this.theFeature.Description = description;
            this.theFeature.Tags = new List<string>(this.featureTags);
            this.featureTags.Clear();
        }

        public void background(string keyword, string name, string description, int line)
        {
            this.isInExample = false;
            this.featureElementState.SetBackgroundActive();
            this.backgroundBuilder = new ScenarioBuilder();
            this.backgroundBuilder.SetName(name);
            this.backgroundBuilder.SetDescription(description);
        }

        public void scenario(string keyword, string name, string description, int line)
        {
            CaptureAndStoreRemainingElements();

            this.isInExample = false;
            this.featureElementState.SetScenarioActive();
            this.scenarioBuilder = new ScenarioBuilder();
            this.scenarioBuilder.SetName(name);
            this.scenarioBuilder.SetDescription(description);
            this.scenarioBuilder.AddTags(this.scenarioTags);
            this.scenarioTags.Clear();
        }

        public void scenarioOutline(string keyword, string name, string description, int line)
        {
            CaptureAndStoreRemainingElements();

            this.isInExample = false;
            this.featureElementState.SetScenarioOutlineActive();
            this.scenarioOutlineBuilder = new ScenarioOutlineBuilder(new TableBuilder());
            this.scenarioOutlineBuilder.SetName(name);
            this.scenarioOutlineBuilder.SetDescription(description);
        }

        public void examples(string keyword, string name, string description, int line)
        {
            this.isInExample = true;
            this.scenarioOutlineBuilder.SetExampleName(name);
            this.scenarioOutlineBuilder.SetExampleDescription(description);
        }

        public void step(string keyword, string name, int line)
        {
            if (this.stepBuilder != null)
            {
                AddStepToElement(this.stepBuilder.GetResult());
            }

            this.stepBuilder = new StepBuilder(new TableBuilder());
            this.stepBuilder.SetKeyword(keyword);
            this.stepBuilder.SetName(name);
        }

        public void row(java.util.List cells, int line)
        {
            if (this.isInExample)
            {
                this.scenarioOutlineBuilder.SetExampleTableRow(cells.toArray().Select(cell => cell.ToString()).ToList());
            }
            else
            {
                this.stepBuilder.AddTableRow(cells.toArray().Select(cell => cell.ToString()).ToList());
            }
        }

        public void docString(string contentType, string content, int line)
        {
            this.stepBuilder.SetDocString(content);
        }

        public void eof()
        {
            CaptureAndStoreRemainingElements();
        }

        #endregion
    }
}
