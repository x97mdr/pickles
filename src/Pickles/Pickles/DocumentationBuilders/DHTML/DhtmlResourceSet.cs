using System;
using System.IO.Abstractions;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.DHTML
{
    public class DhtmlResourceSet
    {
        private readonly Configuration configuration;

        private readonly IFileSystem fileSystem;

        public DhtmlResourceSet(Configuration configuration, IFileSystem fileSystem)
        {
            this.configuration = configuration;
            this.fileSystem = fileSystem;
        }

        public Uri ZippedResources
        {
            get { return this.configuration.OutputFolder.ToFileUriCombined("Pickles.BaseDhtmlFiles.zip", this.fileSystem); }
        }
    }
}