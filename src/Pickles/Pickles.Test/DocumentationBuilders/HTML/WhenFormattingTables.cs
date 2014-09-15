using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Autofac;
using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;
using NFluent;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML
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

            Check.That(output).IsNotNull();
            Check.That(output).HasAttribute("class", "table_container");
            Check.That(output).HasElement("table");

            var tableElement = output.Element(XName.Get("table", HtmlNamespace.Xhtml.NamespaceName));
            Check.That(tableElement).HasElement("thead");
            Check.That(tableElement).HasElement("tbody");
        }

        [Test]
        public void ThenCanFormatNullTableSuccessfully()
        {
            var htmlTableFormatter = Container.Resolve<HtmlTableFormatter>();
            var output = htmlTableFormatter.Format(null);

            Check.That(output).IsNull();
        }

    }
}
