namespace Pickles.Test
{
    using NUnit.Framework;

    using Pickles.Parser;

    [TestFixture]
    public class LanguageServicesTests
    {
        [Test]
        public void Should_return_Givet_for_Given_using_sv_Language()
        {
            var languageService = new LanguageServices(new Configuration { Language = "sv" });

            Assert.AreEqual("Givet ", languageService.GetKeywordString(Keyword.Given));
        }
    }
}
