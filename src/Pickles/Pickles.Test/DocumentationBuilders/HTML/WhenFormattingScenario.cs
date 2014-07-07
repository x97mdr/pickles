using System;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Autofac;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using PicklesDoc.Pickles.ObjectModel;
using PicklesDoc.Pickles.Parser;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML
{
    [TestFixture]
    public class WhenFormattingScenario : BaseFixture
    {
        [Test]
        public void ThenCanRenderTags()
        {
            var configuration = Container.Resolve<Configuration>();

            var scenario = new Scenario
                              {
                                  Name = "A Scenario",
                                  Description = @"This scenario has tags",
                                  Tags = { "tag1", "tag2" }
                              };

            var htmlFeatureFormatter = Container.Resolve<HtmlScenarioFormatter>();
            XElement featureElement = htmlFeatureFormatter.Format(scenario, 1);
            XElement header = featureElement.Elements().FirstOrDefault(element => element.Name.LocalName == "div");

            Assert.NotNull(header);
            header.ShouldBeNamed("div");
            header.ShouldBeInInNamespace("http://www.w3.org/1999/xhtml");
            header.ShouldHaveAttribute("class", "scenario-heading");
            Assert.AreEqual(3, header.Elements().Count());

            header.Elements().ElementAt(0).ShouldBeNamed("h2");
            header.Elements().ElementAt(1).ShouldBeNamed("p");
            header.Elements().ElementAt(2).ShouldBeNamed("div");

            var tagsParagraph = header.Elements().ElementAt(1);

            Assert.AreEqual(
              @"<p class=""tags"" xmlns=""http://www.w3.org/1999/xhtml"">Tags: <span>tag1</span>, <span>tag2</span></p>",
              tagsParagraph.ToString());
        }

        [Test]
        public void NoTags()
        {
          var configuration = Container.Resolve<Configuration>();

          var scenario = new Scenario
          {
            Name = "A Scenario",
            Description = @"This scenario has no tags",
            Tags = { }
          };

          var htmlFeatureFormatter = Container.Resolve<HtmlScenarioFormatter>();
          XElement featureElement = htmlFeatureFormatter.Format(scenario, 1);
          XElement header = featureElement.Elements().FirstOrDefault(element => element.Name.LocalName == "div");

          Assert.NotNull(header);
          header.ShouldBeNamed("div");
          header.ShouldBeInInNamespace("http://www.w3.org/1999/xhtml");
          header.ShouldHaveAttribute("class", "scenario-heading");
          Assert.AreEqual(2, header.Elements().Count());

          header.Elements().ElementAt(0).ShouldBeNamed("h2");
          header.Elements().ElementAt(1).ShouldBeNamed("div");
        }

        [Test]
        public void FeatureTagsAreAddedToScenarioTags()
        {
          var feature = new Feature
          {
            Name = "A Scenario with Tags",
            FeatureElements =
              {
                new Scenario
                  {
                    Name = "A Scenario",
                    Description = @"This scenario has tags",
                    Tags = { "scenarioTag1", "scenarioTag2" }
                  }
              },
            Tags = { "featureTag1", "featureTag2" }
          };

          feature.FeatureElements[0].Feature = feature;

          var htmlFeatureFormatter = Container.Resolve<HtmlFeatureFormatter>();
          XElement featureElement = htmlFeatureFormatter.Format(feature);

          var header = featureElement.Descendants().First(n => n.Attributes().Any(a => a.Name == "class" && a.Value == "scenario-heading"));

          Assert.AreEqual(3, header.Elements().Count());

          header.Elements().ElementAt(0).ShouldBeNamed("h2");
          header.Elements().ElementAt(1).ShouldBeNamed("p");
          header.Elements().ElementAt(2).ShouldBeNamed("div");

          var tagsParagraph = header.Elements().ElementAt(1);

          Assert.AreEqual(
            @"<p class=""tags"" xmlns=""http://www.w3.org/1999/xhtml"">Tags: <span>featureTag1</span>, <span>featureTag2</span>, <span>scenarioTag1</span>, <span>scenarioTag2</span></p>",
            tagsParagraph.ToString());
        }

        [Test]
        public void TagsAreRenderedAlphabetically()
        {
          var feature = new Feature
          {
            Name = "A Scenario with Tags",
            FeatureElements =
              {
                new Scenario
                  {
                    Name = "A Scenario",
                    Description = @"This scenario has tags",
                    Tags = { "a", "c" }
                  }
              },
            Tags = { "d", "b" }
          };

          feature.FeatureElements[0].Feature = feature;

          var htmlFeatureFormatter = Container.Resolve<HtmlFeatureFormatter>();
          XElement featureElement = htmlFeatureFormatter.Format(feature);

          var header = featureElement.Descendants().First(n => n.Attributes().Any(a => a.Name == "class" && a.Value == "scenario-heading"));

          Assert.AreEqual(3, header.Elements().Count());

          header.Elements().ElementAt(0).ShouldBeNamed("h2");
          header.Elements().ElementAt(1).ShouldBeNamed("p");
          header.Elements().ElementAt(2).ShouldBeNamed("div");

          var tagsParagraph = header.Elements().ElementAt(1);

          Assert.AreEqual(
            @"<p class=""tags"" xmlns=""http://www.w3.org/1999/xhtml"">Tags: <span>a</span>, <span>b</span>, <span>c</span>, <span>d</span></p>",
            tagsParagraph.ToString());
        }
    }
}