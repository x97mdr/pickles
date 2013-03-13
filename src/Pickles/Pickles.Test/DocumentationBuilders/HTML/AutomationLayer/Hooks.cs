using System;
using Autofac;
using TechTalk.SpecFlow;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML.AutomationLayer
{
  [Binding]
  public class Hooks
  {
    [BeforeScenario]
    public void BeforeScenario()
    {
      var builder = new ContainerBuilder();
      builder.RegisterAssemblyTypes(typeof(Runner).Assembly);
      builder.RegisterModule<PicklesModule>();
      CurrentScenarioContext.Container = builder.Build();
    }

    [AfterScenario]
    public void AfterScenario()
    {
      var container = CurrentScenarioContext.Container;

      if (container != null)
      {
        container.Dispose();
      }

      CurrentScenarioContext.Container = null;
    }
  }
}
