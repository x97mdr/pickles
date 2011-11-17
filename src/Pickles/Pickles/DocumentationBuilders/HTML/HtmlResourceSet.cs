using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

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
                return new Uri(Path.Combine(configuration.OutputFolder.FullName, "master.css"));
            }
        }

        public Uri jQueryScript
        {
            get
            {
                return new Uri(Path.Combine(configuration.OutputFolder.FullName, "scripts/jquery.js"));
            }
        }

        public Uri jQueryDataTablesScript
        {
            get
            {
                return new Uri(Path.Combine(configuration.OutputFolder.FullName, "scripts/jquery.dataTables.min.js"));
            }
        }

        public Uri SuccessImage
        {
            get
            {
                return new Uri(Path.Combine(configuration.OutputFolder.FullName, "images", "success.png"));
            }
        }

        public Uri FailureImage
        {
            get
            {
                return new Uri(Path.Combine(configuration.OutputFolder.FullName, "images", "failure.png"));
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
                        Uri = new Uri(Path.Combine(configuration.OutputFolder.FullName, fileName))
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
                        Uri = new Uri(Path.Combine(configuration.OutputFolder.FullName, "images", fileName))
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
                        Uri = new Uri(Path.Combine(configuration.OutputFolder.FullName, "scripts", fileName))
                    };
                }
            }
        }
    }
}
