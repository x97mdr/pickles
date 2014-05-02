using System;
using System.Xml;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;
using Should;
using TechTalk.SpecFlow;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML.AutomationLayer
{
  [Binding]
  public class StepDefinitions
  {
    [Given(@"I have this feature description")]
    public void GivenIHaveThisFeatureDescription(string multilineText)
    {
      CurrentScenarioContext.Feature = new Feature
      {
        Name = "a feature",
        Description = multilineText
      };
    }

    [Given(@"I have this feature file")]
    public void GivenIHaveThisFeatureFile(string multilineText)
    {
      CurrentScenarioContext.Feature = new Feature
        {
          Name = "a feature",
          Description = multilineText
        };
    }

    [When(@"I generate the documentation")]
    public void WhenIGenerateTheDocumentation()
    {
      var configuration = CurrentScenarioContext.Container.Resolve<Configuration>();
      configuration.TestResultsFiles = null;
      var htmlFeatureFormatter = CurrentScenarioContext.Container.Resolve<HtmlFeatureFormatter>();

      CurrentScenarioContext.Html = htmlFeatureFormatter.Format(CurrentScenarioContext.Feature);
    }

    [Then(@"the result should be")]
    public void ThenTheResultShouldBe(string multilineText)
    {
      var actual = CurrentScenarioContext.Html.ToString();
      actual = actual.Replace(" xmlns=\"http://www.w3.org/1999/xhtml\"", string.Empty);

      actual = FormatXml(actual);
      multilineText = FormatXml(multilineText);

      actual.ShouldEqual(multilineText);
    }


    private static string FormatXml(string xmlDocument)
    {
      using (var sw = new System.IO.StringWriter())
      {
        using (XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true, NewLineHandling = NewLineHandling.Replace, NewLineChars = Environment.NewLine }))
        {
          xw.WriteRaw(xmlDocument);
          xw.Flush();

          return sw.ToString();
        }
      }
    }
  }
}
