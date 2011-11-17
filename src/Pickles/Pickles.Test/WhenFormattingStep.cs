using System.Collections.Generic;
using System.Xml.Linq;
using Ninject;
using NUnit.Framework;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenFormattingStep : BaseFixture
    {
        private const string EXPECTED_GIVEN_HTML = "Given ";

        [Test]
        public void Steps_get_selected_Language()
        {
            var step = new Step
                {
                    Keyword = Keyword.Given, 
                    Name = "ett enkelt steg", 
                    NativeKeyword = "Givet ",
                    TableArgument = null, 
                    DocStringArgument = null,
                };
            var configuration = Kernel.Get<Configuration>();
            configuration.Language = "sv";

            var formatter = Kernel.Get<HtmlStepFormatter>();
            var actual = formatter.Format(step);

            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var expected = new XElement(
                               xmlns + "li",
                               new XAttribute("class", "step"),
                               new XElement(xmlns + "span", new XAttribute("class", "keyword"), "Givet "),
                               "ett enkelt steg");

            Assert.IsTrue(XNode.DeepEquals(expected, actual), string.Format("Expected:\r\n{0}\r\nActual:\r\n{1}\r\n", expected, actual));
        }

        [Test]
        public void Simple_steps_are_formatted_as_list_items()
        {
            var step = new Step
            {
                Keyword = Keyword.Given,
                Name = "a simple step",
                NativeKeyword = "Given ",
                TableArgument = null,
                DocStringArgument = null,
            };

            var formatter = Kernel.Get<HtmlStepFormatter>();
            var actual = formatter.Format(step);

            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var expected = new XElement(xmlns + "li",
                               new XAttribute("class", "step"),
                               new XElement(xmlns + "span", new XAttribute("class", "keyword"), EXPECTED_GIVEN_HTML),
                               "a simple step"
                           );

            Assert.IsTrue(XElement.DeepEquals(expected, actual));
        }

        [Test]
        public void Tables_are_formatted_as_list_items_with_tables_internal()
        {
            var table = new Table
            {
                HeaderRow = new TableRow("Column 1", "Column 2"),
                DataRows = new List<TableRow> { new TableRow("Value 1", "Value 2") } 
            };

            var step = new Step
            {
                Keyword = Keyword.Given,
                Name = "a simple step",
                TableArgument = table,
                DocStringArgument = null,
            };

            var formatter = Kernel.Get<HtmlStepFormatter>();
            var actual = formatter.Format(step);

            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var expected = new XElement(xmlns + "li",
                               new XAttribute("class", "step"),
                               new XElement(xmlns + "span", new XAttribute("class", "keyword"), EXPECTED_GIVEN_HTML),
                               new XText("a simple step"),
                               new XElement(xmlns + "div",
                                   new XAttribute("class", "table_container"),
                                   new XElement(xmlns + "table",
                                       new XAttribute("class", "datatable"),
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
                               )
                           );

            Assert.IsTrue(XElement.DeepEquals(expected, actual));
        }

        [Test]
        public void Multiline_strings_are_formatted_as_list_items_with_pre_elements_formatted_as_code_internal()
        {
            var step = new Step
            {
                Keyword = Keyword.Given,
                Name = "a simple step",
                TableArgument = null,
                DocStringArgument = "this is a\nmultiline table\nargument",
            };

            var formatter = Kernel.Get<HtmlStepFormatter>();
            var actual = formatter.Format(step);

            var xmlns = XNamespace.Get("http://www.w3.org/1999/xhtml");
            var expected = new XElement(xmlns + "li",
                               new XAttribute("class", "step"),
                               new XElement(xmlns + "span", new XAttribute("class", "keyword"), EXPECTED_GIVEN_HTML),
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
