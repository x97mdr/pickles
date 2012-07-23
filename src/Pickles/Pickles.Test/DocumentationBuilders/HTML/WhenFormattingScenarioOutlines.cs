using System.Collections.Generic;
using Ninject;
using NUnit.Framework;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;
using Pickles.Test.Extensions;

namespace Pickles.Test.DocumentationBuilders.HTML
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

            var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Description = "We need to make sure that scenario outlines work properly",
                Example = new Example { Name = "Some examples", Description = "An example", TableArgument = table }
            };

            var htmlScenarioOutlineFormatter = Kernel.Get<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            output.ShouldContainGherkinScenario();
            output.ShouldContainGherkinTable();
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

            var scenarioOutline = new ScenarioOutline
            {
                Description = "We need to make sure that scenario outlines work properly",
                Example = new Example { Name = "Some examples", Description = "An example", TableArgument = table }
            };

            var htmlScenarioOutlineFormatter = Kernel.Get<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            output.ShouldContainGherkinScenario();
            output.ShouldContainGherkinTable();
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

            var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Example = new Example { Name = "Some examples", Description = "An example", TableArgument = table }
            };

            var htmlScenarioOutlineFormatter = Kernel.Get<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            output.ShouldContainGherkinScenario();
            output.ShouldContainGherkinTable();
        }

        [Test]
        public void ThenCanFormatScenarioOutlineWithMissingExampleCorrectly()
        {
            var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Description = "We need to make sure that scenario outlines work properly",
                Example = null
            };

            var htmlScenarioOutlineFormatter = Kernel.Get<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            output.ShouldContainGherkinScenario();
            output.ShouldNotContainGherkinTable();
        }

        [Test]
        public void ThenCanFormatScenarioOutlineWithMissingTableFromExampleCorrectly()
        {
            var scenarioOutline = new ScenarioOutline
            {
                Name = "Testing a scenario outline",
                Description = "We need to make sure that scenario outlines work properly",
                Example = new Example { Name = "Some examples", Description = "An example" }
            };

            var htmlScenarioOutlineFormatter = Kernel.Get<HtmlScenarioOutlineFormatter>();
            var output = htmlScenarioOutlineFormatter.Format(scenarioOutline, 0);

            output.ShouldContainGherkinScenario();
            output.ShouldNotContainGherkinTable();
        }
    }
}
