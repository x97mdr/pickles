using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;
using Pickles.Formatters;

namespace Pickles.NAnt
{
    [TaskName("pickles")]
    public class Pickles : Task
    {
        [TaskAttribute("features", Required = true)]
        [StringValidator(AllowEmpty=false)]
        public string FeatureDirectory { get; set; }

        [TaskAttribute("output", Required = true)]
        [StringValidator(AllowEmpty = false)]
        public string OutputDirectory { get; set; }

        protected override void ExecuteTask()
        {
            try
            {
                Project.Log(Level.Info, "Pickles v.{0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Project.Log(Level.Info, "Reading features from {0}", FeatureDirectory ?? string.Empty);
                Project.Log(Level.Info, "Writing output to {0}", OutputDirectory ?? string.Empty);

                var documentFormatter = new HtmlDocumentFormatter(new HtmlTableOfContentsFormatter(), new HtmlFeatureFormatter(new HtmlScenarioFormatter(new HtmlStepFormatter(new HtmlTableFormatter(), new HtmlMultilineStringFormatter()))), new HtmlFooterFormatter());
                var documentationBuilder = new HtmlDocumentationBuilder(new FeatureCrawler(new FeatureParser()), documentFormatter, new StylesheetWriter());

                var featureDirectoryInfo = new DirectoryInfo(FeatureDirectory);
                var OutputDirectoryInfo = new DirectoryInfo(OutputDirectory);

                documentationBuilder.Build(featureDirectoryInfo, OutputDirectoryInfo);
            }
            catch (Exception e)
            {
                Project.Log(Level.Warning, e.Message);
            }
        }
    }
}
