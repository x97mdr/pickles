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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using NGenerics.DataStructures.Trees;
using Pickles.DirectoryCrawler;

namespace Pickles.DocumentationBuilders.HTML
{
    public class HtmlContentFormatter
    {
        private readonly HtmlFeatureFormatter htmlFeatureFormatter;
        private readonly HtmlMarkdownFormatter htmlMarkdownFormatter;
      private readonly HtmlIndexFormatter htmlIndexFormatter;

        public HtmlContentFormatter(HtmlFeatureFormatter htmlFeatureFormatter, HtmlMarkdownFormatter htmlMarkdownFormatter, HtmlIndexFormatter htmlIndexFormatter)
        {
            this.htmlFeatureFormatter = htmlFeatureFormatter;
            this.htmlMarkdownFormatter = htmlMarkdownFormatter;
          this.htmlIndexFormatter = htmlIndexFormatter;
        }

        public XElement Format(IDirectoryTreeNode contentNode, IEnumerable<IDirectoryTreeNode> features)
        {
            var xmlns = HtmlNamespace.Xhtml;

            var featureItemNode = contentNode as FeatureDirectoryTreeNode;
            if (featureItemNode != null)
            {
                return this.htmlFeatureFormatter.Format(featureItemNode.Feature);
            }

          var indexItemNode = contentNode as FolderDirectoryTreeNode;
          if (indexItemNode != null)
          {
            return this.htmlIndexFormatter.Format(indexItemNode, features);
          }

            var markdownItemNode = contentNode as MarkdownTreeNode;
            if (markdownItemNode != null)
            {
              return markdownItemNode.MarkdownContent;
            }

          throw new InvalidOperationException("Cannot format a FeatureNode with a Type of " + contentNode.GetType() + " as content");
        }
    }
}
