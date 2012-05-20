using System.IO;
using System.Linq;
using NUnit.Framework;
using Ninject;
using Pickles.Parser;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenParsingFeatureFiles : BaseFixture
    {
        private const string DOC_STRING_DELIMITER = "\"\"\"";

        [Test]
        public void Then_can_parse_feature_with_multiple_scenarios_successfully()
        {
            string featureText =
                @"# ignore this comment
Feature: Test
    In order to do something
    As a user
    I want to run this scenario

	Scenario: A scenario
		Given some feature
		When it runs
		Then I should see that this thing happens	

    Scenario: Another scenario
		Given some other feature
		When it runs
		Then I should see that this other thing happens
        And something else";

            var parser = Kernel.Get<FeatureParser>();
            Feature feature = parser.Parse(new StringReader(featureText));

            Assert.AreNotEqual(null, feature);
            Assert.AreEqual("Test", feature.Name);
            Assert.AreEqual(@"  In order to do something
  As a user
  I want to run this scenario",
                            feature.Description);
            Assert.AreEqual(2, feature.FeatureElements.Count);
            Assert.AreEqual(0, feature.Tags.Count);

            IFeatureElement scenario = feature.FeatureElements[0];
            Assert.AreEqual("A scenario", scenario.Name);
            Assert.AreEqual(string.Empty, scenario.Description);
            Assert.AreEqual(3, scenario.Steps.Count);
            Assert.AreEqual(0, scenario.Tags.Count);

            Step givenStep = scenario.Steps[0];
            Assert.AreEqual(Keyword.Given, givenStep.Keyword);
            Assert.AreEqual("some feature", givenStep.Name);
            Assert.AreEqual(null, givenStep.DocStringArgument);
            Assert.AreEqual(null, givenStep.TableArgument);

            Step whenStep = scenario.Steps[1];
            Assert.AreEqual(Keyword.When, whenStep.Keyword);
            Assert.AreEqual("it runs", whenStep.Name);
            Assert.AreEqual(null, whenStep.DocStringArgument);
            Assert.AreEqual(null, whenStep.TableArgument);

            Step thenStep = scenario.Steps[2];
            Assert.AreEqual(Keyword.Then, thenStep.Keyword);
            Assert.AreEqual("I should see that this thing happens", thenStep.Name);
            Assert.AreEqual(null, thenStep.DocStringArgument);
            Assert.AreEqual(null, thenStep.TableArgument);

            IFeatureElement scenario2 = feature.FeatureElements[1];
            Assert.AreEqual("Another scenario", scenario2.Name);
            Assert.AreEqual(string.Empty, scenario2.Description);
            Assert.AreEqual(4, scenario2.Steps.Count);
            Assert.AreEqual(0, scenario2.Tags.Count);

            Step givenStep2 = scenario2.Steps[0];
            Assert.AreEqual(Keyword.Given, givenStep2.Keyword);
            Assert.AreEqual("some other feature", givenStep2.Name);
            Assert.AreEqual(null, givenStep2.DocStringArgument);
            Assert.AreEqual(null, givenStep2.TableArgument);

            Step whenStep2 = scenario2.Steps[1];
            Assert.AreEqual(Keyword.When, whenStep2.Keyword);
            Assert.AreEqual("it runs", whenStep2.Name);
            Assert.AreEqual(null, whenStep2.DocStringArgument);
            Assert.AreEqual(null, whenStep2.TableArgument);

            Step thenStep2 = scenario2.Steps[2];
            Assert.AreEqual(Keyword.Then, thenStep2.Keyword);
            Assert.AreEqual("I should see that this other thing happens", thenStep2.Name);
            Assert.AreEqual(null, thenStep2.DocStringArgument);
            Assert.AreEqual(null, thenStep2.TableArgument);

            Step thenStep3 = scenario2.Steps[3];
            Assert.AreEqual(Keyword.And, thenStep3.Keyword);
            Assert.AreEqual("something else", thenStep3.Name);
            Assert.AreEqual(null, thenStep3.DocStringArgument);
            Assert.AreEqual(null, thenStep3.TableArgument);
        }

        [Test]
        public void Then_can_parse_most_basic_feature_successfully()
        {
            string featureText =
                @"# ignore this comment
Feature: Test
    In order to do something
    As a user
    I want to run this scenario

	Scenario: A scenario
		Given some feature
		When it runs
		Then I should see that this thing happens";

            var parser = Kernel.Get<FeatureParser>();
            Feature feature = parser.Parse(new StringReader(featureText));

            Assert.AreNotEqual(null, feature);
            Assert.AreEqual("Test", feature.Name);
            Assert.AreEqual(@"  In order to do something
  As a user
  I want to run this scenario",
                            feature.Description);
            Assert.AreEqual(1, feature.FeatureElements.Count);
            Assert.AreEqual(0, feature.Tags.Count);

            IFeatureElement scenario = feature.FeatureElements.First();
            Assert.AreEqual("A scenario", scenario.Name);
            Assert.AreEqual(string.Empty, scenario.Description);
            Assert.AreEqual(3, scenario.Steps.Count);
            Assert.AreEqual(0, scenario.Tags.Count);

            Step givenStep = scenario.Steps[0];
            Assert.AreEqual(Keyword.Given, givenStep.Keyword);
            Assert.AreEqual("some feature", givenStep.Name);
            Assert.AreEqual(null, givenStep.DocStringArgument);
            Assert.AreEqual(null, givenStep.TableArgument);

            Step whenStep = scenario.Steps[1];
            Assert.AreEqual(Keyword.When, whenStep.Keyword);
            Assert.AreEqual("it runs", whenStep.Name);
            Assert.AreEqual(null, whenStep.DocStringArgument);
            Assert.AreEqual(null, whenStep.TableArgument);

            Step thenStep = scenario.Steps[2];
            Assert.AreEqual(Keyword.Then, thenStep.Keyword);
            Assert.AreEqual("I should see that this thing happens", thenStep.Name);
            Assert.AreEqual(null, thenStep.DocStringArgument);
            Assert.AreEqual(null, thenStep.TableArgument);
        }

        [Test]
        public void Then_can_parse_scenario_outlines_successfully()
        {
            string featureText =
                @"# ignore this comment
Feature: Test
    In order to do something
    As a user
    I want to run this scenario

	Scenario Outline: A scenario outline
		Given some feature with <keyword1>
		When it runs
		Then I should see <keyword2>

    Examples:
    | keyword1 | keyword2 |
    | this     | that     |";

            var parser = Kernel.Get<FeatureParser>();
            Feature feature = parser.Parse(new StringReader(featureText));

            var scenarioOutline = feature.FeatureElements[0] as ScenarioOutline;
            Assert.AreNotEqual(null, scenarioOutline);
            Assert.AreEqual("A scenario outline", scenarioOutline.Name);
            Assert.AreEqual(string.Empty, scenarioOutline.Description);
            Assert.AreEqual(3, scenarioOutline.Steps.Count);

            Step givenStep = scenarioOutline.Steps[0];
            Assert.AreEqual(Keyword.Given, givenStep.Keyword);
            Assert.AreEqual("some feature with <keyword1>", givenStep.Name);
            Assert.AreEqual(null, givenStep.DocStringArgument);
            Assert.AreEqual(null, givenStep.TableArgument);

            Step whenStep = scenarioOutline.Steps[1];
            Assert.AreEqual(Keyword.When, whenStep.Keyword);
            Assert.AreEqual("it runs", whenStep.Name);
            Assert.AreEqual(null, whenStep.DocStringArgument);
            Assert.AreEqual(null, whenStep.TableArgument);

            Step thenStep = scenarioOutline.Steps[2];
            Assert.AreEqual(Keyword.Then, thenStep.Keyword);
            Assert.AreEqual("I should see <keyword2>", thenStep.Name);
            Assert.AreEqual(null, thenStep.DocStringArgument);
            Assert.AreEqual(null, thenStep.TableArgument);

            Example examples = scenarioOutline.Example;
            Assert.AreEqual(string.Empty, examples.Name);
            Assert.AreEqual(null, examples.Description);

            Table table = examples.TableArgument;
            Assert.AreEqual("keyword1", table.HeaderRow[0]);
            Assert.AreEqual("keyword2", table.HeaderRow[1]);
            Assert.AreEqual("this", table.DataRows[0][0]);
            Assert.AreEqual("that", table.DataRows[0][1]);
        }

        [Test]
        public void Then_can_parse_scenario_with_background_successfully()
        {
            string featureText =
                @"# ignore this comment
Feature: Test
    In order to do something
    As a user
    I want to run this scenario

    Background: Some background for the scenarios
		Given some prior context
        And yet more prior context

	Scenario: A scenario
		Given some feature
		When it runs
		Then I should see that this thing happens";

            var parser = Kernel.Get<FeatureParser>();
            Feature feature = parser.Parse(new StringReader(featureText));

            Assert.AreNotEqual(null, feature.Background);
            Assert.AreEqual("Some background for the scenarios", feature.Background.Name);
            Assert.AreEqual(string.Empty, feature.Background.Description);
            Assert.AreEqual(2, feature.Background.Steps.Count);
            Assert.AreEqual(0, feature.Background.Tags.Count);

            Step givenStep1 = feature.Background.Steps[0];
            Assert.AreEqual(Keyword.Given, givenStep1.Keyword);
            Assert.AreEqual("some prior context", givenStep1.Name);
            Assert.AreEqual(null, givenStep1.DocStringArgument);
            Assert.AreEqual(null, givenStep1.TableArgument);

            Step givenStep2 = feature.Background.Steps[1];
            Assert.AreEqual(Keyword.And, givenStep2.Keyword);
            Assert.AreEqual("yet more prior context", givenStep2.Name);
            Assert.AreEqual(null, givenStep2.DocStringArgument);
            Assert.AreEqual(null, givenStep2.TableArgument);
        }

        [Test]
        public void Then_can_parse_scenario_with_docstring_successfully()
        {
            string docstring = string.Format(@"{0}
This is a document string
it can be many lines long
{0}",
                                             DOC_STRING_DELIMITER);

            string featureText =
                string.Format(
                    @"Feature: Test
    In order to do something
    As a user
    I want to run this scenario

    Scenario: A scenario
        Given some feature
        {0}
        When it runs
        Then I should see that this thing happens",
                    docstring);

            var parser = Kernel.Get<FeatureParser>();
            Feature feature = parser.Parse(new StringReader(featureText));

            Assert.AreEqual(@"This is a document string
it can be many lines long",
                            feature.FeatureElements[0].Steps[0].DocStringArgument);
        }

        [Test]
        public void Then_can_parse_scenario_with_table_successfully()
        {
            string featureText =
                @"# ignore this comment
Feature: Test
    In order to do something
    As a user
    I want to run this scenario

	Scenario: A scenario
		Given some feature with a table
        | Column1 | Column2 |
        | Value 1 | Value 2 |
		When it runs
		Then I should see that this thing happens";

            var parser = Kernel.Get<FeatureParser>();
            Feature feature = parser.Parse(new StringReader(featureText));

            Table table = feature.FeatureElements[0].Steps[0].TableArgument;
            Assert.AreEqual("Column1", table.HeaderRow[0]);
            Assert.AreEqual("Column2", table.HeaderRow[1]);
            Assert.AreEqual("Value 1", table.DataRows[0][0]);
            Assert.AreEqual("Value 2", table.DataRows[0][1]);
        }

        [Test]
        public void Then_can_parse_scenario_with_tags_successfully()
        {
            string featureText =
                @"# ignore this comment
@feature-tag
Feature: Test
    In order to do something
    As a user
    I want to run this scenario

    @scenario-tag-1 @scenario-tag-2
	Scenario: A scenario
		Given some feature
		When it runs
		Then I should see that this thing happens";

            var parser = new FeatureParser(new LanguageServices(new Configuration()));
            Feature feature = parser.Parse(new StringReader(featureText));

            Assert.AreEqual("@feature-tag", feature.Tags[0]);
            Assert.AreEqual("@scenario-tag-1", feature.FeatureElements[0].Tags[0]);
            Assert.AreEqual("@scenario-tag-2", feature.FeatureElements[0].Tags[1]);
        }
    }
}