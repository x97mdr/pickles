using Autofac;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using Should;
using System.Drawing.Imaging;
using System.IO;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML
{
    [TestFixture]
    public class WhenMovingImages : BaseFixture
    {
        [Test]
        public void ThenCanMoveImageAsRelativePathToOutputLocation()
        {
            var rootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "MovingImagesTest");
            Directory.CreateDirectory(rootDirectory);

            try
            {
                var configuration = Container.Resolve<Configuration>();

                configuration.FeatureFolder = new DirectoryInfo(Path.Combine(rootDirectory, "Features"));
                Directory.CreateDirectory(configuration.FeatureFolder.FullName);

                configuration.OutputFolder = new DirectoryInfo(Path.Combine(rootDirectory, "Output"));
                Directory.CreateDirectory(configuration.OutputFolder.FullName);

                var imageFilePath = Path.Combine(configuration.FeatureFolder.FullName, "test.jpg");
                Resources.test.Save(imageFilePath, ImageFormat.Jpeg);

                var featureText = @"
Feature: Test Image Relocation
    In order to have images that are in the place where I want them
    As a pickles users
    I want my images to be moved

    ![Test Image](test.jpg)
";

                var featureFilePath = Path.Combine(configuration.FeatureFolder.FullName, "test.feature");
                File.WriteAllText(featureFilePath, featureText, System.Text.Encoding.UTF8);

                var factory = Container.Resolve<FeatureNodeFactory>();
                var node = (FeatureDirectoryTreeNode)factory.Create(configuration.FeatureFolder, new FileInfo(featureFilePath));

                var htmlFeatureFormatter = Container.Resolve<HtmlFeatureFormatter>();
                var featureXml = htmlFeatureFormatter.Format(node.Feature);

                var imageRelocator = Container.Resolve<HtmlImageRelocator>();
                imageRelocator.Relocate(node, featureXml);

                var outputImage = new FileInfo(Path.Combine(configuration.OutputFolder.FullName, "test.jpg"));
                outputImage.Exists.ShouldBeTrue();
            }
            finally
            {
                Directory.Delete(rootDirectory, true);
            }
        }
    }
}
