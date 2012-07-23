using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Should;

namespace Pickles.Test.Extensions
{
    public static class XElementExtensions
    {
        public static bool HasAttribute(this XElement element, string name, string value)
        {
            return element.Attribute(name) != null && element.Attribute(name).Value == value;
        }

        public static bool RecursiveSearch(this XElement element, Predicate<XElement> searchCriteria)
        {
            var meetsCriteria = searchCriteria(element);
            if (meetsCriteria) return true;

            return element.Elements().Any(child => child.RecursiveSearch(searchCriteria));
        }

        public static void ShouldContainGherkinTable(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "table_container")).ShouldBeTrue();
        }

        public static void ShouldNotContainGherkinTable(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "table_container")).ShouldBeFalse();
        }

        public static void ShouldContainGherkinDocString(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "pre")).ShouldBeTrue();
        }

        public static void ShouldNotContainGherkinDocString(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "pre")).ShouldBeFalse();
        }

        public static void ShouldContainGherkinSteps(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "step")).ShouldBeTrue();
        }

        public static void ShouldNotContainGherkinSteps(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "step")).ShouldBeFalse();
        }

        public static void ShouldContainGherkinScenario(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "scenario")).ShouldBeTrue();
        }

        public static void ShouldNotContainGherkinScenario(this XElement item)
        {
            item.RecursiveSearch(element => element.HasAttribute("class", "scenario")).ShouldBeFalse();
        }
    }
}
