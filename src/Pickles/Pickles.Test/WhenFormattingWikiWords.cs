using NUnit.Framework;
using Pickles.Extensions;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenFormattingWikiWords : BaseFixture
    {
        [Test]
        public void Then_can_format_text_as_wiki_word_successfully()
        {
            string actual = "ThisIsTheWikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is The Wiki Word", actual);
        }

        [Test]
        public void Then_can_format_text_with_acronym_and_number_as_wiki_word_successfully()
        {
            string actual = "ThisIsAnACRONYM1".ExpandWikiWord();
            Assert.AreEqual("This Is An ACRONYM1", actual);
        }

        [Test]
        public void Then_can_format_text_with_acronym_as_wiki_word_successfully()
        {
            string actual = "ThisIsAnACRONYM".ExpandWikiWord();
            Assert.AreEqual("This Is An ACRONYM", actual);
        }

        [Test]
        public void Then_can_format_text_with_long_numbers_as_wiki_word_successfully()
        {
            string actual = "ThisIsThe5000thWikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is The 5000th Wiki Word", actual);
        }

        [Test]
        public void Then_can_format_text_with_number_followed_by_capital_as_wiki_word_successfully()
        {
            string actual = "001FeatureOne".ExpandWikiWord();
            Assert.AreEqual("001 Feature One", actual);
        }

        [Test]
        public void Then_can_format_text_with_numbers_as_wiki_word_successfully()
        {
            string actual = "ThisIsThe4thWikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is The 4th Wiki Word", actual);
        }

        [Test]
        public void Then_can_format_text_with_special_characters_as_wiki_word_successfully()
        {
            string actual = "ThisIsThe_WikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is The Wiki Word", actual);
        }
    }
}