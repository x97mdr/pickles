using System;
using Should.Fluent;
using Specs.TestEntities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Specs._031ScenarioContext
{
    [Binding]
    public class ScenarioContextSteps
    {
        [When(@"I store a person called (.*) in the Current ScenarioContext")]
        public void StorePersonInScenarioContext(string personName)
        {
            var p = new Person {Name = personName};
            ScenarioContext.Current.Set(p);
            ScenarioContext.Current.Set(p, "PersonKey");
        }

        [Then("a person called (.*) can easily be retrieved")]
        public void RetrieveFromScenarioContext(string personName)
        {
            var pByType = ScenarioContext.Current.Get<Person>();
            pByType.Should().Not.Be.Null();

            var pByKey = ScenarioContext.Current["PersonKey"] as Person;
            pByKey.Should().Not.Be.Null();
        }

        [When(@"I execute any scenario")]
        public void ExecuteAnyScenario()
        {
        }

        [Then(@"the ScenarioInfo contains the following information")]
        public void ScenarioInfoContainsInterestingInformation(Table table)
        {
            // Create our small DTO for the info from the step
            var fromStep = table.CreateInstance<ScenarioInformation>();
            fromStep.Tags = table.Rows[0]["Value"].Split(',');

            // Short-hand to the scenarioInfo
            ScenarioInfo si = ScenarioContext.Current.ScenarioInfo;

            // Assertions
            si.Title.Should().Equal(fromStep.Title);
            for (int i = 0; i < si.Tags.Length - 1; i++)
            {
                si.Tags[i].Should().Equal(fromStep.Tags[i]);
            }
        }

        [When("an error occurs in a step")]
        public void AnErrorOccurs()
        {
            "not correct".Should().Equal("correct");
        }

        [AfterScenario("showingErrorHandling")]
        public void AfterScenarioHook()
        {
            if (ScenarioContext.Current.TestError != null)
            {
                Exception error = ScenarioContext.Current.TestError;
                Console.WriteLine("An error ocurred:" + error.Message);
                Console.WriteLine("It was of type:" + error.GetType().Name);
            }
        }

        [Given("I have a (.*) step")]
        [Given("I have another (.*) step")]
        [When("I have a (.*) step")]
        [Then("I have a (.*) step")]
        public void ReportStepTypeName(string expectedStepType)
        {
            string stepType = ScenarioContext.Current.CurrentScenarioBlock.ToString();
            stepType.Should().Equal(expectedStepType);
        }

        [When("I set the ScenarioContext.Current to pending")]
        public void WhenIHaveAPendingStep()
        {
            ScenarioContext.Current.Pending();
        }

        [Then("this step will not even be executed")]
        public void ThisStepWillNotBeExecuted()
        {
            throw new Exception("See!? This wasn't even thrown");
        }

        #region Nested type: ScenarioInformation

        private class ScenarioInformation
        {
            public string Title { get; set; }
            public string[] Tags { get; set; }
        }

        #endregion
    }
}