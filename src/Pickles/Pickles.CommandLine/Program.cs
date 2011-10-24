using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Pickles.Formatters;

namespace Pickles.CommandLine
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var kernel = new StandardKernel(new PicklesModule());
            var configuration = kernel.Get<Configuration>();
            var commandLineArgumentParser = kernel.Get<CommandLineArgumentParser>();
            bool shouldContinue = commandLineArgumentParser.Parse(args, configuration, Console.Out);

            if (shouldContinue)
            {
                if (log.IsInfoEnabled)
                {
                    log.InfoFormat("Pickles v.{0}{1}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(), Environment.NewLine);
                    log.InfoFormat("Reading features from {0}", configuration.FeatureFolder.FullName);
                }

                var documentationBuilder = kernel.Get<HtmlDocumentationBuilder>();
                documentationBuilder.Build(configuration.FeatureFolder, configuration.OutputFolder);

                if (log.IsInfoEnabled)
                {
                    log.Info("Pickles completed successfully");
                }
            }
        }
    }
}
