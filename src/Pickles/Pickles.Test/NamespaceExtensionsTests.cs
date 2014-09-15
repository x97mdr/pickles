using System;
using System.Xml.Linq;
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class NamespaceExtensionsTests
    {
        private static readonly XNamespace newNamespace = XNamespace.Get("http://myNewNamespace/");

        [Test]
        public void MoveNamespace()
        {
            var tree1 = new XElement(
                "Data",
                new XElement(
                    "Child",
                    "content",
                    new XAttribute("MyAttr", "content")));

            tree1.MoveToNamespace(newNamespace);

            foreach (XElement node in tree1.DescendantsAndSelf())
            {
                Check.That(node).IsInNamespace(newNamespace.NamespaceName);
            }
        }
    }
}