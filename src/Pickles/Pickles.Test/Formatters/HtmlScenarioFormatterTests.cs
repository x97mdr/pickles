using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using Ninject;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;

namespace Pickles.Test.Formatters
{
  [TestFixture]
  public class HtmlScenarioFormatterTests : BaseFixture
  {
    private HtmlScenarioFormatter formatter;

     [SetUp]
    public void Setup()
     {
       this.formatter = new HtmlScenarioFormatter(
         Kernel.Get<HtmlStepFormatter>(),
         Kernel.Get<HtmlDescriptionFormatter>(),
         Kernel.Get<HtmlImageResultFormatter>());
     }

     [Test]
     public void Li_Element_Must_Not_Have_Id_Attribute()
     {
       var scenario = BuildMinimalScenario();

       XElement li = formatter.Format(scenario, 1);

       var idAttribute = li.Attribute("id");

       Assert.IsNull(idAttribute);
     }

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
  }
}