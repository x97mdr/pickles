using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.ContextInjection
{
    [Binding]
    public class FeatureWithMultipleContextsSteps
    {
        private readonly SingleContext _context1;
        private readonly SingleContext _context2;

        public FeatureWithMultipleContextsSteps(SingleContext context1, SingleContext context2)
        {
            _context1 = context1;
            _context2 = context2;
        }

        [Given("a feature which requires multiple contexts")]
        public void GivenAFeatureWhichRequiresMultipleContexts()
        {
        }

        [Then("the contexts are set")]
        public void ThenTheContextsAreSet()
        {
            _context1.Should().Not.Be.Null();
            _context2.Should().Not.Be.Null();
        }
    }
}