using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;

namespace PicklesDoc.Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlScenarioOutlineFormatterTests : BaseFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
          this.formatter = new HtmlScenarioOutlineFormatter(
            Container.Resolve<HtmlStepFormatter>(),
            Container.Resolve<HtmlDescriptionFormatter>(),
            Container.Resolve<HtmlTableFormatter>(),
            Container.Resolve<HtmlImageResultFormatter>(),
            Container.Resolve<Configuration>());
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

            XElement li = this.formatter.Format(scenarioOutline, 1);

            XAttribute idAttribute = li.Attribute("id");

            Assert.IsNull(idAttribute);
        }
    }
}