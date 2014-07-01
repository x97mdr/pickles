using System;
using System.Linq;
using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using Should;

namespace PicklesDoc.Pickles.Test
{
    [TestFixture]
    public class WhenWorkingWithHtmlResources : BaseFixture
    {
        [Test]
        public void ThenCanDetectAllImagesSuccessfully()
        {
            var htmlResources = this.CreateHtmlResourceSet();

            HtmlResource[] images = htmlResources.Images.ToArray();

            images.Length.ShouldEqual(3);
            images.Any(image => image.File == "success.png").ShouldBeTrue();
            images.Any(image => image.File == "failure.png").ShouldBeTrue();
            images.Any(image => image.File == "inconclusive.png").ShouldBeTrue();
        }

        private HtmlResourceSet CreateHtmlResourceSet()
        {
            var configuration = new Configuration() { OutputFolder = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\") };
            var htmlResources = new HtmlResourceSet(configuration, FileSystem);
            return htmlResources;
        }

        [Test]
        public void ThenCanDetectAllResourcesSuccessfully()
        {
            var htmlResources = this.CreateHtmlResourceSet();
          
            HtmlResource[] resources = htmlResources.All.ToArray();

            resources.Length.ShouldEqual(9);
            resources.Any(resource => resource.File == "success.png").ShouldBeTrue();
            resources.Any(resource => resource.File == "failure.png").ShouldBeTrue();
            resources.Any(resource => resource.File == "inconclusive.png").ShouldBeTrue();
            resources.Any(resource => resource.File == "global.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "master.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "reset.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "structure.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "print.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "font-awesome.css").ShouldBeTrue();
        }

        [Test]
        public void ThenCanDetectAllScriptsSuccessfully()
        {
            var htmlResources = this.CreateHtmlResourceSet();

            HtmlResource[] scripts = htmlResources.Scripts.ToArray();

            scripts.Length.ShouldEqual(2);
            scripts.Any(script => script.File == "jquery.js").ShouldBeTrue();
            scripts.Any(script => script.File == "scripts.js").ShouldBeTrue();
        }

        [Test]
        public void ThenCanDetectAllStylesheetsSuccessfully()
        {
            var htmlResources = this.CreateHtmlResourceSet();

            HtmlResource[] stylesheets = htmlResources.Stylesheets.ToArray();

            stylesheets.Length.ShouldEqual(6);
            stylesheets.Any(stylesheet => stylesheet.File == "global.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "master.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "reset.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "structure.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "print.css").ShouldBeTrue();
            stylesheets.Any(resource => resource.File == "font-awesome.css").ShouldBeTrue();
        }
    }
}