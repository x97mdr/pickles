using System;
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.Test.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        public void IsNullOrWhiteSpace_ContentPresent_ReturnsFalse()
        {
            string s = "some text";

            bool result = s.IsNullOrWhiteSpace();

            Check.That(result).IsFalse();
        }

        [Test]
        public void IsNullOrWhiteSpace_EmptyString_ReturnsTrue()
        {
            string s = "";

            bool result = s.IsNullOrWhiteSpace();

            Check.That(result).IsTrue();
        }

        [Test]
        public void IsNullOrWhiteSpace_NullArgument_ReturnsTrue()
        {
            string s = null;

            bool result = s.IsNullOrWhiteSpace();

            Check.That(result).IsTrue();
        }

        [Test]
        public void IsNullOrWhiteSpace_WhiteSpace_ReturnsTrue()
        {
            string s = "  ";

            bool result = s.IsNullOrWhiteSpace();

            Check.That(result).IsTrue();
        }
    }
}