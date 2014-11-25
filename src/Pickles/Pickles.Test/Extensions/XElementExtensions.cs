using System;
using System.Linq;
using System.Xml.Linq;
using NFluent;
using NFluent.Extensibility;

namespace PicklesDoc.Pickles.Test.Extensions
{
    public static class XElementExtensions
    {
        private static bool HasAttribute(this XElement element, string name, string value)
        {
            return element.Attribute(name) != null && element.Attribute(name).Value == value;
        }

        private static bool RecursiveSearch(this XElement element, Predicate<XElement> searchCriteria)
        {
            var meetsCriteria = searchCriteria(element);
            return meetsCriteria || element.Elements().Any(child => child.RecursiveSearch(searchCriteria));
        }

        public static ICheckLink<ICheck<XElement>> ContainsGherkinTable(this ICheck<XElement> check)
        {
          var checker = ExtensibilityHelper.ExtractChecker(check);

          return checker.ExecuteCheck(
            () =>
            {
              if (!checker.Value.RecursiveSearch(element => element.HasAttribute("class", "table_container")))
              {
                var errorMessage = FluentMessage.BuildMessage("The {0} does not contain a gherkin table (marked by the presence of a class attribute with value 'table_container')").For("XML element").On(checker.Value).ToString();
                throw new FluentCheckException(errorMessage);
              }
            },
            FluentMessage.BuildMessage("The {0} contains a gherkin table (marked by the presence of a class attribute with value 'table_container'), whereas it must not.").For("XML element").On(checker.Value).ToString());
        }

        public static ICheckLink<ICheck<XElement>> ContainsGherkinScenario(this ICheck<XElement> check)
        {
          var checker = ExtensibilityHelper.ExtractChecker(check);

          return checker.ExecuteCheck(
            () =>
            {
              if (!checker.Value.RecursiveSearch(element => element.HasAttribute("class", "scenario")))
              {
                var errorMessage = FluentMessage.BuildMessage("The {0} does not contain a gherkin scenario (marked by the presence of a class attribute with value 'scenario')").For("XML element").On(checker.Value).ToString();
                throw new FluentCheckException(errorMessage);
              }
            },
            FluentMessage.BuildMessage("The {0} contains a gherkin scenario (marked by the presence of a class attribute with value 'scenario'), whereas it must not.").For("XML element").On(checker.Value).ToString());
        }
    }
}
