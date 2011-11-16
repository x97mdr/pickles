namespace Pickles.Test
{
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    using Pickles.Parser;

    using FeatureParser = Pickles.FeatureParser;

    [TestFixture]
    public class WhenParsingLocalizedFeatureFiles : BaseFixture
    {
        [Test]
        public void Then_can_parse_most_basic_feature_successfully()
        {
            string featureText =
                @"# ignorera denna kommentar
Egenskap: Test egenskap
    Som svensk användare
    Vill jag skriva mina krav på svenska
    Så att beställaren kan förstå dem

	Scenario: Ett scenario
        Givet en egenskap
        När den körs
        Så skall jag se att det inträffat";

            var configuration = new Configuration
                {
                    Language = "sv"
                };

            var parser = new FeatureParser(new LanguageServices(configuration));
            var feature = parser.Parse(new StringReader(featureText));

            Assert.AreNotEqual(null, feature);
            Assert.AreEqual("Test egenskap", feature.Name);
            Assert.AreEqual("  Som svensk användare\r\n  Vill jag skriva mina krav på svenska\r\n  Så att beställaren kan förstå dem", feature.Description);
            Assert.AreEqual(1, feature.FeatureElements.Count);
            Assert.AreEqual(0, feature.Tags.Count);

            var scenario = feature.FeatureElements.First();
            Assert.AreEqual("Ett scenario", scenario.Name);
            Assert.AreEqual(string.Empty, scenario.Description);
            Assert.AreEqual(3, scenario.Steps.Count);
            Assert.AreEqual(0, scenario.Tags.Count);

            var givenStep = scenario.Steps[0];
            Assert.AreEqual(Keyword.Given, givenStep.Keyword);
            Assert.AreEqual("en egenskap", givenStep.Name);
            Assert.AreEqual(null, givenStep.DocStringArgument);
            Assert.AreEqual(null, givenStep.TableArgument);

            var whenStep = scenario.Steps[1];
            Assert.AreEqual(Keyword.When, whenStep.Keyword);
            Assert.AreEqual("den körs", whenStep.Name);
            Assert.AreEqual(null, whenStep.DocStringArgument);
            Assert.AreEqual(null, whenStep.TableArgument);

            var thenStep = scenario.Steps[2];
            Assert.AreEqual(Keyword.Then, thenStep.Keyword);
            Assert.AreEqual("skall jag se att det inträffat", thenStep.Name);
            Assert.AreEqual(null, thenStep.DocStringArgument);
            Assert.AreEqual(null, thenStep.TableArgument);
        }
    }
}
