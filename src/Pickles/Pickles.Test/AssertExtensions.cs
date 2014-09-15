using System;
using System.Linq;
using System.Xml.Linq;
using NFluent;
using NFluent.Extensibility;
using NUnit.Framework;
using PicklesDoc.Pickles.Test.Extensions;

namespace PicklesDoc.Pickles.Test
{
    public static class AssertExtensions
    {
        public static void HasAttribute(this ICheck<XElement> check, string name, string value)
        {
            var actual = ExtensibilityHelper.ExtractChecker(check).Value;

            ShouldHaveAttribute(actual, name, value);
        }

        private static void ShouldHaveAttribute(this XElement element, string name, string value)
        {
            XAttribute xAttribute = element.Attributes().FirstOrDefault(attribute => attribute.Name.LocalName == name);
            Check.That(xAttribute).IsNotNull();
            // ReSharper disable once PossibleNullReferenceException
            Check.That(xAttribute.Value).IsEqualTo(value);
        }

        public static void HasElement(this ICheck<XElement> check, string name)
        {
          var actual = ExtensibilityHelper.ExtractChecker(check).Value;

          ShouldHaveElement(actual, name);
        }

        private static void ShouldHaveElement(this XElement element, string name)
        {
            Check.That(element.HasElement(name)).IsTrue();
        }

        public static void IsInNamespace(this ICheck<XElement> check, string nameOfNamespace)
        {
          var actual = ExtensibilityHelper.ExtractChecker(check).Value;

          ShouldBeInNamespace(actual, nameOfNamespace);
        }

        private static void ShouldBeInNamespace(this XElement element, string nameOfNamespace)
        {
            Check.That(element.Name.NamespaceName).IsEqualTo(nameOfNamespace);
        }

        public static void IsNamed(this ICheck<XElement> check, string name)
        {
          var actual = ExtensibilityHelper.ExtractChecker(check).Value;

          ShouldBeNamed(actual, name);
        }

        private static void ShouldBeNamed(this XElement element, string name)
        {
          Check.That(element.Name.LocalName).IsEqualTo(name);
        }

        public static void IsDeeplyEqualTo(this ICheck<XElement> check, XElement actual)
        {
          var element = ExtensibilityHelper.ExtractChecker(check).Value;

          if (!XNode.DeepEquals(element, actual))
          {
            var fluentMessage = FluentMessage.BuildMessage("The {0} is not equal to the given one (using deep comparison)").For("XML element").On(element.ToString()).And.WithGivenValue(actual.ToString());

            throw new FluentCheckException(fluentMessage.ToString());
          }
        }
    }
}