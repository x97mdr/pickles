using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.Background
{
    [Binding]
    public class BackgroundSteps
    {
        private const string SUM = "SUM";

        [Given(@"I have initialized the Sum-variable to 0")]
        public void InitiVariableToZero()
        {
            ScenarioContext.Current.Set(0, SUM);
        }

        [When(@"I add (\d+) to the Sum-variable")]
        public void AddIntegerToSum(int numberToAdd)
        {
            int sum = int.Parse(ScenarioContext.Current[SUM].ToString());
            sum += numberToAdd;
            ScenarioContext.Current.Set(sum, SUM);
        }

        [Then(@"the total sum should be (\d+)")]
        public void ThenTheSumShouldBe(int expectedSum)
        {
            int sum = int.Parse(ScenarioContext.Current[SUM].ToString());
            sum.Should().Equal(expectedSum);
        }
    }
}