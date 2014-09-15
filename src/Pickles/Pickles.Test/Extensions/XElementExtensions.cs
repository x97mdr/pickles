using System;
using System.Linq;
using System.Xml.Linq;
using NFluent;

namespace PicklesDoc.Pickles.Test.Extensions
{
    public static class XElementExtensions
    {
        public static bool HasAttribute(this XElement element, string name, string value)
        {
            return element.Attribute(name) != null && element.Attribute(name).Value == value;
        }

        public static bool HasElement(this XElement element, string name)
        {
            return element.Elements().Any(e => e.Name.LocalName == name);
        }

        public static bool RecursiveSearch(this XElement element, Predicate<XElement> searchCriteria)
        {
            var meetsCriteria = searchCriteria(element);
            return meetsCriteria || element.Elements().Any(child => child.RecursiveSearch(searchCriteria));
        }

        public static void ShouldContainGherkinTable(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "table_container"))).IsTrue();
        }

        public static void ShouldNotContainGherkinTable(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "table_container"))).IsFalse();
        }

        public static void ShouldContainGherkinDocString(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "pre"))).IsTrue();
        }

        public static void ShouldNotContainGherkinDocString(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "pre"))).IsFalse();
        }

        public static void ShouldContainGherkinSteps(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "step"))).IsTrue();
        }

        public static void ShouldNotContainGherkinSteps(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "step"))).IsFalse();
        }

        public static void ShouldContainGherkinScenario(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "scenario"))).IsTrue();
        }

        public static void ShouldNotContainGherkinScenario(this XElement item)
        {
            Check.That(item.RecursiveSearch(element => element.HasAttribute("class", "scenario"))).IsFalse();
        }
    }
}
