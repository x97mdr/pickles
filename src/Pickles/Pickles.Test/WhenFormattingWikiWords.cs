using System;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;
using NFluent;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenFormattingWikiWords : BaseFixture
    {
        [Test]
        public void ThenCanFormatTextAsWikiWordSuccessfully()
        {
            string actual = "ThisIsTheWikiWord".ExpandWikiWord();
            Check.That(actual).IsEqualTo("This Is The Wiki Word");
        }

        [Test]
        public void ThenCanFormatTextWithAcronymAndNumberAsWikiWordSuccessfully()
        {
            string actual = "ThisIsAnACRONYM1".ExpandWikiWord();
            Check.That(actual).IsEqualTo("This Is An ACRONYM1");
        }

        [Test]
        public void ThenCanFormatTextWithAcronymAsWikiWordSuccessfully()
        {
            string actual = "ThisIsAnACRONYM".ExpandWikiWord();
            Check.That(actual).IsEqualTo("This Is An ACRONYM");
        }

        [Test]
        public void ThenCanFormatTextWithLongNumbersAsWikiWordSuccessfully()
        {
            string actual = "ThisIsThe5000thWikiWord".ExpandWikiWord();
            Check.That(actual).IsEqualTo("This Is The 5000th Wiki Word");
        }

        [Test]
        public void ThenCanFormatTextWithNumberFollowedByCapitalAsWikiWordSuccessfully()
        {
            string actual = "001FeatureOne".ExpandWikiWord();
            Check.That(actual).IsEqualTo("001 Feature One");
        }

        [Test]
        public void ThenCanFormatTextWithNumbersAsWikiWordSuccessfully()
        {
            string actual = "ThisIsThe4thWikiWord".ExpandWikiWord();
            Check.That(actual).IsEqualTo("This Is The 4th Wiki Word");
        }

        [Test]
        public void ThenCanFormatTextWithSpecialCharactersAsWikiWordSuccessfully()
        {
            string actual = "ThisIsThe_WikiWord".ExpandWikiWord();
            Check.That(actual).IsEqualTo("This Is The Wiki Word");
        }
    }
}