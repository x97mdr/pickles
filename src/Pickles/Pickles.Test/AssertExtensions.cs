using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Xml.Linq;

namespace Pickles.Test
{
    public static class AssertExtensions
    {
        public static void AssertHasAttribute(this XElement element, string name, string value)
        {
            var classAttribute = element.Attributes().FirstOrDefault(attribute => attribute.Name.LocalName == name);
            Assert.IsNotNull(classAttribute);
            Assert.AreEqual(value, classAttribute.Value);
        }

        public static void AssertIsInNamespace(this XElement element, string _namespace)
        {
            Assert.AreEqual(_namespace, element.Name.NamespaceName);
        }

        public static void AssertIsNamed(this XElement element, string name)
        {
            Assert.AreEqual(name, element.Name.LocalName);
        }
    }
}
