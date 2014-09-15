using System;
using System.Collections.Generic;
using Autofac;
using NFluent;
using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;
using PicklesDoc.Pickles.Test.Extensions;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML
{
    [TestFixture]
    public class WhenFormattingScenarioOutlines : BaseFixture
    {
        [Test]
        public void ThenCanFormatCompleteScenarioOutlineCorrectly()
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
      
      var example = new Example { Name = "Some examples", Description = "An example", TableArgument = table };
      var examples = new List<Example>();
      examples.Add(example);

            var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Description = "We need to make sure that scenario outlines work properly",
                Examples = examples
            };

            var htmlScenarioOutlineFormatter = Container.Resolve<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            Check.That(output).ContainsGherkinScenario();
            Check.That(output).ContainsGherkinTable();
        }

        [Test]
        public void ThenCanFormatScenarioOutlineWithMissingNameCorrectly()
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

      var example = new Example { Name = "Some examples", Description = "An example", TableArgument = table };
      var examples = new List<Example>();
      examples.Add(example);

            var scenarioOutline = new ScenarioOutline
            {
                Description = "We need to make sure that scenario outlines work properly",
                Examples = examples
            };

            var htmlScenarioOutlineFormatter = Container.Resolve<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            Check.That(output).ContainsGherkinScenario();
            Check.That(output).ContainsGherkinTable();
        }

        [Test]
        public void ThenCanFormatScenarioOutlineWithMissingDescriptionCorrectly()
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

            var example = new Example { Name = "Some examples", Description = "An example", TableArgument = table };
      var examples = new List<Example>();
      examples.Add(example);

      var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Examples = examples
            };

            var htmlScenarioOutlineFormatter = Container.Resolve<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            Check.That(output).ContainsGherkinScenario();
            Check.That(output).ContainsGherkinTable();
        }

        [Test]
        public void ThenCanFormatScenarioOutlineWithMissingExampleCorrectly()
        {
            var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Description = "We need to make sure that scenario outlines work properly",
                Examples = new List<Example>()
            };

            var htmlScenarioOutlineFormatter = Container.Resolve<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            Check.That(output).ContainsGherkinScenario();
            Check.That(output).Not.ContainsGherkinTable();
        }

        [Test]
        public void ThenCanFormatScenarioOutlineWithMissingTableFromExampleCorrectly()
        {
            var example = new Example { Name = "Some examples", Description = "An example" };
      var examples = new List<Example>();
      examples.Add(example);

      var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Description = "We need to make sure that scenario outlines work properly",
                Examples = examples
            };

            var htmlScenarioOutlineFormatter = Container.Resolve<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            Check.That(output).ContainsGherkinScenario();
            Check.That(output).Not.ContainsGherkinTable();
        }
    }
}
