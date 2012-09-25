using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;

namespace Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlScenarioOutlineFormatterTests : BaseFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            formatter = new HtmlScenarioOutlineFormatter(Kernel.Get<HtmlStepFormatter>(),
                                                         Kernel.Get<HtmlDescriptionFormatter>(),
                                                         Kernel.Get<HtmlTableFormatter>(),
                                                         Kernel.Get<HtmlImageResultFormatter>());
        }

        #endregion

        private HtmlScenarioOutlineFormatter formatter;

        private static ScenarioOutline BuildMinimalScenarioOutline()
        {
			var examples = new List<Example>();
			examples.Add(new Example
                                                    {
                                                        Description = "My Example Description",
                                                        TableArgument = new Table
                                                                            {
                                                                                HeaderRow = new TableRow("Cell1"),
                                                                                DataRows =
                                                                                    new List<TableRow>(new[]
                                                                                                           {
                                                                                                               new TableRow
                                                                                                                   ("Value1")
                                                                                                           })
                                                                            },
                                                    });
            var scenarioOutline = new ScenarioOutline
                                      {
                                          Description = "My Outline Description",
                                          Examples = examples,
                                          Steps = new List<Step>
                                                      {
                                                          new Step
                                                              {
                                                                  NativeKeyword = "Given",
                                                                  Name = "My Step Name",
                                                                  TableArgument = new Table
                                                                                      {
                                                                                          HeaderRow =
                                                                                              new TableRow("Cell1"),
                                                                                          DataRows =
                                                                                              new List<TableRow>(new[]
                                                                                                                     {
                                                                                                                         new TableRow
                                                                                                                             ("Value1")
                                                                                                                     })
                                                                                      },
                                                              }
                                                      }
                                      };
            return scenarioOutline;
        }

        [Test]
        public void Li_Element_Must_Not_Have_Id_Attribute()
        {
            ScenarioOutline scenarioOutline = BuildMinimalScenarioOutline();

            XElement li = formatter.Format(scenarioOutline, 1);

            XAttribute idAttribute = li.Attribute("id");

            Assert.IsNull(idAttribute);
        }
    }
}