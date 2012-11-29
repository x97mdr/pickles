using System;
using System.Xml.Linq;

namespace PicklesDoc.Pickles.DocumentationBuilders.HTML
{
    public static class HtmlNamespace
    {
        /// <summary>
        /// An XNamespace instance for http://www.w3.org/1999/xhtml.
        /// </summary>
        public static readonly XNamespace Xhtml = XNamespace.Get("http://www.w3.org/1999/xhtml");
    }
}