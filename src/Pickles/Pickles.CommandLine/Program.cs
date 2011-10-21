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
            var configuration = new Configuration();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, Console.Out);

            if (shouldContinue)
            {
                var documentFormatter = new HtmlDocumentFormatter(new HtmlTableOfContentsFormatter(), new HtmlFeatureFormatter(new HtmlScenarioFormatter(new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter()))), new HtmlFooterFormatter());
                var documentationBuilder = new HtmlDocumentationBuilder(new FeatureCrawler(new FeatureParser()), documentFormatter, new StylesheetWriter());

                documentationBuilder.Build(configuration.FeatureFolder, configuration.OutputFolder);
            }
        }
    }
}
