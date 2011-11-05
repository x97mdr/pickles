using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace Pickles
{
    public class Runner
    {
        public void Run(IKernel kernel)
        {
            var configuration = kernel.Get<Configuration>();
            if (!configuration.OutputFolder.Exists) configuration.OutputFolder.Create();

            var documentationBuilder = kernel.Get<HtmlDocumentationBuilder>();
            documentationBuilder.Build(configuration.FeatureFolder, configuration.OutputFolder);
        }
    }
}
