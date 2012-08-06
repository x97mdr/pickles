#region License

/*
    Copyright [2011] [Jeffrey Cameron]

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

#endregion

using System.Xml.Linq;
using MarkdownDeep;
using Pickles.Extensions;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlMarkdownFormatter
    {
        private readonly Markdown markdown;

        private readonly XNamespace xmlns;

        public HtmlMarkdownFormatter(Markdown markdown)
        {
            this.markdown = markdown;
            xmlns = HtmlNamespace.Xhtml;
        }

        public XElement Format(string text)
        {
            // HACK - we add the div around the markdown content because XElement requires a single root element from which to parse and Markdown.Transform() returns a series of elements
            XElement xElement = XElement.Parse("<div>" + markdown.Transform(text) + "</div>");
            xElement.SetAttributeValue("id", "markdown");

            xElement.MoveToNamespace(xmlns);

            return xElement;
        }
    }
}