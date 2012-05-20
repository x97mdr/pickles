using System.Text.RegularExpressions;
using NUnit.Framework;
using Ninject;
using Pickles.Parser;
using Pickles.TestFrameworks;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingTheSignatureOfAnNUnitExampleRow : BaseFixture
    {
        [Test]
        public void ThenCanSuccessfullyMatch()
        {
            var scenarioOutline = new ScenarioOutline {Name = "Adding several numbers"};
            var exampleRow = new[] {"40", "50", "90"};

            var signatureBuilder = Kernel.Get<NUnitExampleSignatureBuilder>();
            Regex signature = signatureBuilder.Build(scenarioOutline, exampleRow);

            signature.IsMatch(
                "Pickles.TestHarness.AdditionFeature.AddingSeveralNumbers(\"40\",\"50\",\"90\",System.String[])".
                    ToLowerInvariant()).ShouldBeTrue();
        }
    }
}