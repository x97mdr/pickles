using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Pickles.Extensions;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlResourceSet
    {
        private readonly Configuration configuration;

        public HtmlResourceSet(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public Uri MasterStylesheet
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("master.css"); }
        }

        public Uri PrintStylesheet
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("print.css"); }
        }

        public Uri jQueryScript
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("scripts/jquery.js"); }
        }

        public Uri AdditionalScripts
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("scripts/scripts.js"); }
        }

        public Uri SuccessImage
        {
            get { return new Uri(Path.Combine(this.ImagesFolder, "success.png")); }
        }

        public string ImagesFolder
        {
            get { return Path.Combine(this.configuration.OutputFolder.FullName, "images"); }
        }

        public string ScriptsFolder
        {
            get { return Path.Combine(this.configuration.OutputFolder.FullName, "scripts"); }
        }

        public Uri FailureImage
        {
            get { return new Uri(Path.Combine(this.ImagesFolder, "failure.png")); }
        }

        public IEnumerable<HtmlResource> All
        {
            get { return this.Stylesheets.Concat(this.Images); }
        }

        public IEnumerable<HtmlResource> Stylesheets
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (string resource in resources.Where(resource => resource.EndsWith(".css")))
                {
                    string fileName = this.GetNameFromResourceName(resource);
                    yield return new HtmlResource
                                     {
                                         File = fileName,
                                         Uri = this.configuration.OutputFolder.ToFileUriCombined(fileName)
                                     };
                }
            }
        }

        public IEnumerable<HtmlResource> Images
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (string resource in resources.Where(resource => resource.EndsWith(".png")))
                {
                    string fileName = this.GetNameFromResourceName(resource);
                    yield return new HtmlResource
                                     {
                                         File = fileName,
                                         Uri = new Uri(Path.Combine(this.ImagesFolder, fileName))
                                     };
                }
            }
        }

        public IEnumerable<HtmlResource> Scripts
        {
            get
            {
                string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (string resource in resources.Where(resource => resource.EndsWith(".js")))
                {
                    string fileName = this.GetNameFromResourceName(resource);
                    yield return new HtmlResource
                                     {
                                         File = fileName,
                                         Uri = new Uri(Path.Combine(this.ScriptsFolder, fileName))
                                     };
                }
            }
        }

        public Uri InconclusiveImage
        {
          get { return new Uri(Path.Combine(this.ImagesFolder, "inconclusive.png")); }
        }



        private string GetNameFromResourceName(string resourceName)
        {
            if (resourceName.StartsWith("Pickles.Resources.images"))
                return resourceName.Replace("Pickles.Resources.images.", string.Empty);
            else if (resourceName.StartsWith("Pickles.Resources.scripts"))
                return resourceName.Replace("Pickles.Resources.scripts.", string.Empty);
            else return resourceName.Replace("Pickles.Resources.", string.Empty);
        }
    }
}