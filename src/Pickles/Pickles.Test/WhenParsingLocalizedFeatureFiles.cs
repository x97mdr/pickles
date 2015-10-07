using System;
using System.Linq;
using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using NFluent;
using PicklesDoc.Pickles.Extensions;
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
                @"# language: sv
# ignorera denna kommentar
Egenskap: Test egenskap
  Som svensk användare
  Vill jag skriva mina krav på svenska
  Så att beställaren kan förstå dem

  Scenario: Ett scenario
        Givet en egenskap
        När den körs
        Så skall jag se att det inträffat";

            var configuration = new Configuration() {
                                        Language = "sv"
                                    };

            var parser = new FeatureParser(FileSystem);
            Feature feature = parser.Parse(new StringReader(featureText));

            Check.That(feature).IsNotNull();
            Check.That(feature.Name).IsEqualTo("Test egenskap");
            Check.That(feature.Description.ComparisonNormalize()).IsEqualTo(@"  Som svensk användare
  Vill jag skriva mina krav på svenska
  Så att beställaren kan förstå dem".ComparisonNormalize());
            Check.That(feature.FeatureElements.Count).IsEqualTo(1);
            Check.That(feature.Tags.Count).IsEqualTo(0);

            IFeatureElement scenario = feature.FeatureElements.First();
            Check.That(scenario.Name).IsEqualTo("Ett scenario");
            Check.That(scenario.Description).IsEqualTo(string.Empty);
            Check.That(scenario.Steps.Count).IsEqualTo(3);
            Check.That(scenario.Tags.Count).IsEqualTo(0);

            Step givenStep = scenario.Steps[0];
            Check.That(givenStep.Keyword).IsEqualTo(Keyword.Given);
            Check.That(givenStep.Name).IsEqualTo("en egenskap");
            Check.That(givenStep.DocStringArgument).IsNull();
            Check.That(givenStep.TableArgument).IsNull();

            Step whenStep = scenario.Steps[1];
            Check.That(whenStep.Keyword).IsEqualTo(Keyword.When);
            Check.That(whenStep.Name).IsEqualTo("den körs");
            Check.That(whenStep.DocStringArgument).IsNull();
            Check.That(whenStep.TableArgument).IsNull();

            Step thenStep = scenario.Steps[2];
            Check.That(thenStep.Keyword).IsEqualTo(Keyword.Then);
            Check.That(thenStep.Name).IsEqualTo("skall jag se att det inträffat");
            Check.That(thenStep.DocStringArgument).IsNull();
            Check.That(thenStep.TableArgument).IsNull();
        }
    }
}