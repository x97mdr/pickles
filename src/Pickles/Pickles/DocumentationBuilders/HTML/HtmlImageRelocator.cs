using System;
using System.IO.Abstractions;
using System.Xml.Linq;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.DocumentationBuilders.HTML
{
    public class HtmlImageRelocator
    {
        private readonly Configuration configuration;

        private readonly IFileSystem fileSystem;

        public HtmlImageRelocator(Configuration configuration, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
        }

        public virtual void Relocate(INode node, XElement parsedFeature)
        {
            var images = parsedFeature.Descendants(HtmlNamespace.Xhtml + "img");

            foreach (var image in images)
            {
                var sourceValue = image.Attribute("src").Value;

                if (!string.IsNullOrEmpty(sourceValue))
                {
                    var relativePathToImage = this.fileSystem.Path.Combine(this.fileSystem.Path.GetDirectoryName(node.RelativePathFromRoot), sourceValue);
                    var source = this.fileSystem.Path.Combine(configuration.FeatureFolder.FullName, relativePathToImage);
                    var destination = this.fileSystem.Path.Combine(configuration.OutputFolder.FullName, relativePathToImage);
                    this.fileSystem.File.WriteAllBytes(destination, this.fileSystem.File.ReadAllBytes(source));
                }
            }
        }
    }
}
