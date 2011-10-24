using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkdownSharp;
using System.Xml.Linq;

namespace Pickles.Formatters
{
    public class HtmlDescriptionFormatter
    {
        private readonly Markdown markdown;

        public HtmlDescriptionFormatter(Markdown markdown)
        {
            this.markdown = markdown;
        }

        public XElement Format(string descriptionText)
        {
            var markdownResult = "<div class=\"description\" xmlns=\"http://www.w3.org/1999/xhtml\">" + markdown.Transform(descriptionText) + "</div>";
            var descriptionElements = XElement.Parse(markdownResult);

            return descriptionElements;
        }
    }
}
