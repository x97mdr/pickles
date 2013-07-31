using Autofac;
using NUnit.Framework;
using PicklesDoc.Pickles.DirectoryCrawler;
using PicklesDoc.Pickles.DocumentationBuilders.HTML;
using Should;
using System.Drawing.Imaging;

namespace PicklesDoc.Pickles.Test.DocumentationBuilders.HTML
{
    [TestFixture]
    public class WhenMovingImages : BaseFixture
    {
        [Test]
        public void ThenCanMoveImageAsRelativePathToOutputLocation()
        {
            var rootDirectory = RealFileSystem.Path.Combine(RealFileSystem.Directory.GetCurrentDirectory(), "MovingImagesTest");
            RealFileSystem.Directory.CreateDirectory(rootDirectory);

            try
            {
                var configuration = Container.Resolve<Configuration>();

                configuration.FeatureFolder = RealFileSystem.DirectoryInfo.FromDirectoryName(RealFileSystem.Path.Combine(rootDirectory, "Features"));
                RealFileSystem.Directory.CreateDirectory(configuration.FeatureFolder.FullName);

                configuration.OutputFolder = RealFileSystem.DirectoryInfo.FromDirectoryName(RealFileSystem.Path.Combine(rootDirectory, "Output"));
                RealFileSystem.Directory.CreateDirectory(configuration.OutputFolder.FullName);

                var imageFilePath = RealFileSystem.Path.Combine(configuration.FeatureFolder.FullName, "test.jpg");
                Resources.test.Save(imageFilePath, ImageFormat.Jpeg);

                var featureText = @"
Feature: Test Image Relocation
    In order to have images that are in the place where I want them
    As a pickles users
    I want my images to be moved

    ![Test Image](test.jpg)
";

                var featureFilePath = RealFileSystem.Path.Combine(configuration.FeatureFolder.FullName, "test.feature");
                RealFileSystem.File.WriteAllText(featureFilePath, featureText, System.Text.Encoding.UTF8);

                var factory = Container.Resolve<FeatureNodeFactory>();
                var node = (FeatureNode)factory.Create(configuration.FeatureFolder, RealFileSystem.FileInfo.FromFileName(featureFilePath));

                var htmlFeatureFormatter = Container.Resolve<HtmlFeatureFormatter>();
                var featureXml = htmlFeatureFormatter.Format(node.Feature);

                var imageRelocator = Container.Resolve<HtmlImageRelocator>();
                imageRelocator.Relocate(node, featureXml);

                var outputImage = RealFileSystem.FileInfo.FromFileName(RealFileSystem.Path.Combine(configuration.OutputFolder.FullName, "test.jpg"));
                outputImage.Exists.ShouldBeTrue();
            }
            finally
            {
              RealFileSystem.Directory.Delete(rootDirectory, true);
            }
        }
    }
}
