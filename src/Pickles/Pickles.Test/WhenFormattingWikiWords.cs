using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pickles;
using NUnit.Framework;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenFormattingWikiWords : BaseFixture
    {
        [Test]
        public void Then_can_format_text_as_wiki_word_successfully()
        {
            string actual = "ThisIsAWikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is A Wiki Word", actual);
        }

        [Test]
        public void Then_can_format_text_with_numbers_as_wiki_word_successfully()
        {
            string actual = "ThisIsThe4thWikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is The 4th Wiki Word", actual);
        }

        [Test]
        public void Then_can_format_text_with_long_numbers_as_wiki_word_successfully()
        {
            string actual = "ThisIsThe5000thWikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is The 5000th Wiki Word", actual);
        }

        [Test]
        public void Then_can_format_text_with_special_characters_as_wiki_word_successfully()
        {
            string actual = "ThisIsThe_WikiWord".ExpandWikiWord();
            Assert.AreEqual("This Is The Wiki Word", actual);
        }
    }
}
