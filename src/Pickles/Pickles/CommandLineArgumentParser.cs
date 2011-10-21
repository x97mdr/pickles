using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDesk.Options;
using System.IO;

namespace Pickles
{
    public class CommandLineArgumentParser
    {
        private readonly OptionSet options;
        private string featureDirectory;
        private string outputDirectory;
        private bool versionRequested;
        private bool helpRequested;

        public CommandLineArgumentParser()
        {
            this.options = new OptionSet
            {
   	            { "f|feature-directory=", "directory to start scanning recursively for features", v => featureDirectory = v },
   	            { "o|output-directory=", "directory where output files will be placed", v => outputDirectory = v },
   	            { "v|version",  v => versionRequested = v != null },
   	            { "h|?|help",   v => helpRequested = v != null }
            };
        }

        private void DisplayVersion(TextWriter stdout)
        {
            stdout.WriteLine("Pickles version {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void DisplayHelp(TextWriter stdout)
        {
            DisplayVersion(stdout);
            this.options.WriteOptionDescriptions(stdout);
        }

        public bool Parse(string[] args, Configuration configuration, TextWriter stdout)
        {
            var extra = this.options.Parse(args);

            if (versionRequested)
            {
                DisplayVersion(stdout);
                return false;
            }
            else if (helpRequested)
            {
                DisplayHelp(stdout);
                return false;
            }

            configuration.FeatureFolder = new System.IO.DirectoryInfo(this.featureDirectory);
            configuration.OutputFolder = new System.IO.DirectoryInfo(this.outputDirectory);
            return true;
        }
    }
}
