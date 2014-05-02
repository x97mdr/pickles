using System;
using System.Xml.Linq;
using Autofac;

using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;
using TechTalk.SpecFlow;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML.AutomationLayer
{
  public static class CurrentScenarioContext
  {
    public static Feature Feature
    {
      get
      {
        if (ScenarioContext.Current.ContainsKey("Feature"))
        {
          return ScenarioContext.Current["Feature"] as Feature;
        }
        else
        {
          return null;
        }
      }

      set
      {
        ScenarioContext.Current["Feature"] = value;
      }
    }

    public static IContainer Container { get; set; }

    public static XElement Html
    {
      get
      {
        if (ScenarioContext.Current.ContainsKey("Html"))
        {
          return ScenarioContext.Current["Html"] as XElement;
        }
        else
        {
          return null;
        }
      }

      set
      {
        ScenarioContext.Current["Html"] = value;
      }
    }
  }
}