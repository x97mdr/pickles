using NUnit.Framework;
using Pickles.Extensions;

namespace Pickles.Test.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void IsNullOrWhiteSpace_NullArgument_ReturnsTrue()
        {
            string s = null;

            // ReSharper disable ExpressionIsAlwaysNull
            bool result = s.IsNullOrWhiteSpace();
            // ReSharper restore ExpressionIsAlwaysNull

            Assert.IsTrue(result);
        }

        [Test]
        public void IsNullOrWhiteSpace_ContentPresent_ReturnsFalse()
        {
            string s = "some text";

            bool result = s.IsNullOrWhiteSpace();

            Assert.IsFalse(result);
        }

        [Test]
        public void IsNullOrWhiteSpace_EmptyString_ReturnsTrue()
        {
            string s = "";

            bool result = s.IsNullOrWhiteSpace();

            Assert.IsTrue(result);
        }

        [Test]
        public void IsNullOrWhiteSpace_WhiteSpace_ReturnsTrue()
        {
            string s = "  ";

            bool result = s.IsNullOrWhiteSpace();

            Assert.IsTrue(result);
        }
    }
}