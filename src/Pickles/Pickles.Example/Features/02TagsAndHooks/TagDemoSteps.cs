using System;
using System.Linq;
using Should.Fluent;
using TechTalk.SpecFlow;

namespace Specs.TagsAndHooks
{
    [Binding]
    public class TagDemoSteps
    {
        private bool _noTags;
        private bool _testTag1;
        private bool _testTag2;
        private bool _testTag3;
        private bool _testTags;

        [BeforeScenario]
        public void ThisHookRunBeforeAllScenariosRegardlessOfTheirTags()
        {
            _noTags = true;
        }

        [BeforeScenario("testTag1")]
        public void ThisHookRunBeforeScenariosWithTestTag1()
        {
            _testTag1 = true;
        }

        [BeforeScenario("testTag2")]
        public void ThisHookRunBeforeScenariosWithTestTag2()
        {
            _testTag2 = true;
        }

        [BeforeScenario("testTag3")]
        public void ThisHookRunBeforeScenariosWithTestTag3()
        {
            _testTag3 = true;
        }

        [BeforeScenario("testTag1", "testTag2", "testTag3")]
        public void ThisHookRunBeforeScenariosWithTestTag1_2_or_3()
        {
            _testTags = true;
        }

        [When(@"I run the scenario")]
        public void WhenIRunTheScenario()
        {
            // Nothing to do here
        }

        [Given(@"that my scenario has (\d) tags")]
        public void GivenThatMyScenarioHas1Tag(int numberOfExpectedTags)
        {
            if (numberOfExpectedTags <= 0)
            {
                ScenarioContext.Current.ScenarioInfo.Tags.Count().Should().Equal(0);
            }
            else
            {
                ScenarioContext.Current.ScenarioInfo.Tags.Should().Not.Be.Null();
                ScenarioContext.Current.ScenarioInfo.Tags.ToList().Count.Should().Equal(numberOfExpectedTags);
            }
        }

        [Then("before scenario hook with '(.*)' is run")]
        public void AssertCorrectHooksHasBeenRun(string expectedTags)
        {
            var tags = from t in expectedTags.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       select t.Trim();

            // I wouldn't dream about writing code like this if
            // I didn't wanted to demonstrate a principle
            if (tags.Count() == 0)
            {
                // If no tags was set the only hook that is to be run is the
                // hook with no tags is run
                _noTags.Should().Be.True();

                // And all the other hooks are not run
                _testTag1.Should().Be.False();
                _testTag2.Should().Be.False();
                _testTag3.Should().Be.False();
                _testTags.Should().Be.False();
            }

            if (tags.Count() > 0)
            {
                // If there were any tags set
                // the no tag hook is still run (!)
                // and also the hook with all the tags 
                _noTags.Should().Be.True();
                _testTags.Should().Be.True();

            }

            // Finally the hooks for each tag is set in their respective hook
            if (tags.Contains("testTag1"))
            {
                _testTag1.Should().Be.True();
            }

            if (tags.Contains("testTag2"))
            {
                _testTag2.Should().Be.True();
            }

            if (tags.Contains("testTag3"))
            {
                _testTag3.Should().Be.True();
            }
        }

    }
}