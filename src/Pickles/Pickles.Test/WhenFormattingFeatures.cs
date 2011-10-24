using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;
using Pickles.Parser;
using Pickles.Formatters;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenFormattingFeatures : BaseFixture
    {
        [Test]
        public void When_can_format_description_as_markdown()
        {
            var feature = new Feature
            {
                Name = "A feature",
                Description = @"In order to see the description as nice HTML
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

            var htmlFeatureFormatter = Kernel.Get<HtmlFeatureFormatter>();
            var featureElement = htmlFeatureFormatter.Format(feature);
            var description = featureElement.Elements().FirstOrDefault(element => element.Name.LocalName == "div");

            Assert.NotNull(description);
            description.AssertIsNamed("div");
            description.AssertIsInNamespace("http://www.w3.org/1999/xhtml");
            description.AssertHasAttribute("class", "description");
            Assert.AreEqual(9, description.Elements().Count());

            description.Elements().ElementAt(0).AssertIsNamed("p");
            description.Elements().ElementAt(1).AssertIsNamed("h1");
            description.Elements().ElementAt(2).AssertIsNamed("p");
            description.Elements().ElementAt(3).AssertIsNamed("h2");
            description.Elements().ElementAt(4).AssertIsNamed("blockquote");
            description.Elements().ElementAt(5).AssertIsNamed("p");
            description.Elements().ElementAt(6).AssertIsNamed("ul");
            description.Elements().ElementAt(7).AssertIsNamed("p");
            description.Elements().ElementAt(8).AssertIsNamed("ol");
        }
    }
}
