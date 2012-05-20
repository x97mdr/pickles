using System;
using Should.Fluent;
using Specs.StepTransformation.Entities;
using TechTalk.SpecFlow;

namespace Specs.StepTransformation
{
    [Binding]
    public class StepArgumentTransformationSteps
    {
        [Given("(.*) has been registered at (.*)")]
        public void RegistrationStep(User user, DateTime dateTime)
        {
        }

        [Given("(.*) has been registered at (.*)")]
        public void RegistrationStep(User user, Terminal terminal)
        {
        }

        [Then(@"I should be able to see (.*) at (.*)")]
        public void SeeUserAtTerminal(User user, Terminal terminal)
        {
            user.Name.Should().Equal(user.Name);
            terminal.Id.Should().Equal(terminal.Id);
        }
    }
}