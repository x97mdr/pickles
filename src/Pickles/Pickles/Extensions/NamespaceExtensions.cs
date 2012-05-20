using System.Xml.Linq;

namespace Pickles.Extensions
{
    /// <summary>
    /// Extension Methods to work with namespaces.
    /// </summary>
    public static class NamespaceExtensions
    {
        /// <summary>
        /// Moves <paramref name="element"/> into namespace <paramref name="newNamespace"/>.
        /// </summary>
        /// <param name="element">The element that will be moved into a new namespace.</param>
        /// <param name="newNamespace">The new namespace for the element.</param>
        public static void MoveToNamespace(this XElement element, XNamespace newNamespace)
        {
            foreach (XElement el in element.DescendantsAndSelf())
            {
                el.Name = newNamespace.GetName(el.Name.LocalName);
            }
        }
    }
}