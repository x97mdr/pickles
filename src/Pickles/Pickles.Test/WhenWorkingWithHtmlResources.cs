using System;
using System.Linq;
using NUnit.Framework;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using NFluent;

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

            Check.That(images.Length).IsEqualTo(3);
            Check.That(images.Select(image => image.File == "success.png")).Not.IsEmpty();
            Check.That(images.Select(image => image.File == "failure.png")).Not.IsEmpty();
            Check.That(images.Select(image => image.File == "inconclusive.png")).Not.IsEmpty();
        }

        private HtmlResourceSet CreateHtmlResourceSet()
        {
            var configuration = new Configuration { OutputFolder = FileSystem.DirectoryInfo.FromDirectoryName(@"c:\") };
            var htmlResources = new HtmlResourceSet(configuration, FileSystem);
            return htmlResources;
        }

        [Test]
        public void ThenCanDetectAllResourcesSuccessfully()
        {
            var htmlResources = this.CreateHtmlResourceSet();
          
            HtmlResource[] resources = htmlResources.All.ToArray();

            Check.That(resources.Length).IsEqualTo(9);
            Check.That(resources.Select(resource => resource.File == "success.png")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "failure.png")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "inconclusive.png")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "global.css")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "master.css")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "reset.css")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "structure.css")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "print.css")).Not.IsEmpty();
            Check.That(resources.Select(resource => resource.File == "font-awesome.css")).Not.IsEmpty();
        }

        [Test]
        public void ThenCanDetectAllScriptsSuccessfully()
        {
            var htmlResources = this.CreateHtmlResourceSet();

            HtmlResource[] scripts = htmlResources.Scripts.ToArray();

            Check.That(scripts.Length).IsEqualTo(2);
            Check.That(scripts.Select(script => script.File == "jquery.js")).Not.IsEmpty();
            Check.That(scripts.Select(script => script.File == "scripts.js")).Not.IsEmpty();
        }

        [Test]
        public void ThenCanDetectAllStylesheetsSuccessfully()
        {
            var htmlResources = this.CreateHtmlResourceSet();

            HtmlResource[] stylesheets = htmlResources.Stylesheets.ToArray();

            Check.That(stylesheets.Length).IsEqualTo(6);
            Check.That(stylesheets.Select(stylesheet => stylesheet.File == "global.css")).Not.IsEmpty();
            Check.That(stylesheets.Select(stylesheet => stylesheet.File == "master.css")).Not.IsEmpty();
            Check.That(stylesheets.Select(stylesheet => stylesheet.File == "reset.css")).Not.IsEmpty();
            Check.That(stylesheets.Select(stylesheet => stylesheet.File == "structure.css")).Not.IsEmpty();
            Check.That(stylesheets.Select(stylesheet => stylesheet.File == "print.css")).Not.IsEmpty();
            Check.That(stylesheets.Select(resource => resource.File == "font-awesome.css")).Not.IsEmpty();
        }

        [Test]
        public void ThenSavesCssFilesToCorrectLocation()
        {
            FileSystem.AddDirectory(@"c:\output\");
            var htmlResourceWriter = new HtmlResourceWriter(FileSystem);

            htmlResourceWriter.WriteTo(@"c:\output\");

            var filesOnFileSystem = FileSystem.AllFiles.AsEnumerable<string>().ToArray();

            Check.That(filesOnFileSystem).Contains(@"c:\output\css\master.css");
            Check.That(filesOnFileSystem).Contains(@"c:\output\css\reset.css");
            Check.That(filesOnFileSystem).Contains(@"c:\output\css\global.css");
            Check.That(filesOnFileSystem).Contains(@"c:\output\css\global.css");
            Check.That(filesOnFileSystem).Contains(@"c:\output\css\structure.css");
            Check.That(filesOnFileSystem).Contains(@"c:\output\css\print.css");
            Check.That(filesOnFileSystem).Contains(@"c:\output\css\font-awesome.css");
        }
    }
}