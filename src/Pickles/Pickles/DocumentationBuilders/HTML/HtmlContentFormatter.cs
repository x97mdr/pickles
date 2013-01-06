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

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using PicklesDoc.Pickles.DirectoryCrawler;

namespace PicklesDoc.Pickles.DocumentationBuilders.HTML
{
    public class HtmlContentFormatter
    {
        private readonly IHtmlFeatureFormatter htmlFeatureFormatter;
        private readonly HtmlIndexFormatter htmlIndexFormatter;
        private readonly HtmlImageRelocator htmlImageRelocator;

        public HtmlContentFormatter(IHtmlFeatureFormatter htmlFeatureFormatter, HtmlIndexFormatter htmlIndexFormatter, HtmlImageRelocator htmlImageRelocator)
        {
            if (htmlFeatureFormatter == null) throw new ArgumentNullException("htmlFeatureFormatter");
            if (htmlIndexFormatter == null) throw new ArgumentNullException("htmlIndexFormatter");

            this.htmlFeatureFormatter = htmlFeatureFormatter;
            this.htmlIndexFormatter = htmlIndexFormatter;
            this.htmlImageRelocator = htmlImageRelocator;
        }

        public XElement Format(IDirectoryTreeNode contentNode, IEnumerable<IDirectoryTreeNode> features)
        {
            var featureItemNode = contentNode as FeatureDirectoryTreeNode;
            if (featureItemNode != null)
            {
                var formattedContent = this.htmlFeatureFormatter.Format(featureItemNode.Feature);
                this.htmlImageRelocator.Relocate(contentNode, formattedContent);
                return formattedContent;
            }

            var indexItemNode = contentNode as FolderDirectoryTreeNode;
            if (indexItemNode != null)
            {
                return this.htmlIndexFormatter.Format(indexItemNode, features);
            }

            var markdownItemNode = contentNode as MarkdownTreeNode;
            if (markdownItemNode != null)
            {
                this.htmlImageRelocator.Relocate(contentNode, markdownItemNode.MarkdownContent);
                return markdownItemNode.MarkdownContent;
            }

            throw new InvalidOperationException("Cannot format a FeatureNode with a Type of " + contentNode.GetType() +
                                                " as content");
        }
    }
}