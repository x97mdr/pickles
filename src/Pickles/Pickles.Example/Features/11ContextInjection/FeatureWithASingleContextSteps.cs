using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.ContextInjection
{
    [Binding]
    public class FeatureWithASingleContextSteps
    {
        private readonly SingleContext _context;

        public FeatureWithASingleContextSteps(SingleContext context)
        {
            _context = context;
            _context.WasCreatedBy = "Feature With A Single Context";
        }

        [Given("a feature which requires a single context")]
        public void GivenAFeatureWhichRequiresASingleContext()
        {
        }

        [Then("the context is set")]
        public void ThenTheContextIsSet()
        {
            _context.Should().Not.Be.Null();
        }
    }
}