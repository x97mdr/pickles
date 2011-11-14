using System;
namespace Pickles
{
    public interface IDirectoryTreeNode
    {
        string GetRelativeUriTo(Uri other);
        string GetRelativeUriTo(Uri other, string newExtension);
        bool IsContent { get; }
        string Name { get; }
        System.IO.FileSystemInfo OriginalLocation { get; }
        Uri OriginalLocationUrl { get; }
        string RelativePathFromRoot { get; }
    }
}
