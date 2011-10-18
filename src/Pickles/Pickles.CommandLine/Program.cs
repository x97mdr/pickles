using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pickles.Formatters;

namespace Pickles.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = args[0];
            var output = args[1];

            var documentFormatter = new HtmlDocumentFormatter(new HtmlTableOfContentsFormatter(), new HtmlFeatureFormatter(new HtmlScenarioFormatter(new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter()))));
            var documentationBuilder = new HtmlDocumentationBuilder(new FeatureCrawler(new FeatureParser()), documentFormatter);

            documentationBuilder.Build(new System.IO.DirectoryInfo(input), new System.IO.DirectoryInfo(output));
        }
    }
}
