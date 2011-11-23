using System.Collections.Generic;
using Should.Fluent;
using Specs.TestEntities;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Specs.CompareToAssist
{
    [Binding]
    public class CompareToSteps
    {
        [Given(@"I have the following person")]
        public void GivenIHaveTheFollowingPerson(Table person)
        {
            var p = person.CreateInstance<Person>();
            ScenarioContext.Current.Set(p);
        }

        [Then(@"CompareToInstance should match this guy")]
        public void CompareToInstanceShouldMatch(Table instanceTableToMatch)
        {
            var p = ScenarioContext.Current.Get<Person>();
            instanceTableToMatch.CompareToInstance(p);
        }

        [Then(@"CompareToInstance should not match this guy")]
        public void CompareToInstanceShouldNotMatch(Table instanceTableToNotMatch)
        {
            var p = ScenarioContext.Current.Get<Person>();
            try
            {
                instanceTableToNotMatch.CompareToInstance(p);
            }
            catch (ComparisonException ex)
            {
                ex.Message.Should().Not.Be.Empty();
            }
        }


        [Then(@"CompareToSet should match this")]
        public void CompareToSetShouldMatch(Table tableToMatch)
        {
            var persons = ScenarioContext.Current.Get<List<Person>>();
            tableToMatch.CompareToSet(persons);
        }

        [Then(@"CompareToSet should not match this")]
        public void CompareToSetShouldNotMatch(Table tableToNotMatch)
        {
            var persons = ScenarioContext.Current.Get<List<Person>>();

            try
            {
                tableToNotMatch.CompareToSet(persons);
            }
            catch (ComparisonException ex)
            {
                ex.Message.Should().Not.Be.Empty();
            }
        }
    }
}
