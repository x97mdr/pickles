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
using System.IO;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NGenerics.Patterns.Visitor;
using Pickles.DirectoryCrawler;
using Pickles.Extensions;
using System.Xml.Linq;
using OpenXmlPowerTools;

namespace Pickles.DocumentationBuilders.Word
{
    public class WordDocumentationBuilder : IDocumentationBuilder
    {
        private readonly Configuration configuration;
        private readonly WordFeatureFormatter wordFeatureFormatter;
        private readonly WordStyleApplicator wordStyleApplicator;
        private readonly WordFontApplicator wordFontApplicator;
        private readonly WordHeaderFooterFormatter wordHeaderFooterFormatter;

        public WordDocumentationBuilder(Configuration configuration, WordFeatureFormatter wordFeatureFormatter, WordStyleApplicator wordStyleApplicator, WordFontApplicator wordFontApplicator, WordHeaderFooterFormatter wordHeaderFooterFormatter)
        {
            this.configuration = configuration;
            this.wordFeatureFormatter = wordFeatureFormatter;
            this.wordStyleApplicator = wordStyleApplicator;
            this.wordFontApplicator = wordFontApplicator;
            this.wordHeaderFooterFormatter = wordHeaderFooterFormatter;
        }

        #region IDocumentationBuilder Members

        public void Build(NGenerics.DataStructures.Trees.GeneralTree<DirectoryCrawler.IDirectoryTreeNode> features)
        {
            string filename = string.IsNullOrEmpty(this.configuration.SystemUnderTestName) ? "features.docx" : this.configuration.SystemUnderTestName + ".docx";
            var documentFileName = Path.Combine(this.configuration.OutputFolder.FullName, filename);
            if (File.Exists(documentFileName)) File.Delete(documentFileName);

            using (var wordProcessingDocument = WordprocessingDocument.Create(documentFileName, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                var mainDocumentPart = wordProcessingDocument.AddMainDocumentPart();
                this.wordStyleApplicator.AddStylesPartToPackage(wordProcessingDocument);
                this.wordStyleApplicator.AddStylesWithEffectsPartToPackage(wordProcessingDocument);
                this.wordFontApplicator.AddFontTablePartToPackage(wordProcessingDocument);
                var documentSettingsPart = mainDocumentPart.AddNewPart<DocumentSettingsPart>();
                documentSettingsPart.Settings = new Settings();
                this.wordHeaderFooterFormatter.ApplyHeaderAndFooter(wordProcessingDocument);
                
                var document = new Document();
                var body = new Body();
                document.Append(body);

                var actionVisitor = new ActionVisitor<IDirectoryTreeNode>(node =>
                {
                    var featureDirectoryTreeNode = node as FeatureDirectoryTreeNode;
                    if (featureDirectoryTreeNode != null)
                    {
                        this.wordFeatureFormatter.Format(body, featureDirectoryTreeNode);
                    }
                });

                features.AcceptVisitor(actionVisitor);

                mainDocumentPart.Document = document;
                mainDocumentPart.Document.Save();
            }

            // HACK - Add the table of contents
            using (var wordProcessingDocument = WordprocessingDocument.Open(documentFileName, true))
            {
                XElement firstPara = wordProcessingDocument
                                    .MainDocumentPart
                                    .GetXDocument()
                                    .Descendants(W.p)
                                    .FirstOrDefault();

                TocAdder.AddToc(wordProcessingDocument, firstPara, @"TOC \o '1-2' \h \z \u", null, 4);
            }
        }

        #endregion
    }
}
