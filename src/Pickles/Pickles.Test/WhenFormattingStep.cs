using System.Xml.Linq;
using NUnit.Framework;
using Pickles.Formatters;
using TechTalk.SpecFlow.Parser.SyntaxElements;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenFormattingStep
    {
        [Test]
        public void Simple_steps_are_formatted_as_list_items()
        {
            var step = new ScenarioStep
            {
                Keyword = "Given",
                StepKeyword = TechTalk.SpecFlow.Parser.Gherkin.StepKeyword.Given,
                TableArg = null,
                MultiLineTextArgument = null,
                ScenarioBlock = TechTalk.SpecFlow.Parser.Gherkin.ScenarioBlock.Given,
                Text = "a simple step"
            };

            var formatter = new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter());
            var actual = formatter.Format(step);

            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var expected = new XElement(xmlns + "li",
                               new XAttribute("class", "step"),
                               new XElement(xmlns + "span", new XAttribute("class", "keyword"), "Given"),
                               "a simple step"
                           );

            Assert.IsTrue(XElement.DeepEquals(expected, actual));
        }

        [Test]
        public void Tables_are_formatted_as_list_items_with_tables_internal()
        {
            var table = new GherkinTable
            {
                Header = new GherkinTableRow(new GherkinTableCell("Column 1"), new GherkinTableCell("Column 2")),
                Body = new GherkinTableRow[] { new GherkinTableRow(new GherkinTableCell("Value 1"), new GherkinTableCell("Value 2")) } 
            };

            var step = new ScenarioStep
            {
                Keyword = "Given",
                StepKeyword = TechTalk.SpecFlow.Parser.Gherkin.StepKeyword.Given,
                TableArg = table,
                MultiLineTextArgument = null,
                ScenarioBlock = TechTalk.SpecFlow.Parser.Gherkin.ScenarioBlock.Given,
                Text = "a simple step"
            };

            var formatter = new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter());
            var actual = formatter.Format(step);

            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var expected = new XElement(xmlns + "li",
                               new XAttribute("class", "step"),
                               new XElement(xmlns + "span", new XAttribute("class", "keyword"), "Given"),
                               new XText("a simple step"),
                               new XElement(xmlns + "table",
                                   new XElement(xmlns + "thead",
                                       new XElement(xmlns + "tr",
                                           new XElement(xmlns + "th", "Column 1"),
                                           new XElement(xmlns + "th", "Column 2")
                                        )
                                   ),
                                   new XElement(xmlns + "tbody",
                                       new XElement(xmlns + "tr",
                                           new XElement(xmlns + "td", "Value 1"),
                                           new XElement(xmlns + "td", "Value 2")
                                       )
                                   )
                               )
                           );

            Assert.IsTrue(XElement.DeepEquals(expected, actual));
        }

        [Test]
        public void Multiline_strings_are_formatted_as_list_items_with_pre_elements_formatted_as_code_internal()
        {
            var step = new ScenarioStep
            {
                Keyword = "Given",
                StepKeyword = TechTalk.SpecFlow.Parser.Gherkin.StepKeyword.Given,
                TableArg = null,
                MultiLineTextArgument = "this is a\nmultiline table\nargument",
                ScenarioBlock = TechTalk.SpecFlow.Parser.Gherkin.ScenarioBlock.Given,
                Text = "a simple step"
            };

            var formatter = new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter());
            var actual = formatter.Format(step);

            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var expected = new XElement(xmlns + "li",
                               new XAttribute("class", "step"),
                               new XElement(xmlns + "span", new XAttribute("class", "keyword"), "Given"),
                               new XText("a simple step"),
                               new XElement(xmlns + "div",
                                   new XAttribute("class", "pre"),
                                   new XElement(xmlns + "pre",
                                       new XElement(xmlns + "code",
                                           new XAttribute("class", "no-highlight"),
                                           new XText("this is a\nmultiline table\nargument")
                                        )
                                   )
                               )
                           );

            Assert.IsTrue(XElement.DeepEquals(expected, actual));
        }

    }
}
