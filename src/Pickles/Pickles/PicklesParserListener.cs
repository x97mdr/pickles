using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow.Parser.Gherkin;
using System.IO;

namespace Pickles
{
    public class PicklesParserListener : IGherkinListener
    {
        private readonly TextWriter progressListener;
        private readonly HtmlFeatureFormatter htmlFeatureBuilder;

        public PicklesParserListener(TextWriter progressListener, HtmlFeatureFormatter htmlFeatureBuilder)
        {
            this.progressListener = progressListener;
            this.htmlFeatureBuilder = htmlFeatureBuilder;
        }

        #region IGherkinListener Members

        public void Background(string keyword, string name, string description, GherkinBufferSpan headerSpan, GherkinBufferSpan descriptionSpan)
        {
            throw new NotImplementedException();
        }

        public void Comment(string commentText, GherkinBufferSpan commentSpan)
        {
            throw new NotImplementedException();
        }

        public void EOF(GherkinBufferPosition eofPosition)
        {
        }

        public void Error(string message, GherkinBufferPosition errorPosition, Exception exception)
        {
            this.progressListener.WriteLine("An error occured while parsing : {0}", message);
            if (exception != null)
            {
                this.progressListener.WriteLine("The exception was :\n{0}", exception.ToString());
            }
        }

        public void Examples(string keyword, string name, string description, GherkinBufferSpan headerSpan, GherkinBufferSpan descriptionSpan)
        {
            throw new NotImplementedException();
        }

        public void ExamplesTag(string name, GherkinBufferSpan tagSpan)
        {
            throw new NotImplementedException();
        }

        public void Feature(string keyword, string name, string description, GherkinBufferSpan headerSpan, GherkinBufferSpan descriptionSpan)
        {
            htmlFeatureBuilder.SetNameAndDescription(name, description);
        }

        public void FeatureTag(string name, GherkinBufferSpan tagSpan)
        {
            throw new NotImplementedException();
        }

        public void Init(GherkinBuffer buffer, bool isPartialScan)
        {
        }

        public void MultilineText(string text, GherkinBufferSpan textSpan)
        {
            throw new NotImplementedException();
        }

        public void Scenario(string keyword, string name, string description, GherkinBufferSpan headerSpan, GherkinBufferSpan descriptionSpan)
        {
        }

        public void ScenarioOutline(string keyword, string name, string description, GherkinBufferSpan headerSpan, GherkinBufferSpan descriptionSpan)
        {
            throw new NotImplementedException();
        }

        public void ScenarioTag(string name, GherkinBufferSpan tagSpan)
        {
            throw new NotImplementedException();
        }

        public void Step(string keyword, StepKeyword stepKeyword, ScenarioBlock scenarioBlock, string text, GherkinBufferSpan stepSpan)
        {
        }

        public void TableHeader(string[] cells, GherkinBufferSpan rowSpan, GherkinBufferSpan[] cellSpans)
        {
            throw new NotImplementedException();
        }

        public void TableRow(string[] cells, GherkinBufferSpan rowSpan, GherkinBufferSpan[] cellSpans)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
