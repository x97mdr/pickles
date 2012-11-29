using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.TestFrameworks;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingTheSignatureOfAnNUnitExampleRow : BaseFixture
    {
        [Test]
        public void ThenCanSuccessfullyMatch()
        {
            var scenarioOutline = new ScenarioOutline {Name = "Adding several numbers"};
            var exampleRow = new[] {"40", "50", "90"};

            var signatureBuilder = Container.Resolve<NUnitExampleSignatureBuilder>();
            Regex signature = signatureBuilder.Build(scenarioOutline, exampleRow);

            signature.IsMatch("Pickles.TestHarness.AdditionFeature.AddingSeveralNumbers(\"40\",\"50\",\"90\",System.String[])".ToLowerInvariant()).ShouldBeTrue();
        }
    }
}