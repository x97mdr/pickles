using System;
namespace Pickles
{
    public interface IDocumentationBuilder
    {
        void Build(NGenerics.DataStructures.Trees.GeneralTree<System.IO.FileSystemInfo> featureFiles);
    }
}
