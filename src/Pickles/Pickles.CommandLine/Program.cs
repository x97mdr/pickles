#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Pickles.Formatters;
using Pickles.TestFrameworks;

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

                if (configuration.ShouldLinkResults)
                {
                    var results = kernel.Get<Results>();
                    results.Load(configuration.LinkedTestFrameworkResultsFile);
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
