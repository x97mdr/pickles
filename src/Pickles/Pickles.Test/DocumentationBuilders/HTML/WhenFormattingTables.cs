using System.Collections.Generic;
using System.Xml.Linq;
using Autofac;
using NUnit.Framework;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders.HTML
{
    [TestFixture]
    public class WhenFormattingTables : BaseFixture
    {
        [Test]
        public void ThenCanFormatNormalTableSuccessfully()
        {
            var table = new Table
            {
                HeaderRow = new TableRow("Var1", "Var2", "Var3", "Var4"),
                DataRows =
                    new List<TableRow>(new[]
                                            {
                                                new TableRow("1", "2", "3", "4"),
                                                new TableRow("5", "6", "7", "8")
                                            })
            };

            var htmlTableFormatter = Container.Resolve<HtmlTableFormatter>();

            var output = htmlTableFormatter.Format(table);

            output.ShouldNotBeNull();
            output.ShouldHaveAttribute("class", "table_container");
            output.ShouldHaveElement("table");

            var tableElement = output.Element(XName.Get("table", HtmlNamespace.Xhtml.NamespaceName));
            tableElement.ShouldHaveElement("thead");
            tableElement.ShouldHaveElement("tbody");
        }

        [Test]
        public void ThenCanFormatNullTableSuccessfully()
        {
            var htmlTableFormatter = Container.Resolve<HtmlTableFormatter>();
            var output = htmlTableFormatter.Format(null);

            output.ShouldBeNull();
        }

    }
}
