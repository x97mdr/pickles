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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Pickles.DocumentationBuilders.Word
{
    public class WordHeaderFooterFormatter
    {
        private readonly Configuration configuration;

        public WordHeaderFooterFormatter(Configuration configuration)
        {
            this.configuration = configuration;
        }

        public void ApplyHeaderAndFooter(WordprocessingDocument wordProcessingDocument)
        {
            var headerPart = wordProcessingDocument.MainDocumentPart.AddNewPart<HeaderPart>();
            ApplyHeader(headerPart);

            var footerPart = wordProcessingDocument.MainDocumentPart.AddNewPart<FooterPart>();
            ApplyFooter(footerPart);
        }

        private void ApplyFooter(FooterPart footerPart)
        {
            Footer footer1 = new Footer();
            footer1.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            footer1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            footer1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            footer1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            footer1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            footer1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            footer1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            footer1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            footer1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

            Paragraph paragraph1 = new Paragraph() { RsidParagraphAddition = "005641D2", RsidRunAdditionDefault = "005641D2" };

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId1 = new ParagraphStyleId() { Val = "Footer" };

            paragraphProperties1.Append(paragraphStyleId1);

            Run run1 = new Run();
            Text text1 = new Text();
            text1.Text = "Generated with Pickles " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            run1.Append(text1);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);

            footer1.Append(paragraph1);

            footerPart.Footer = footer1;
        }

        private void ApplyHeader(HeaderPart headerPart)
        {
            Header header1 = new Header();
            header1.AddNamespaceDeclaration("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            header1.AddNamespaceDeclaration("o", "urn:schemas-microsoft-com:office:office");
            header1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            header1.AddNamespaceDeclaration("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
            header1.AddNamespaceDeclaration("v", "urn:schemas-microsoft-com:vml");
            header1.AddNamespaceDeclaration("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
            header1.AddNamespaceDeclaration("w10", "urn:schemas-microsoft-com:office:word");
            header1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            header1.AddNamespaceDeclaration("wne", "http://schemas.microsoft.com/office/word/2006/wordml");

            Paragraph paragraph1 = new Paragraph() { RsidParagraphAddition = "005641D2", RsidRunAdditionDefault = "005641D2" };

            ParagraphProperties paragraphProperties1 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId1 = new ParagraphStyleId() { Val = "Header" };

            paragraphProperties1.Append(paragraphStyleId1);

            Run run1 = new Run();
            Text text1 = new Text();

            if (!string.IsNullOrEmpty(this.configuration.SystemUnderTestName) && !string.IsNullOrEmpty(this.configuration.SystemUnderTestVersion))
            {
                text1.Text = string.Format("{0}, version {1}", this.configuration.SystemUnderTestName, this.configuration.SystemUnderTestVersion);
            }
            else if (!string.IsNullOrEmpty(this.configuration.SystemUnderTestName))
            {
                text1.Text =  this.configuration.SystemUnderTestName;
            }
            else if (!string.IsNullOrEmpty(this.configuration.SystemUnderTestVersion))
            {
                text1.Text =  string.Format("Features for version {0}", this.configuration.SystemUnderTestVersion);
            }

            run1.Append(text1);

            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run1);

            Paragraph paragraph2 = new Paragraph() { RsidParagraphAddition = "005641D2", RsidRunAdditionDefault = "005641D2" };

            ParagraphProperties paragraphProperties2 = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId2 = new ParagraphStyleId() { Val = "Header" };

            paragraphProperties2.Append(paragraphStyleId2);

            paragraph2.Append(paragraphProperties2);

            header1.Append(paragraph1);
            header1.Append(paragraph2);

            headerPart.Header = header1;
        }
    }
}
