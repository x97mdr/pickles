using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using NUnit.Framework;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenWorkingWithHtmlResources : BaseFixture
    {
        [Test]
        public void Then_can_detect_all_stylesheets_successfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new System.IO.DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            var stylesheets = htmlResources.Stylesheets.ToArray();

            Assert.AreEqual(5, stylesheets.Length);
            Assert.AreEqual(true, stylesheets.Any(stylesheet => stylesheet.File == "global.css"));
            Assert.AreEqual(true, stylesheets.Any(stylesheet => stylesheet.File == "master.css"));
            Assert.AreEqual(true, stylesheets.Any(stylesheet => stylesheet.File == "reset.css"));
            Assert.AreEqual(true, stylesheets.Any(stylesheet => stylesheet.File == "structure.css"));
            Assert.AreEqual(true, stylesheets.Any(stylesheet => stylesheet.File == "print.css"));
        }

        [Test]
        public void Then_can_detect_all_images_successfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new System.IO.DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            var images = htmlResources.Images.ToArray();

            Assert.AreEqual(2, images.Length);
            Assert.AreEqual(true, images.Any(image => image.File == "success.png"));
            Assert.AreEqual(true, images.Any(image => image.File == "failure.png"));
        }

        [Test]
        public void Then_can_detect_all_scripts_successfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new System.IO.DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            var scripts = htmlResources.Scripts.ToArray();

            Assert.AreEqual(2, scripts.Length);
            Assert.AreEqual(true, scripts.Any(script => script.File == "jquery.dataTables.min.js"));
            Assert.AreEqual(true, scripts.Any(script => script.File == "jquery.js"));
        }

        [Test]
        public void Then_can_detect_all_resources_successfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new System.IO.DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            var resources = htmlResources.All.ToArray();

            Assert.AreEqual(7, resources.Length);
            Assert.AreEqual(true, resources.Any(resource => resource.File == "success.png"));
            Assert.AreEqual(true, resources.Any(resource => resource.File == "failure.png"));
            Assert.AreEqual(true, resources.Any(resource => resource.File == "global.css"));
            Assert.AreEqual(true, resources.Any(resource => resource.File == "master.css"));
            Assert.AreEqual(true, resources.Any(resource => resource.File == "reset.css"));
            Assert.AreEqual(true, resources.Any(resource => resource.File == "structure.css"));
            Assert.AreEqual(true, resources.Any(resource => resource.File == "print.css"));
        }
    }
}
