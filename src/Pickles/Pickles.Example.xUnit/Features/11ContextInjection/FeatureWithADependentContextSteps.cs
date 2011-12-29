using Should;
using TechTalk.SpecFlow;

namespace Specs.ContextInjection
{
    [Binding]
    public class FeatureWithADependentContextSteps
    {
        private readonly SingleContext _context;

        public FeatureWithADependentContextSteps(SingleContext context)
        {
            _context = context;
        }

        [Given("a feature which requires a dependent context")]
        public void GivenAFeatureWhichRequiresADependentContext()
        {
        }

        [Then("the context was created by the feature with a single context scenario")]
        public void ThenTheContextWasCreatedByTheFeatureWithASingleContextScenario()
        {
            _context.WasCreatedBy.ShouldEqual("Feature With A Single Context"); 
        }
    }
}
