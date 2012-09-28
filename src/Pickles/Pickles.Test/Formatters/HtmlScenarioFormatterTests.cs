using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using Autofac;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;

namespace Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlScenarioFormatterTests : BaseFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            formatter = new HtmlScenarioFormatter(
                Container.Resolve<HtmlStepFormatter>(),
                Container.Resolve<HtmlDescriptionFormatter>(),
                Container.Resolve<HtmlImageResultFormatter>());
        }

        #endregion

        private HtmlScenarioFormatter formatter;

        private Scenario BuildMinimalScenario()
        {
            return new Scenario
                       {
                           Description = "My Scenario Description",
                           Steps = new List<Step>
                                       {
                                           new Step
                                               {
                                                   NativeKeyword = "Given",
                                                   Name = "My Step Name",
                                               }
                                       }
                       };
        }

        [Test]
        public void Li_Element_Must_Not_Have_Id_Attribute()
        {
            Scenario scenario = BuildMinimalScenario();

            XElement li = formatter.Format(scenario, 1);

            XAttribute idAttribute = li.Attribute("id");

            Assert.IsNull(idAttribute);
        }
    }
}