using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Autofac;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenFormattingFeatures : BaseFixture
    {
        [Test]
        public void ThenCanFormatDescriptionAsMarkdown()
        {
            var configuration = Container.Resolve<Configuration>();
            configuration.TestResultsFile = null;

            var feature = new Feature
                              {
                                  Name = "A feature",
                                  Description =
                                      @"In order to see the description as nice HTML
As a Pickles user
I want to see the descriptions written in markdown rendered as HTML

Introduction
============

This feature should have some markdown elements that get displayed properly

Context
-------

> I really like blockquotes to describe
> to describe text

I also enjoy using lists as well, here are the reasons

- Lists are easy to read
- Lists make my life easier

I also enjoy ordering things

1. This is the first reason
2. This is the second reason"
                              };

            var htmlFeatureFormatter = Container.Resolve<HtmlFeatureFormatter>();
            XElement featureElement = htmlFeatureFormatter.Format(feature);
            XElement description = featureElement.Elements().FirstOrDefault(element => element.Name.LocalName == "div");

            Assert.NotNull(description);
            description.ShouldBeNamed("div");
            description.ShouldBeInInNamespace("http://www.w3.org/1999/xhtml");
            description.ShouldHaveAttribute("class", "description");
            Assert.AreEqual(9, description.Elements().Count());

            description.Elements().ElementAt(0).ShouldBeNamed("p");
            description.Elements().ElementAt(1).ShouldBeNamed("h1");
            description.Elements().ElementAt(2).ShouldBeNamed("p");
            description.Elements().ElementAt(3).ShouldBeNamed("h2");
            description.Elements().ElementAt(4).ShouldBeNamed("blockquote");
            description.Elements().ElementAt(5).ShouldBeNamed("p");
            description.Elements().ElementAt(6).ShouldBeNamed("ul");
            description.Elements().ElementAt(7).ShouldBeNamed("p");
            description.Elements().ElementAt(8).ShouldBeNamed("ol");
        }
    }
}