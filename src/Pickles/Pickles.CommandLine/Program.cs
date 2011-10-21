using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pickles.Formatters;

namespace Pickles.CommandLine
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var configuration = new Configuration();
            var commandLineArgumentParser = new CommandLineArgumentParser();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, Console.Out);

            if (shouldContinue)
            {
                if (log.IsInfoEnabled)
                {
                    log.InfoFormat("Pickles v.{0}{1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(), Environment.NewLine);
                    log.InfoFormat("Reading features from {0}", configuration.FeatureFolder.FullName);
                }

                var documentFormatter = new HtmlDocumentFormatter(new HtmlTableOfContentsFormatter(), new HtmlFeatureFormatter(new HtmlScenarioFormatter(new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter()))), new HtmlFooterFormatter());
                var documentationBuilder = new HtmlDocumentationBuilder(new FeatureCrawler(new FeatureParser()), documentFormatter, new StylesheetWriter());

                documentationBuilder.Build(configuration.FeatureFolder, configuration.OutputFolder);

                if (log.IsInfoEnabled)
                {
                    log.Info("Pickles completed successfully");
                }
            }
        }
    }
}
