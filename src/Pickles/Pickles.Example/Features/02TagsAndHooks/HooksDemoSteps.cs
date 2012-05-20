using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Specs.TagsAndHooks
{
    [Binding]
    public class HooksDemoSteps
    {
        private static bool _beforeTestRunHookExecuted;
        private static bool _beforeFeatureHookExecuted;
        private static bool _afterFeatureHookExecuted;
        private static bool _afterScenarioHookExecuted;
        private static bool _afterScenarioBlockHookExecuted;
        private static bool _afterStepHookExecuted;

        private static string report;
        private static int reportIndentation;
        private bool _beforeScenarioBlockHookExecuted;
        private bool _beforeScenarioHookExecuted;
        private bool _beforeStepHookExecuted;

        private static void Report(string text)
        {
            if (text.StartsWith("Before"))
                reportIndentation++;
            else
                reportIndentation--;

            report += string.Format("{0}'{1}' was called\n", new string('-', reportIndentation), text);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _beforeTestRunHookExecuted = true;
            Report("BeforeTestRun");
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            _beforeFeatureHookExecuted = true;
            Report("BeforeFeature");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            _beforeScenarioHookExecuted = true;
            Report("BeforeScenario");
        }

        [BeforeScenarioBlock]
        public void BeforeScenarioBlock()
        {
            _beforeScenarioBlockHookExecuted = true;
            Report("BeforeScenarioBlock");
        }

        [BeforeStep]
        public void BeforeStep()
        {
            _beforeStepHookExecuted = true;
            Report("BeforeStep");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            // Testing AfterTestRun is tricky as it would probably involve manipulating the AppDomain
            // Its been manually tested and verified that this code block is hit, but if something changes
            // which stops it from being called, we'll never know...

            Assert.That(_afterFeatureHookExecuted, Is.True);
            Assert.That(_afterScenarioHookExecuted, Is.True);
            Assert.That(_afterScenarioBlockHookExecuted, Is.True);
            Assert.That(_afterStepHookExecuted, Is.True);
            Report("AfterTestRun");

            //Debug.WriteLine(report);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            _afterFeatureHookExecuted = true;
            Report("AfterFeature");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _afterScenarioHookExecuted = true;
            Report("AfterScenario");
        }

        [AfterScenarioBlock]
        public void AfterScenarioBlock()
        {
            _afterScenarioBlockHookExecuted = true;
            Report("AfterScenarioBlock");
        }

        [AfterStep]
        public void AfterStep()
        {
            _afterStepHookExecuted = true;
            Report("AfterStep");
        }

        [Given(@"the scenario is running")]
        public void GivenTheScenarioIsRunning()
        {
        }

        [Then(@"the BeforeTestRun hook should have been executed")]
        public void ThenTheBeforeTestRunHookShouldHaveBeenExecuted()
        {
            Assert.That(_beforeTestRunHookExecuted, Is.True);
        }

        [Then(@"the BeforeFeature hook should have been executed")]
        public void ThenTheBeforeFeatureHookShouldHaveBeenExecuted()
        {
            Assert.That(_beforeFeatureHookExecuted, Is.True);
        }

        [Then(@"the BeforeScenario hook should have been executed")]
        public void ThenTheBeforeScenarioHookShouldHaveBeenExecuted()
        {
            Assert.That(_beforeScenarioHookExecuted, Is.True);
        }

        [Then(@"the BeforeScenarioBlock hook should have been executed")]
        public void ThenTheBeforeScenarioBlockHookShouldHaveBeenExecuted()
        {
            Assert.That(_beforeScenarioBlockHookExecuted, Is.True);
        }

        [Then(@"the BeforeStep hook should have been executed")]
        public void ThenTheBeforeStepHookShouldHaveBeenExecuted()
        {
            Assert.That(_beforeStepHookExecuted, Is.True);
        }
    }
}