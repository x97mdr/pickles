using System;

using NUnit.Framework;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.TestFrameworks;

using NFluent;

namespace PicklesDoc.Pickles.Test.TestFrameworks
{
    [TestFixture]
    public class WhenParsingxUnit2ResultsFile : WhenParsingTestResultFiles<XUnitResults>
    {
        public WhenParsingxUnit2ResultsFile()
            : base("results-example-xunit2.xml")
        {
        }

        [Test]
        public void ThenCannotReadFeatureResult()
        {
            // Write out the embedded test results file
            var results = ParseResultsFile();

            var feature = new Feature { Name = "Addition" };

            Check.ThatCode(() => results.GetFeatureResult(feature)).Throws<Exception>();
        }
    }
  }