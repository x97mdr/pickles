using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Pickles.Example
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
            if (!value1.HasValue)
            {
                value1 = number;
            }
            else
            {
                value2 = number;
            }
        }

        [When("I press (.*)")]
        public void WhenIPressAdd(string button)
        {
            switch (button.ToLowerInvariant())
            {
                case "add":
                    result = value1.Value + value2.Value;
                    break;
                case "subtract":
                    result = value1.Value - value2.Value;
                    break;
                case "multiply":
                    result = value1.Value*value2.Value;
                    break;
                case "divide":
                    result = value1.Value/value2.Value;
                    break;
                case "sin":
                    result = Convert.ToInt32(Math.Sin(Convert.ToDouble(value1.Value)));
                    break;
                case "cos":
                    result = Convert.ToInt32(Math.Cos(Convert.ToDouble(value1.Value)));
                    break;
                case "tan":
                    result = Convert.ToInt32(Math.Floor(Math.Tan(Convert.ToDouble(value1.Value))));
                    break;
                case "C":
                    result = null;
                    break;
            }
        }

        [Then("the result should be (.*) on the screen")]
        public void ThenTheResultShouldBe(int result)
        {
            Assert.AreEqual(result, this.result);
        }

        [Then(@"the screen should be empty")]
        public void ThenTheScreenShouldBeEmpty()
        {
            Assert.AreEqual(false, result.HasValue);
        }
    }
}