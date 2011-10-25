using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using MarkdownSharp;

namespace Pickles.Formatters
{
    public class HtmlMarkdownFormatter
    {
        private readonly Markdown markdown;

        public HtmlMarkdownFormatter(Markdown markdown)
        {
            this.markdown = markdown;
        }

        public XElement Format(string text)
        {
            // HACK - we add the div around the markdown content because XElement requires a single root element from which to parse and Markdown.Transform() returns a series of elements
            return XElement.Parse("<div id=\"markdown\" xmlns=\"http://www.w3.org/1999/xhtml\">" + this.markdown.Transform(text) + "</div>");
        }
    }
}
