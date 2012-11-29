using System.IO;
using System.Linq;
using NUnit.Framework;
using Autofac;
using Pickles.DocumentationBuilders.HTML;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenWorkingWithHtmlResources : BaseFixture
    {
        [Test]
        public void ThenCanDetectAllImagesSuccessfully()
        {
            var configuration = Container.Resolve<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Container.Resolve<HtmlResourceSet>();
            HtmlResource[] images = htmlResources.Images.ToArray();

            images.Length.ShouldEqual(3);
            images.Any(image => image.File == "success.png").ShouldBeTrue();
            images.Any(image => image.File == "failure.png").ShouldBeTrue();
            images.Any(image => image.File == "inconclusive.png").ShouldBeTrue();
        }

        [Test]
        public void ThenCanDetectAllResourcesSuccessfully()
        {
            var configuration = Container.Resolve<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Container.Resolve<HtmlResourceSet>();
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
            var configuration = Container.Resolve<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Container.Resolve<HtmlResourceSet>();
            HtmlResource[] scripts = htmlResources.Scripts.ToArray();

            scripts.Length.ShouldEqual(2);
            scripts.Any(script => script.File == "jquery.js").ShouldBeTrue();
            scripts.Any(script => script.File == "scripts.js").ShouldBeTrue();
        }

        [Test]
        public void ThenCanDetectAllStylesheetsSuccessfully()
        {
            var configuration = Container.Resolve<Configuration>();
            configuration.OutputFolder = configuration.OutputFolder = new DirectoryInfo(@"c:\");

            var htmlResources = Container.Resolve<HtmlResourceSet>();
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