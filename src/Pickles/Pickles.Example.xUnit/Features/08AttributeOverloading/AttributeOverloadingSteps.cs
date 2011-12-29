using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.AttributeOverloading
{
    [Binding]
    public class AttributeOverloadingSteps
    {

        [Given(@"I have this simple step")]
        [Given(@"this simple step")]
        [Given(@"also this step")]
        public void ASimpleStep() { }

        [When(@"I do something")]
        public void WhenIDoSomething() { }

        [Then(@"I could validate that the number (\d+) is (.*)")]
        [Then(@"that the number (\d+) is (.*)")]
        [Then(@"the number (\d+) is (.*)")]
        public void ValidateOddOrEven(int numbeToValidate, string comparisonKind)
        {
            var result = numbeToValidate % 2;
            if (comparisonKind.ToLowerInvariant() == "even")
            {
                result.Should().Equal(0);
            }
            else
            {
                result.Should().Not.Equal(0);
            }
        }
    }

}

