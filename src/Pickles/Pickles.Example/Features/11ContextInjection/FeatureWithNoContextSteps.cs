using TechTalk.SpecFlow;

namespace Specs.ContextInjection
{
    [Binding]
    public class FeatureWithNoContextSteps
    {
        [Given("a feature which requires no context")]
        public void GivenAFeatureWhichRequiresNoContext()
        {
        }

        [Then("everything is dandy")]
        public void ThenEverythingIsDandy()
        {
        }
    }
}