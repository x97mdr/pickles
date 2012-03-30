using System;
using Pickles.DirectoryCrawler;

namespace Pickles.Extensions
{
  public static class TreeNodeExtensions
  {
    public static bool IsIndexMarkDownNode(this IDirectoryTreeNode node)
    {
      var markdownItemNode = node as MarkdownTreeNode;
      if (markdownItemNode != null && markdownItemNode.Name.StartsWith("index", StringComparison.InvariantCultureIgnoreCase))
      {
        return true;
      }

      return false;
    }
  }
}