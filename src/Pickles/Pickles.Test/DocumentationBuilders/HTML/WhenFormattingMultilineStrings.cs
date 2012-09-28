using System.Collections.Generic;
using System.Xml.Linq;
using Autofac;
using NUnit.Framework;
using Pickles.DocumentationBuilders.HTML;
using Pickles.Parser;
using Should;

namespace Pickles.Test.DocumentationBuilders.HTML
{
    [TestFixture]
    public class WhenFormattingMultilineStrings : BaseFixture
    {
        [Test]
        public void ThenCanFormatNormalMultilineStringSuccessfully()
        {
            var multilineString = @"This is a
multiline string 
that has been put into a 
gherkin-style spec";

            var multilineStringFormatter = Container.Resolve<HtmlMultilineStringFormatter>();
            var output = multilineStringFormatter.Format(multilineString);

            output.ShouldNotBeNull();
        }

        [Test]
        public void ThenCanFormatNullMultilineStringSuccessfully()
        {
            var multilineStringFormatter = Container.Resolve<HtmlMultilineStringFormatter>();
            var output = multilineStringFormatter.Format(null);

            output.ShouldBeNull();
        }
    }
}
