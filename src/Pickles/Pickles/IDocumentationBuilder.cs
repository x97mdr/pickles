using System;
using System.IO;
namespace Pickles
{
    public interface IDocumentationBuilder
    {
        void Build(DirectoryInfo inputPath, DirectoryInfo outputPath);
    }
}
