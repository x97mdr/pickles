using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs._00BasicGherkin
{
    [Binding]
    public class BasicGherkinSteps
    {
        private Application SUT;
        protected string returnValue { get; set; }

        [Given(@"the initial state of the application is Running")]
        public void InitialStateIsRunning()
        {
            SUT = new Application {State = "Running"};
        }

        [Given(@"I have authorization to ask application state")]
        public void IsAuthorized()
        {
        }

        [When(@"I ask what the application state is")]
        public void AskState()
        {
            returnValue = SUT.State;
        }

        [Then(@"I should see Running as the answer")]
        public void SeeRunninAsState()
        {
            SUT.State.Should().Equal("Running");
        }

        [Then(@"I should see the time of the application")]
        public void SeeTheTime()
        {
        }

        [Then(@"the state of the application should not be Stopped")]
        public void StateisNotStopped()
        {
            SUT.State.Should().Not.Equal("Stopped");
        }

        #region Nested type: Application

        private class Application
        {
            public Application()
            {
                State = "Running";
            }

            public string State { get; set; }
        }

        #endregion
    }
}