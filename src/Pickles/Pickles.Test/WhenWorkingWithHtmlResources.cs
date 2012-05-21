using System.IO;
using System.Linq;
using NUnit.Framework;
using Ninject;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenWorkingWithHtmlResources : BaseFixture
    {
        [Test]
        public void ThenCanDetectAllImagesSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            HtmlResource[] images = htmlResources.Images.ToArray();

            images.Length.ShouldEqual(2);
            images.Any(image => image.File == "success.png").ShouldBeTrue();
            images.Any(image => image.File == "failure.png").ShouldBeTrue();
        }

        [Test]
        public void ThenCanDetectAllResourcesSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            HtmlResource[] resources = htmlResources.All.ToArray();

            resources.Length.ShouldEqual(7);
            resources.Any(resource => resource.File == "success.png").ShouldBeTrue();
            resources.Any(resource => resource.File == "failure.png").ShouldBeTrue();
            resources.Any(resource => resource.File == "global.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "master.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "reset.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "structure.css").ShouldBeTrue();
            resources.Any(resource => resource.File == "print.css").ShouldBeTrue();
        }

        [Test]
        public void ThenCanDetectAllScriptsSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            HtmlResource[] scripts = htmlResources.Scripts.ToArray();

            scripts.Length.ShouldEqual(2);
            scripts.Any(script => script.File == "jquery.js").ShouldBeTrue();
            scripts.Any(script => script.File == "scripts.js").ShouldBeTrue();
        }

        [Test]
        public void ThenCanDetectAllStylesheetsSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Kernel.Get<HtmlResourceSet>();
            HtmlResource[] stylesheets = htmlResources.Stylesheets.ToArray();

            stylesheets.Length.ShouldEqual(5);
            stylesheets.Any(stylesheet => stylesheet.File == "global.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "master.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "reset.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "structure.css").ShouldBeTrue();
            stylesheets.Any(stylesheet => stylesheet.File == "print.css").ShouldBeTrue();
        }
    }
}