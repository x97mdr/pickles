using System;
using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.HTML;

namespace Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlContentFormatterTests : BaseFixture
    {
        [Test]
        public void Constructor_NullHtmlFeatureFormatter_ThrowsArgumentNullException()
        {
            ArgumentNullException exception =
                Assert.Throws<ArgumentNullException>(
                    () =>
                    new HtmlContentFormatter(null, null, null));

            Assert.AreEqual("htmlFeatureFormatter", exception.ParamName);
        }

        [Test]
        public void Constructor_NullHtmlIndexFormatter_ThrowsArgumentNullException()
        {
            ArgumentNullException exception =
                Assert.Throws<ArgumentNullException>(
                    () =>
                    new HtmlContentFormatter(
                        Kernel.Get<HtmlFeatureFormatter>(),
                        null,
                        null));

            Assert.AreEqual("htmlIndexFormatter", exception.ParamName);
        }
    }
}