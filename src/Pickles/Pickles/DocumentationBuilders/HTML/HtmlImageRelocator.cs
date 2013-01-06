using PicklesDoc.Pickles.DirectoryCrawler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace PicklesDoc.Pickles.DocumentationBuilders.HTML
{
    public class HtmlImageRelocator
    {
        private readonly Configuration configuration;

        public HtmlImageRelocator(Configuration configuration)
        {
            this.configuration = configuration;
        }
        
        public virtual void Relocate(IDirectoryTreeNode node, XElement parsedFeature)
        {
            var images = parsedFeature.Descendants(HtmlNamespace.Xhtml + "img");

            foreach (var image in images)
            {
                var sourceValue = image.Attribute("src").Value;

                if (!String.IsNullOrEmpty(sourceValue))
                {
                    var relativePathToImage = Path.Combine(Path.GetDirectoryName(node.RelativePathFromRoot), sourceValue);
                    var source = Path.Combine(configuration.FeatureFolder.FullName, relativePathToImage);
                    var destination = Path.Combine(configuration.OutputFolder.FullName, relativePathToImage);
                    File.WriteAllBytes(destination, File.ReadAllBytes(source));
                }
            }
        }
    }
}
