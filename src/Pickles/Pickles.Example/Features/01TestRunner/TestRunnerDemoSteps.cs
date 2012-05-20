using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.TestRunner
{
    [Binding]
    public class TestRunnerDemoSteps
    {
        [Given(@"I have step defintions in place")]
        public void IHaveStepsDefintionsInPlace()
        {
        }

        [When(@"I call a step")]
        public void CallAStep()
        {
            ScenarioContext.Current.Set(true, "WhenCalled");
        }

        [Then(@"the step should have been called")]
        public void StepShouldHaveBeenCalled()
        {
            bool whencalled = bool.Parse(ScenarioContext.Current["WhenCalled"].ToString());
            whencalled.Should().Be.True();
        }
    }
}