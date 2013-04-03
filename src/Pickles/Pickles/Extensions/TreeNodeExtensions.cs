using System;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.Extensions
{
    public static class TreeNodeExtensions
    {
        public static bool IsIndexMarkDownNode(this INode node)
        {
            var markdownItemNode = node as MarkdownNode;
            if (markdownItemNode != null &&
                markdownItemNode.OriginalLocation.Name.StartsWith("index", StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }
    }
}