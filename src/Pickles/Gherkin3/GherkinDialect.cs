using System.Linq;

namespace Gherkin3
{
    public class GherkinDialect
    {
        public string Language { get; private set; }

        public string[] FeatureKeywords { get; private set; }
        public string[] BackgroundKeywords { get; private set; }
        public string[] ScenarioKeywords { get; private set; }
        public string[] ScenarioOutlineKeywords { get; private set; }
        public string[] ExamplesKeywords { get; private set; }
        public string[] GivenStepKeywords { get; private set; }
        public string[] WhenStepKeywords { get; private set; }
        public string[] ThenStepKeywords { get; private set; }
        public string[] AndStepKeywords { get; private set; }
        public string[] ButStepKeywords { get; private set; }


        public string[] StepKeywords { get; private set; }

        public GherkinDialect(
            string language,
            string[] featureKeywords, 
            string[] backgroundKeywords, 
            string[] scenarioKeywords,
            string[] scenarioOutlineKeywords,
            string[] examplesKeywords,
            string[] givenStepKeywords,
            string[] whenStepKeywords,
            string[] thenStepKeywords,
            string[] andStepKeywords,
            string[] butStepKeywords)
        {
            this.Language = language;
            this.FeatureKeywords = featureKeywords;
            this.BackgroundKeywords = backgroundKeywords;
            this.ScenarioKeywords = scenarioKeywords;
            this.ScenarioOutlineKeywords = scenarioOutlineKeywords;
            this.ExamplesKeywords = examplesKeywords;
            this.GivenStepKeywords = givenStepKeywords;
            this.WhenStepKeywords = whenStepKeywords;
            this.ThenStepKeywords = thenStepKeywords;
            this.AndStepKeywords = andStepKeywords;
            this.ButStepKeywords = butStepKeywords;

            this.StepKeywords = givenStepKeywords
                .Concat(whenStepKeywords)
                .Concat(thenStepKeywords)
                .Concat(andStepKeywords)
                .Concat(butStepKeywords)
                .Distinct()
                .ToArray();
        }
    }
}
