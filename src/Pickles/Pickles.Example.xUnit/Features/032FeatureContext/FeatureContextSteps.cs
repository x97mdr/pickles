using Should.Fluent;
using Specs.TestEntities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Specs._032FeatureContext
{
    [Binding]
    public class FeatureContextSteps
    {
        [When(@"I store a person called (.*) in the current FeatureContext")]
        public void StorePersonInFeatureContext(string personName)
        {
            var p = new Person {Name = personName};
            FeatureContext.Current.Set(p);
            FeatureContext.Current.Set(p, "PersonKey");
        }

        [Then("a person called (.*) can easily be retrieved from the current FeatureContext")]
        public void RetrieveFromFeatureContext(string personName)
        {
            var pByType = FeatureContext.Current.Get<Person>();
            pByType.Should().Not.Be.Null();

            var pByKey = FeatureContext.Current["PersonKey"] as Person;
            pByKey.Should().Not.Be.Null();
        }

        [When(@"I execute any scenario in the feature")]
        public void ExecuteAnyScenario()
        {
        }

        [Then(@"the FeatureInfo contains the following information")]
        public void FeatureInfoContainsInterestingInformation(Table table)
        {
            // Create our small DTO for the info from the step
            var fromStep = table.CreateInstance<FeatureInformation>();
            fromStep.Tags = table.Rows[0]["Value"].Split(',');

            // Short-hand to the feature information
            FeatureInfo fi = FeatureContext.Current.FeatureInfo;

            // Assertions
            fi.Title.Should().Equal(fromStep.Title);
            fi.GenerationTargetLanguage.Should().Equal(fromStep.TargetLanguage);
            fi.Description.Should().StartWith(fromStep.Description);
            fi.Language.IetfLanguageTag.Should().Equal(fromStep.Language);
            for (int i = 0; i < fi.Tags.Length - 1; i++)
            {
                fi.Tags[i].Should().Equal(fromStep.Tags[i]);
            }
        }

        #region Nested type: FeatureInformation

        private class FeatureInformation
        {
            public string Title { get; set; }
            public ProgrammingLanguage TargetLanguage { get; set; }
            public string Description { get; set; }
            public string Language { get; set; }
            public string[] Tags { get; set; }
        }

        #endregion
    }
}