using System;
using System.Linq;
using NUnit.Framework;
using PicklesDoc.Pickles.Parser;
using Should;

using StringReader = System.IO.StringReader;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenParsingLocalizedFeatureFiles : BaseFixture
    {
        [Test]
        public void ThenCanParseMostBasicFeatureSuccessfully()
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

            var parser = new FeatureParser(new LanguageServices(configuration), FileSystem);
            Feature feature = parser.Parse(new StringReader(featureText));

            feature.ShouldNotBeNull();
            feature.Name.ShouldEqual("Test egenskap");
            feature.Description.ShouldEqual(@"  Som svensk användare
  Vill jag skriva mina krav på svenska
  Så att beställaren kan förstå dem");
            feature.FeatureElements.Count.ShouldEqual(1);
            feature.Tags.Count.ShouldEqual(0);

            IFeatureElement scenario = feature.FeatureElements.First();
            scenario.Name.ShouldEqual("Ett scenario");
            scenario.Description.ShouldEqual(string.Empty);
            scenario.Steps.Count.ShouldEqual(3);
            scenario.Tags.Count.ShouldEqual(0);

            Step givenStep = scenario.Steps[0];
            givenStep.Keyword.ShouldEqual(Keyword.Given);
            givenStep.Name.ShouldEqual("en egenskap");
            givenStep.DocStringArgument.ShouldBeNull();
            givenStep.TableArgument.ShouldBeNull();

            Step whenStep = scenario.Steps[1];
            Assert.AreEqual(Keyword.When, whenStep.Keyword);
            Assert.AreEqual("den körs", whenStep.Name);
            whenStep.DocStringArgument.ShouldBeNull();
            whenStep.TableArgument.ShouldBeNull();

            Step thenStep = scenario.Steps[2];
            thenStep.Keyword.ShouldEqual(Keyword.Then);
            thenStep.Name.ShouldEqual("skall jag se att det inträffat");
            thenStep.DocStringArgument.ShouldBeNull();
            thenStep.TableArgument.ShouldBeNull();
        }
    }
}