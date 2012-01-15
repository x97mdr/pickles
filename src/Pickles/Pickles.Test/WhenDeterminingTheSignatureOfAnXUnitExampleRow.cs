using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;
using Pickles.Parser;
using Pickles.TestFrameworks;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenDeterminingTheSignatureOfAnXUnitExampleRow : BaseFixture
    {
        [Test]
        public void ThenCanSuccessfullyMatch()
        {
            var scenarioOutline = new ScenarioOutline { Name = "Adding several numbers" };
            var exampleRow = new string[] { "40", "50", "90" };

            var signatureBuilder = Kernel.Get<xUnitExampleSignatureBuilder>();
            var signature = signatureBuilder.Build(scenarioOutline, exampleRow);

            signature.IsMatch("Pickles.TestHarness.xUnit.AdditionFeature.AddingSeveralNumbers(firstNumber: \"40\", secondNumber: \"50\", result: \"90\", exampleTags: System.String[])".ToLowerInvariant()).ShouldBeTrue();
        }
    }
}
