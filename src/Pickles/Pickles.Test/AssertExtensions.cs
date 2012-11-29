using System;
using System.Linq;
using System.Xml.Linq;
using PicklesDoc.Pickles.Test.Extensions;
using Should;

namespace PicklesDoc.Pickles.Test
{
    public static class AssertExtensions
    {
        public static void ShouldHaveAttribute(this XElement element, string name, string value)
        {
            XAttribute xAttribute = element.Attributes().FirstOrDefault(attribute => attribute.Name.LocalName == name);
            xAttribute.ShouldNotBeNull();
            xAttribute.Value.ShouldEqual(value);
        }

        public static void ShouldHaveElement(this XElement element, string name)
        {
            element.HasElement(name).ShouldBeTrue();
        }

        public static void ShouldBeInInNamespace(this XElement element, string _namespace)
        {
            element.Name.NamespaceName.ShouldEqual(_namespace);
        }

        public static void ShouldBeNamed(this XElement element, string name)
        {
            element.Name.LocalName.ShouldEqual(name);
        }

        public static void ShouldDeepEquals(this XElement element, XElement other)
        {
            const string format = "Expected:\r\n{0}\r\nActual:\r\n{1}\r\n";
            XNode.DeepEquals(element, other).ShouldBeTrue(string.Format(format, element, other));
        }
    }
}