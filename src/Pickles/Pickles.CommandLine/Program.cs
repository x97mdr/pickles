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
using System.Reflection;
using Ninject;
using log4net;
using log4net.Config;

namespace Pickles.CommandLine
{
    internal class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var kernel = new StandardKernel(new PicklesModule());
            var configuration = kernel.Get<Configuration>();

            var commandLineArgumentParser = kernel.Get<CommandLineArgumentParser>();
            var shouldContinue = commandLineArgumentParser.Parse(args, configuration, Console.Out);

            if (shouldContinue)
            {
                if (log.IsInfoEnabled)
                {
                    log.InfoFormat("Pickles v.{0}{1}", Assembly.GetExecutingAssembly().GetName().Version,
                                   Environment.NewLine);
                    log.InfoFormat("Reading features from {0}", configuration.FeatureFolder.FullName);
                }

                var runner = kernel.Get<Runner>();
                runner.Run(kernel);

                if (log.IsInfoEnabled)
                {
                    log.Info("Pickles completed successfully");
                }
            }
        }
    }
}