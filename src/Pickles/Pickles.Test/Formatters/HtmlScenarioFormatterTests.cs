using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NFluent;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.Test.Formatters
{
    [TestFixture]
    public class HtmlScenarioFormatterTests : BaseFixture
    {
        #region Setup/Teardown

        [SetUp]
        public void Setup()
        {
            this.formatter = new HtmlScenarioFormatter(
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
            Scenario scenario = this.BuildMinimalScenario();

            XElement li = this.formatter.Format(scenario, 1);

            XAttribute idAttribute = li.Attribute("id");

            Check.That(idAttribute).IsNull();
        }
    }
}