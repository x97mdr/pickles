using System;
using System.Text.RegularExpressions;

using Autofac;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

using Should;

namespace PicklesDoc.Pickles.Test.TestFrameworks
{
    [TestFixture]
    public class WhenDeterminingTheSignatureOfAnXUnitExampleRow : BaseFixture
    {
        [Test]
        public void ThenCanSuccessfullyMatch()
        {
            var scenarioOutline = new ScenarioOutline {Name = "Adding several numbers"};
            var exampleRow = new[] {"40", "50", "90"};

            var signatureBuilder = Container.Resolve<xUnitExampleSignatureBuilder>();
            Regex signature = signatureBuilder.Build(scenarioOutline, exampleRow);

            signature.IsMatch("Pickles.TestHarness.xUnit.AdditionFeature.AddingSeveralNumbers(firstNumber: \"40\", secondNumber: \"50\", result: \"90\", exampleTags: System.String[])".ToLowerInvariant()).ShouldBeTrue();
        }
    }
}