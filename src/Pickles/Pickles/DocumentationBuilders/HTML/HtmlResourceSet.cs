using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Pickles.Extensions;

namespace Pickles
{
    public class HtmlResourceSet
    {
        private readonly Configuration configuration;

        public HtmlResourceSet(Configuration configuration)
        {
            this.configuration = configuration;
        }

        private string GetNameFromResourceName(string resourceName)
        {
            if (resourceName.StartsWith("Pickles.Resources.images")) return resourceName.Replace("Pickles.Resources.images.", string.Empty);
            else if (resourceName.StartsWith("Pickles.Resources.scripts")) return resourceName.Replace("Pickles.Resources.scripts.", string.Empty);
            else return resourceName.Replace("Pickles.Resources.", string.Empty);
        }

        public Uri MasterStylesheet
        {
            get
            {
              return configuration.OutputFolder.ToFileUriCombined("master.css");
            }
        }

        public Uri PrintStylesheet
        {
            get
            {
              return configuration.OutputFolder.ToFileUriCombined("print.css");
            }
        }

        public Uri jQueryScript
        {
            get
            {
              return configuration.OutputFolder.ToFileUriCombined("scripts/jquery.js");
            }
        }

        public Uri AdditionalScripts
        {
            get
            {
              return configuration.OutputFolder.ToFileUriCombined("scripts/scripts.js");
            }
        }

        public Uri SuccessImage
        {
            get
            {
                return new Uri(Path.Combine(ImagesFolder, "success.png"));
            }
        }

        public string ImagesFolder { get { return Path.Combine(configuration.OutputFolder.FullName, "images"); } }
        public string ScriptsFolder { get { return Path.Combine(configuration.OutputFolder.FullName, "scripts"); } }

        public Uri FailureImage
        {
            get
            {
                return new Uri(Path.Combine(ImagesFolder, "failure.png"));
            }
        }

        public IEnumerable<HtmlResource> All
        {
            get
            {
                return Stylesheets.Concat(Images);
            }
        }

        public IEnumerable<HtmlResource> Stylesheets
        {
            get
            {
                var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (var resource in resources.Where(resource => resource.EndsWith(".css")))
                {
                    var fileName = GetNameFromResourceName(resource);
                    yield return new HtmlResource
                    {
                        File = fileName,
                        Uri = configuration.OutputFolder.ToFileUriCombined(fileName)
                    };
                }
            }
        }

        public IEnumerable<HtmlResource> Images
        {
            get
            {
                var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (var resource in resources.Where(resource => resource.EndsWith(".png")))
                {
                    var fileName = GetNameFromResourceName(resource);
                    yield return new HtmlResource
                    {
                        File = fileName,
                        Uri = new Uri(Path.Combine(ImagesFolder, fileName))
                    };
                }
            }
        }

        public IEnumerable<HtmlResource> Scripts
        {
            get
            {
                var resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                foreach (var resource in resources.Where(resource => resource.EndsWith(".js")))
                {
                    var fileName = GetNameFromResourceName(resource);
                    yield return new HtmlResource
                    {
                        File = fileName,
                        Uri = new Uri(Path.Combine(ScriptsFolder, fileName))
                    };
                }
            }
        }
    }
}
