﻿using System.Collections.Generic;
using System.Linq;
using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.ScenarioOutline
{
    [Binding]
    public class ScenarioOutlineSteps
    {
        private const string VALUES_KEY = "Values";
        private const string SUM_KEY = "Result";

        private List<int> TalLista
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey(VALUES_KEY))
                {
                    ScenarioContext.Current.Set(new List<int>(), VALUES_KEY);
                }

                return ScenarioContext.Current.Get<List<int>>(VALUES_KEY);
            }
        }

        [Given(@"I enter (\d+) into the calculator")]
        public void GivenIHaveEntered(int numberToEnter)
        {
            TalLista.Add(numberToEnter);
        }


        [When(@"I preform add")]
        public void WhenIPressAdd()
        {
            ScenarioContext.Current.Set(TalLista.Sum(), SUM_KEY);
        }

        [Then(@"the result should be (\d+)")]
        public void ThenTheResultShouldBe(int expectedResult)
        {
            var summa = int.Parse(ScenarioContext.Current[SUM_KEY].ToString());
            summa.Should().Equal(expectedResult);
        }
    }
}
