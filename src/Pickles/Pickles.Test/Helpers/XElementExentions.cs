using System.Linq;
using System.Xml.Linq;

namespace Pickles.Test.Helpers
{
    public static class XElementExentions
    {
        public static XElement FindFirstDescendantWithName(this XElement xelement, string localName)
        {
            return xelement.Descendants().First(x => x.Name.LocalName == localName);
        }
    }
}