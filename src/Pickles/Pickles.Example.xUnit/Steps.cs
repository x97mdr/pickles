using System;
using Should;
using TechTalk.SpecFlow;

namespace PicklesDoc.Pickles.Example.xUnit
{
    [Binding]
    public class Steps
    {
        private int? result;
        private int? value1;
        private int? value2;

        [Given("I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredSomethingIntoTheCalculator(int number)
        {
            if (!this.value1.HasValue)
            {
                this.value1 = number;
            }
            else
            {
                this.value2 = number;
            }
        }

        [When("I press (.*)")]
        public void WhenIPressAdd(string button)
        {
            switch (button.ToLowerInvariant())
            {
                case "add":
                    this.result = this.value1.Value + this.value2.Value;
                    break;
                case "subtract":
                    this.result = this.value1.Value - this.value2.Value;
                    break;
                case "multiply":
                    this.result = this.value1.Value*this.value2.Value;
                    break;
                case "divide":
                    this.result = this.value1.Value/this.value2.Value;
                    break;
                case "sin":
                    this.result = Convert.ToInt32(Math.Sin(Convert.ToDouble(this.value1.Value)));
                    break;
                case "cos":
                    this.result = Convert.ToInt32(Math.Cos(Convert.ToDouble(this.value1.Value)));
                    break;
                case "tan":
                    this.result = Convert.ToInt32(Math.Floor(Math.Tan(Convert.ToDouble(this.value1.Value))));
                    break;
                case "C":
                    this.result = null;
                    break;
            }
        }

        [Then("the result should be (.*) on the screen")]
        public void ThenTheResultShouldBe(int result)
        {
            this.result.ShouldEqual(result);
        }

        [Then(@"the screen should be empty")]
        public void ThenTheScreenShouldBeEmpty()
        {
            this.result.HasValue.ShouldBeFalse();
        }
    }
}