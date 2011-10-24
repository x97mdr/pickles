using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Ninject;
using Pickles.Formatters;

namespace Pickles.MSBuild
{
    public class Pickles : Task
    {
        [Required]
        public string FeatureDirectory { get; set; }

        [Required]
        public string OutputDirectory { get; set; }

        public override bool Execute()
        {
            try
            {
                Log.LogMessage("Pickles v.{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Log.LogMessage("Reading features from {0}", FeatureDirectory ?? string.Empty);
                Log.LogMessage("Writing output to {0}", OutputDirectory ?? string.Empty);

                var kernel = new StandardKernel(new PicklesModule());
                var documentationBuilder = kernel.Get<HtmlDocumentationBuilder>();

                var featureDirectoryInfo = new DirectoryInfo(FeatureDirectory);
                var OutputDirectoryInfo = new DirectoryInfo(OutputDirectory);

                documentationBuilder.Build(featureDirectoryInfo, OutputDirectoryInfo);
            }
            catch (Exception e)
            {
                Log.LogWarningFromException(e, false);
            }

            return true; // HACK - since this is merely producing documentation we do not want it to cause a build to fail if something goes wrong
        }
    }
}
