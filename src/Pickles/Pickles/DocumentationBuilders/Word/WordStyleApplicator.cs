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
using DocumentFormat.OpenXml;

namespace Pickles.DocumentationBuilders.Word
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Most of the code in this class was generated using the Open XML Productivity Tool</remarks>
    public class WordStyleApplicator
    {
        // Apply a style to a paragraph.
        public void ApplyStyleToParagraph(WordprocessingDocument doc, string styleid, string stylename, Paragraph p)
        {
            // If the paragraph has no ParagraphProperties object, create one.
            if (p.Elements<ParagraphProperties>().Count() == 0)
            {
                p.PrependChild<ParagraphProperties>(new ParagraphProperties());
            }

            // Get the paragraph properties element of the paragraph.
            ParagraphProperties pPr = p.Elements<ParagraphProperties>().First();

            // Get the Styles part for this document.
            StyleDefinitionsPart part =
                doc.MainDocumentPart.StyleDefinitionsPart;

            // If the Styles part does not exist, add it and then add the style.
            if (part == null)
            {
                part = AddStylesPartToPackage(doc);
                AddNewStyle(part, styleid, stylename);
            }
            else
            {
                // If the style is not in the document, add it.
                if (IsStyleIdInDocument(doc, styleid) != true)
                {
                    // No match on styleid, so let's try style name.
                    string styleidFromName = GetStyleIdFromStyleName(doc, stylename);
                    if (styleidFromName == null)
                    {
                        AddNewStyle(part, styleid, stylename);
                    }
                    else
                        styleid = styleidFromName;
                }
            }

            // Set the style of the paragraph.
            pPr.ParagraphStyleId = new ParagraphStyleId() { Val = styleid };
        }

        // Return true if the style id is in the document, false otherwise.
        public bool IsStyleIdInDocument(WordprocessingDocument doc, string styleid)
        {
            // Get access to the Styles element for this document.
            Styles s = doc.MainDocumentPart.StyleDefinitionsPart.Styles;

            // Check that there are styles and how many.
            int n = s.Elements<Style>().Count();
            if (n == 0)
                return false;

            // Look for a match on styleid.
            Style style = s.Elements<Style>()
                .Where(st => (st.StyleId == styleid) && (st.Type == StyleValues.Paragraph))
                .FirstOrDefault();
            if (style == null)
                return false;

            return true;
        }

        // Return styleid that matches the styleName, or null when there's no match.
        public string GetStyleIdFromStyleName(WordprocessingDocument doc, string styleName)
        {
            StyleDefinitionsPart stylePart = doc.MainDocumentPart.StyleDefinitionsPart;
            string styleId = stylePart.Styles.Descendants<StyleName>()
                .Where(s => s.Val.Value.Equals(styleName) &&
                    (((Style)s.Parent).Type == StyleValues.Paragraph))
                .Select(n => ((Style)n.Parent).StyleId).FirstOrDefault();
            return styleId;
        }

        // Create a new style with the specified styleid and stylename and add it to the specified
        // style definitions part.
        private static void AddNewStyle(StyleDefinitionsPart styleDefinitionsPart,
            string styleid, string stylename)
        {
            // Get access to the root element of the styles part.
            Styles styles = styleDefinitionsPart.Styles;

            // Create a new paragraph style and specify some of the properties.
            Style style = new Style()
            {
                Type = StyleValues.Paragraph,
                StyleId = styleid,
                CustomStyle = true
            };
            StyleName styleName1 = new StyleName() { Val = stylename };
            BasedOn basedOn1 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            style.Append(styleName1);
            style.Append(basedOn1);
            style.Append(nextParagraphStyle1);

            // Create the StyleRunProperties object and specify some of the run properties.
            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            Bold bold1 = new Bold();
            Color color1 = new Color() { ThemeColor = ThemeColorValues.Accent2 };
            RunFonts font1 = new RunFonts() { Ascii = "Lucida Console" };
            Italic italic1 = new Italic();
            // Specify a 12 point size.
            FontSize fontSize1 = new FontSize() { Val = "24" };
            styleRunProperties1.Append(bold1);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(font1);
            styleRunProperties1.Append(fontSize1);
            styleRunProperties1.Append(italic1);

            // Add the run properties to the style.
            style.Append(styleRunProperties1);

            // Add the style to the styles part.
            styles.Append(style);
        }

        // Add a StylesDefinitionsPart to the document.  Returns a reference to it.
        public StyleDefinitionsPart AddStylesPartToPackage(WordprocessingDocument doc)
        {
            StyleDefinitionsPart part = doc.MainDocumentPart.AddNewPart<StyleDefinitionsPart>();
            GeneratePartContent(part);
            Styles root = new Styles();
            root.Save(part);
            return part;
        }

        // Generates content of part.
        private void GeneratePartContent(StyleDefinitionsPart part)
        {
            Styles styles1 = new Styles();
            styles1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            styles1.AddNamespaceDeclaration("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");

            DocDefaults docDefaults1 = new DocDefaults();

            RunPropertiesDefault runPropertiesDefault1 = new RunPropertiesDefault();

            RunPropertiesBaseStyle runPropertiesBaseStyle1 = new RunPropertiesBaseStyle();
            RunFonts runFonts1 = new RunFonts() { AsciiTheme = ThemeFontValues.MinorHighAnsi, HighAnsiTheme = ThemeFontValues.MinorHighAnsi, EastAsiaTheme = ThemeFontValues.MinorHighAnsi, ComplexScriptTheme = ThemeFontValues.MinorBidi };
            FontSize fontSize1 = new FontSize() { Val = "22" };
            FontSizeComplexScript fontSizeComplexScript1 = new FontSizeComplexScript() { Val = "22" };
            Languages languages1 = new Languages() { Val = "en-US", EastAsia = "en-US", Bidi = "ar-SA" };

            runPropertiesBaseStyle1.Append(runFonts1);
            runPropertiesBaseStyle1.Append(fontSize1);
            runPropertiesBaseStyle1.Append(fontSizeComplexScript1);
            runPropertiesBaseStyle1.Append(languages1);

            runPropertiesDefault1.Append(runPropertiesBaseStyle1);

            ParagraphPropertiesDefault paragraphPropertiesDefault1 = new ParagraphPropertiesDefault();

            ParagraphPropertiesBaseStyle paragraphPropertiesBaseStyle1 = new ParagraphPropertiesBaseStyle();
            SpacingBetweenLines spacingBetweenLines1 = new SpacingBetweenLines() { After = "200", Line = "276", LineRule = LineSpacingRuleValues.Auto };

            paragraphPropertiesBaseStyle1.Append(spacingBetweenLines1);

            paragraphPropertiesDefault1.Append(paragraphPropertiesBaseStyle1);

            docDefaults1.Append(runPropertiesDefault1);
            docDefaults1.Append(paragraphPropertiesDefault1);

            LatentStyles latentStyles1 = new LatentStyles() { DefaultLockedState = false, DefaultUiPriority = 99, DefaultSemiHidden = true, DefaultUnhideWhenUsed = true, DefaultPrimaryStyle = false, Count = 267 };
            LatentStyleExceptionInfo latentStyleExceptionInfo1 = new LatentStyleExceptionInfo() { Name = "Normal", UiPriority = 0, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo2 = new LatentStyleExceptionInfo() { Name = "heading 1", UiPriority = 9, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo3 = new LatentStyleExceptionInfo() { Name = "heading 2", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo4 = new LatentStyleExceptionInfo() { Name = "heading 3", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo5 = new LatentStyleExceptionInfo() { Name = "heading 4", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo6 = new LatentStyleExceptionInfo() { Name = "heading 5", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo7 = new LatentStyleExceptionInfo() { Name = "heading 6", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo8 = new LatentStyleExceptionInfo() { Name = "heading 7", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo9 = new LatentStyleExceptionInfo() { Name = "heading 8", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo10 = new LatentStyleExceptionInfo() { Name = "heading 9", UiPriority = 9, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo11 = new LatentStyleExceptionInfo() { Name = "toc 1", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo12 = new LatentStyleExceptionInfo() { Name = "toc 2", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo13 = new LatentStyleExceptionInfo() { Name = "toc 3", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo14 = new LatentStyleExceptionInfo() { Name = "toc 4", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo15 = new LatentStyleExceptionInfo() { Name = "toc 5", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo16 = new LatentStyleExceptionInfo() { Name = "toc 6", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo17 = new LatentStyleExceptionInfo() { Name = "toc 7", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo18 = new LatentStyleExceptionInfo() { Name = "toc 8", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo19 = new LatentStyleExceptionInfo() { Name = "toc 9", UiPriority = 39 };
            LatentStyleExceptionInfo latentStyleExceptionInfo20 = new LatentStyleExceptionInfo() { Name = "caption", UiPriority = 35, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo21 = new LatentStyleExceptionInfo() { Name = "Title", UiPriority = 10, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo22 = new LatentStyleExceptionInfo() { Name = "Default Paragraph Font", UiPriority = 1 };
            LatentStyleExceptionInfo latentStyleExceptionInfo23 = new LatentStyleExceptionInfo() { Name = "Subtitle", UiPriority = 11, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo24 = new LatentStyleExceptionInfo() { Name = "Strong", UiPriority = 22, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo25 = new LatentStyleExceptionInfo() { Name = "Emphasis", UiPriority = 20, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo26 = new LatentStyleExceptionInfo() { Name = "Table Grid", UiPriority = 59, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo27 = new LatentStyleExceptionInfo() { Name = "Placeholder Text", UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo28 = new LatentStyleExceptionInfo() { Name = "No Spacing", UiPriority = 1, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo29 = new LatentStyleExceptionInfo() { Name = "Light Shading", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo30 = new LatentStyleExceptionInfo() { Name = "Light List", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo31 = new LatentStyleExceptionInfo() { Name = "Light Grid", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo32 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo33 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo34 = new LatentStyleExceptionInfo() { Name = "Medium List 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo35 = new LatentStyleExceptionInfo() { Name = "Medium List 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo36 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo37 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo38 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo39 = new LatentStyleExceptionInfo() { Name = "Dark List", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo40 = new LatentStyleExceptionInfo() { Name = "Colorful Shading", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo41 = new LatentStyleExceptionInfo() { Name = "Colorful List", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo42 = new LatentStyleExceptionInfo() { Name = "Colorful Grid", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo43 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 1", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo44 = new LatentStyleExceptionInfo() { Name = "Light List Accent 1", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo45 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 1", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo46 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 1", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo47 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 1", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo48 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 1", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo49 = new LatentStyleExceptionInfo() { Name = "Revision", UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo50 = new LatentStyleExceptionInfo() { Name = "List Paragraph", UiPriority = 34, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo51 = new LatentStyleExceptionInfo() { Name = "Quote", UiPriority = 29, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo52 = new LatentStyleExceptionInfo() { Name = "Intense Quote", UiPriority = 30, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo53 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 1", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo54 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 1", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo55 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 1", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo56 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 1", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo57 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 1", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo58 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 1", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo59 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 1", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo60 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 1", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo61 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 2", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo62 = new LatentStyleExceptionInfo() { Name = "Light List Accent 2", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo63 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 2", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo64 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 2", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo65 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 2", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo66 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 2", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo67 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 2", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo68 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 2", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo69 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 2", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo70 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 2", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo71 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 2", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo72 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 2", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo73 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 2", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo74 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 2", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo75 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 3", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo76 = new LatentStyleExceptionInfo() { Name = "Light List Accent 3", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo77 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 3", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo78 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 3", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo79 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 3", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo80 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 3", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo81 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 3", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo82 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 3", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo83 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 3", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo84 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 3", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo85 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 3", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo86 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 3", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo87 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 3", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo88 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 3", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo89 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 4", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo90 = new LatentStyleExceptionInfo() { Name = "Light List Accent 4", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo91 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 4", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo92 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 4", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo93 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 4", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo94 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 4", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo95 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 4", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo96 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 4", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo97 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 4", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo98 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 4", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo99 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 4", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo100 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 4", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo101 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 4", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo102 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 4", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo103 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 5", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo104 = new LatentStyleExceptionInfo() { Name = "Light List Accent 5", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo105 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 5", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo106 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 5", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo107 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 5", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo108 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 5", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo109 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 5", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo110 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 5", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo111 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 5", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo112 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 5", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo113 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 5", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo114 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 5", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo115 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 5", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo116 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 5", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo117 = new LatentStyleExceptionInfo() { Name = "Light Shading Accent 6", UiPriority = 60, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo118 = new LatentStyleExceptionInfo() { Name = "Light List Accent 6", UiPriority = 61, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo119 = new LatentStyleExceptionInfo() { Name = "Light Grid Accent 6", UiPriority = 62, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo120 = new LatentStyleExceptionInfo() { Name = "Medium Shading 1 Accent 6", UiPriority = 63, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo121 = new LatentStyleExceptionInfo() { Name = "Medium Shading 2 Accent 6", UiPriority = 64, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo122 = new LatentStyleExceptionInfo() { Name = "Medium List 1 Accent 6", UiPriority = 65, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo123 = new LatentStyleExceptionInfo() { Name = "Medium List 2 Accent 6", UiPriority = 66, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo124 = new LatentStyleExceptionInfo() { Name = "Medium Grid 1 Accent 6", UiPriority = 67, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo125 = new LatentStyleExceptionInfo() { Name = "Medium Grid 2 Accent 6", UiPriority = 68, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo126 = new LatentStyleExceptionInfo() { Name = "Medium Grid 3 Accent 6", UiPriority = 69, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo127 = new LatentStyleExceptionInfo() { Name = "Dark List Accent 6", UiPriority = 70, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo128 = new LatentStyleExceptionInfo() { Name = "Colorful Shading Accent 6", UiPriority = 71, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo129 = new LatentStyleExceptionInfo() { Name = "Colorful List Accent 6", UiPriority = 72, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo130 = new LatentStyleExceptionInfo() { Name = "Colorful Grid Accent 6", UiPriority = 73, SemiHidden = false, UnhideWhenUsed = false };
            LatentStyleExceptionInfo latentStyleExceptionInfo131 = new LatentStyleExceptionInfo() { Name = "Subtle Emphasis", UiPriority = 19, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo132 = new LatentStyleExceptionInfo() { Name = "Intense Emphasis", UiPriority = 21, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo133 = new LatentStyleExceptionInfo() { Name = "Subtle Reference", UiPriority = 31, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo134 = new LatentStyleExceptionInfo() { Name = "Intense Reference", UiPriority = 32, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo135 = new LatentStyleExceptionInfo() { Name = "Book Title", UiPriority = 33, SemiHidden = false, UnhideWhenUsed = false, PrimaryStyle = true };
            LatentStyleExceptionInfo latentStyleExceptionInfo136 = new LatentStyleExceptionInfo() { Name = "Bibliography", UiPriority = 37 };
            LatentStyleExceptionInfo latentStyleExceptionInfo137 = new LatentStyleExceptionInfo() { Name = "TOC Heading", UiPriority = 39, PrimaryStyle = true };

            latentStyles1.Append(latentStyleExceptionInfo1);
            latentStyles1.Append(latentStyleExceptionInfo2);
            latentStyles1.Append(latentStyleExceptionInfo3);
            latentStyles1.Append(latentStyleExceptionInfo4);
            latentStyles1.Append(latentStyleExceptionInfo5);
            latentStyles1.Append(latentStyleExceptionInfo6);
            latentStyles1.Append(latentStyleExceptionInfo7);
            latentStyles1.Append(latentStyleExceptionInfo8);
            latentStyles1.Append(latentStyleExceptionInfo9);
            latentStyles1.Append(latentStyleExceptionInfo10);
            latentStyles1.Append(latentStyleExceptionInfo11);
            latentStyles1.Append(latentStyleExceptionInfo12);
            latentStyles1.Append(latentStyleExceptionInfo13);
            latentStyles1.Append(latentStyleExceptionInfo14);
            latentStyles1.Append(latentStyleExceptionInfo15);
            latentStyles1.Append(latentStyleExceptionInfo16);
            latentStyles1.Append(latentStyleExceptionInfo17);
            latentStyles1.Append(latentStyleExceptionInfo18);
            latentStyles1.Append(latentStyleExceptionInfo19);
            latentStyles1.Append(latentStyleExceptionInfo20);
            latentStyles1.Append(latentStyleExceptionInfo21);
            latentStyles1.Append(latentStyleExceptionInfo22);
            latentStyles1.Append(latentStyleExceptionInfo23);
            latentStyles1.Append(latentStyleExceptionInfo24);
            latentStyles1.Append(latentStyleExceptionInfo25);
            latentStyles1.Append(latentStyleExceptionInfo26);
            latentStyles1.Append(latentStyleExceptionInfo27);
            latentStyles1.Append(latentStyleExceptionInfo28);
            latentStyles1.Append(latentStyleExceptionInfo29);
            latentStyles1.Append(latentStyleExceptionInfo30);
            latentStyles1.Append(latentStyleExceptionInfo31);
            latentStyles1.Append(latentStyleExceptionInfo32);
            latentStyles1.Append(latentStyleExceptionInfo33);
            latentStyles1.Append(latentStyleExceptionInfo34);
            latentStyles1.Append(latentStyleExceptionInfo35);
            latentStyles1.Append(latentStyleExceptionInfo36);
            latentStyles1.Append(latentStyleExceptionInfo37);
            latentStyles1.Append(latentStyleExceptionInfo38);
            latentStyles1.Append(latentStyleExceptionInfo39);
            latentStyles1.Append(latentStyleExceptionInfo40);
            latentStyles1.Append(latentStyleExceptionInfo41);
            latentStyles1.Append(latentStyleExceptionInfo42);
            latentStyles1.Append(latentStyleExceptionInfo43);
            latentStyles1.Append(latentStyleExceptionInfo44);
            latentStyles1.Append(latentStyleExceptionInfo45);
            latentStyles1.Append(latentStyleExceptionInfo46);
            latentStyles1.Append(latentStyleExceptionInfo47);
            latentStyles1.Append(latentStyleExceptionInfo48);
            latentStyles1.Append(latentStyleExceptionInfo49);
            latentStyles1.Append(latentStyleExceptionInfo50);
            latentStyles1.Append(latentStyleExceptionInfo51);
            latentStyles1.Append(latentStyleExceptionInfo52);
            latentStyles1.Append(latentStyleExceptionInfo53);
            latentStyles1.Append(latentStyleExceptionInfo54);
            latentStyles1.Append(latentStyleExceptionInfo55);
            latentStyles1.Append(latentStyleExceptionInfo56);
            latentStyles1.Append(latentStyleExceptionInfo57);
            latentStyles1.Append(latentStyleExceptionInfo58);
            latentStyles1.Append(latentStyleExceptionInfo59);
            latentStyles1.Append(latentStyleExceptionInfo60);
            latentStyles1.Append(latentStyleExceptionInfo61);
            latentStyles1.Append(latentStyleExceptionInfo62);
            latentStyles1.Append(latentStyleExceptionInfo63);
            latentStyles1.Append(latentStyleExceptionInfo64);
            latentStyles1.Append(latentStyleExceptionInfo65);
            latentStyles1.Append(latentStyleExceptionInfo66);
            latentStyles1.Append(latentStyleExceptionInfo67);
            latentStyles1.Append(latentStyleExceptionInfo68);
            latentStyles1.Append(latentStyleExceptionInfo69);
            latentStyles1.Append(latentStyleExceptionInfo70);
            latentStyles1.Append(latentStyleExceptionInfo71);
            latentStyles1.Append(latentStyleExceptionInfo72);
            latentStyles1.Append(latentStyleExceptionInfo73);
            latentStyles1.Append(latentStyleExceptionInfo74);
            latentStyles1.Append(latentStyleExceptionInfo75);
            latentStyles1.Append(latentStyleExceptionInfo76);
            latentStyles1.Append(latentStyleExceptionInfo77);
            latentStyles1.Append(latentStyleExceptionInfo78);
            latentStyles1.Append(latentStyleExceptionInfo79);
            latentStyles1.Append(latentStyleExceptionInfo80);
            latentStyles1.Append(latentStyleExceptionInfo81);
            latentStyles1.Append(latentStyleExceptionInfo82);
            latentStyles1.Append(latentStyleExceptionInfo83);
            latentStyles1.Append(latentStyleExceptionInfo84);
            latentStyles1.Append(latentStyleExceptionInfo85);
            latentStyles1.Append(latentStyleExceptionInfo86);
            latentStyles1.Append(latentStyleExceptionInfo87);
            latentStyles1.Append(latentStyleExceptionInfo88);
            latentStyles1.Append(latentStyleExceptionInfo89);
            latentStyles1.Append(latentStyleExceptionInfo90);
            latentStyles1.Append(latentStyleExceptionInfo91);
            latentStyles1.Append(latentStyleExceptionInfo92);
            latentStyles1.Append(latentStyleExceptionInfo93);
            latentStyles1.Append(latentStyleExceptionInfo94);
            latentStyles1.Append(latentStyleExceptionInfo95);
            latentStyles1.Append(latentStyleExceptionInfo96);
            latentStyles1.Append(latentStyleExceptionInfo97);
            latentStyles1.Append(latentStyleExceptionInfo98);
            latentStyles1.Append(latentStyleExceptionInfo99);
            latentStyles1.Append(latentStyleExceptionInfo100);
            latentStyles1.Append(latentStyleExceptionInfo101);
            latentStyles1.Append(latentStyleExceptionInfo102);
            latentStyles1.Append(latentStyleExceptionInfo103);
            latentStyles1.Append(latentStyleExceptionInfo104);
            latentStyles1.Append(latentStyleExceptionInfo105);
            latentStyles1.Append(latentStyleExceptionInfo106);
            latentStyles1.Append(latentStyleExceptionInfo107);
            latentStyles1.Append(latentStyleExceptionInfo108);
            latentStyles1.Append(latentStyleExceptionInfo109);
            latentStyles1.Append(latentStyleExceptionInfo110);
            latentStyles1.Append(latentStyleExceptionInfo111);
            latentStyles1.Append(latentStyleExceptionInfo112);
            latentStyles1.Append(latentStyleExceptionInfo113);
            latentStyles1.Append(latentStyleExceptionInfo114);
            latentStyles1.Append(latentStyleExceptionInfo115);
            latentStyles1.Append(latentStyleExceptionInfo116);
            latentStyles1.Append(latentStyleExceptionInfo117);
            latentStyles1.Append(latentStyleExceptionInfo118);
            latentStyles1.Append(latentStyleExceptionInfo119);
            latentStyles1.Append(latentStyleExceptionInfo120);
            latentStyles1.Append(latentStyleExceptionInfo121);
            latentStyles1.Append(latentStyleExceptionInfo122);
            latentStyles1.Append(latentStyleExceptionInfo123);
            latentStyles1.Append(latentStyleExceptionInfo124);
            latentStyles1.Append(latentStyleExceptionInfo125);
            latentStyles1.Append(latentStyleExceptionInfo126);
            latentStyles1.Append(latentStyleExceptionInfo127);
            latentStyles1.Append(latentStyleExceptionInfo128);
            latentStyles1.Append(latentStyleExceptionInfo129);
            latentStyles1.Append(latentStyleExceptionInfo130);
            latentStyles1.Append(latentStyleExceptionInfo131);
            latentStyles1.Append(latentStyleExceptionInfo132);
            latentStyles1.Append(latentStyleExceptionInfo133);
            latentStyles1.Append(latentStyleExceptionInfo134);
            latentStyles1.Append(latentStyleExceptionInfo135);
            latentStyles1.Append(latentStyleExceptionInfo136);
            latentStyles1.Append(latentStyleExceptionInfo137);

            Style style1 = new Style() { Type = StyleValues.Paragraph, StyleId = "Normal", Default = true };
            StyleName styleName1 = new StyleName() { Val = "Normal" };
            PrimaryStyle primaryStyle1 = new PrimaryStyle();
            Rsid rsid1 = new Rsid() { Val = "00BA40EF" };

            StyleParagraphProperties styleParagraphProperties1 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines2 = new SpacingBetweenLines() { After = "0" };

            styleParagraphProperties1.Append(spacingBetweenLines2);

            style1.Append(styleName1);
            style1.Append(primaryStyle1);
            style1.Append(rsid1);
            style1.Append(styleParagraphProperties1);

            Style style2 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading1" };
            StyleName styleName2 = new StyleName() { Val = "heading 1" };
            BasedOn basedOn1 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle1 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle1 = new LinkedStyle() { Val = "Heading1Char" };
            UIPriority uIPriority1 = new UIPriority() { Val = 9 };
            PrimaryStyle primaryStyle2 = new PrimaryStyle();
            Rsid rsid2 = new Rsid() { Val = "0016335E" };

            StyleParagraphProperties styleParagraphProperties2 = new StyleParagraphProperties();
            KeepNext keepNext1 = new KeepNext();
            KeepLines keepLines1 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines3 = new SpacingBetweenLines() { Before = "480" };
            OutlineLevel outlineLevel1 = new OutlineLevel() { Val = 0 };

            styleParagraphProperties2.Append(keepNext1);
            styleParagraphProperties2.Append(keepLines1);
            styleParagraphProperties2.Append(spacingBetweenLines3);
            styleParagraphProperties2.Append(outlineLevel1);

            StyleRunProperties styleRunProperties1 = new StyleRunProperties();
            RunFonts runFonts2 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold1 = new Bold();
            BoldComplexScript boldComplexScript1 = new BoldComplexScript();
            Color color1 = new Color() { Val = "365F91", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "BF" };
            FontSize fontSize2 = new FontSize() { Val = "28" };
            FontSizeComplexScript fontSizeComplexScript2 = new FontSizeComplexScript() { Val = "28" };

            styleRunProperties1.Append(runFonts2);
            styleRunProperties1.Append(bold1);
            styleRunProperties1.Append(boldComplexScript1);
            styleRunProperties1.Append(color1);
            styleRunProperties1.Append(fontSize2);
            styleRunProperties1.Append(fontSizeComplexScript2);

            style2.Append(styleName2);
            style2.Append(basedOn1);
            style2.Append(nextParagraphStyle1);
            style2.Append(linkedStyle1);
            style2.Append(uIPriority1);
            style2.Append(primaryStyle2);
            style2.Append(rsid2);
            style2.Append(styleParagraphProperties2);
            style2.Append(styleRunProperties1);

            Style style3 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading2" };
            StyleName styleName3 = new StyleName() { Val = "heading 2" };
            BasedOn basedOn2 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle2 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle2 = new LinkedStyle() { Val = "Heading2Char" };
            UIPriority uIPriority2 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed1 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle3 = new PrimaryStyle();
            Rsid rsid3 = new Rsid() { Val = "0016335E" };

            StyleParagraphProperties styleParagraphProperties3 = new StyleParagraphProperties();
            KeepNext keepNext2 = new KeepNext();
            KeepLines keepLines2 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines4 = new SpacingBetweenLines() { Before = "200" };
            OutlineLevel outlineLevel2 = new OutlineLevel() { Val = 1 };

            styleParagraphProperties3.Append(keepNext2);
            styleParagraphProperties3.Append(keepLines2);
            styleParagraphProperties3.Append(spacingBetweenLines4);
            styleParagraphProperties3.Append(outlineLevel2);

            StyleRunProperties styleRunProperties2 = new StyleRunProperties();
            RunFonts runFonts3 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold2 = new Bold();
            BoldComplexScript boldComplexScript2 = new BoldComplexScript();
            Color color2 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };
            FontSize fontSize3 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript3 = new FontSizeComplexScript() { Val = "26" };

            styleRunProperties2.Append(runFonts3);
            styleRunProperties2.Append(bold2);
            styleRunProperties2.Append(boldComplexScript2);
            styleRunProperties2.Append(color2);
            styleRunProperties2.Append(fontSize3);
            styleRunProperties2.Append(fontSizeComplexScript3);

            style3.Append(styleName3);
            style3.Append(basedOn2);
            style3.Append(nextParagraphStyle2);
            style3.Append(linkedStyle2);
            style3.Append(uIPriority2);
            style3.Append(unhideWhenUsed1);
            style3.Append(primaryStyle3);
            style3.Append(rsid3);
            style3.Append(styleParagraphProperties3);
            style3.Append(styleRunProperties2);

            Style style4 = new Style() { Type = StyleValues.Paragraph, StyleId = "Heading3" };
            StyleName styleName4 = new StyleName() { Val = "heading 3" };
            BasedOn basedOn3 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle3 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle3 = new LinkedStyle() { Val = "Heading3Char" };
            UIPriority uIPriority3 = new UIPriority() { Val = 9 };
            UnhideWhenUsed unhideWhenUsed2 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle4 = new PrimaryStyle();
            Rsid rsid4 = new Rsid() { Val = "0016335E" };

            StyleParagraphProperties styleParagraphProperties4 = new StyleParagraphProperties();
            KeepNext keepNext3 = new KeepNext();
            KeepLines keepLines3 = new KeepLines();
            SpacingBetweenLines spacingBetweenLines5 = new SpacingBetweenLines() { Before = "200" };
            OutlineLevel outlineLevel3 = new OutlineLevel() { Val = 2 };

            styleParagraphProperties4.Append(keepNext3);
            styleParagraphProperties4.Append(keepLines3);
            styleParagraphProperties4.Append(spacingBetweenLines5);
            styleParagraphProperties4.Append(outlineLevel3);

            StyleRunProperties styleRunProperties3 = new StyleRunProperties();
            RunFonts runFonts4 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold3 = new Bold();
            BoldComplexScript boldComplexScript3 = new BoldComplexScript();
            Color color3 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };

            styleRunProperties3.Append(runFonts4);
            styleRunProperties3.Append(bold3);
            styleRunProperties3.Append(boldComplexScript3);
            styleRunProperties3.Append(color3);

            style4.Append(styleName4);
            style4.Append(basedOn3);
            style4.Append(nextParagraphStyle3);
            style4.Append(linkedStyle3);
            style4.Append(uIPriority3);
            style4.Append(unhideWhenUsed2);
            style4.Append(primaryStyle4);
            style4.Append(rsid4);
            style4.Append(styleParagraphProperties4);
            style4.Append(styleRunProperties3);

            Style style5 = new Style() { Type = StyleValues.Character, StyleId = "DefaultParagraphFont", Default = true };
            StyleName styleName5 = new StyleName() { Val = "Default Paragraph Font" };
            UIPriority uIPriority4 = new UIPriority() { Val = 1 };
            SemiHidden semiHidden1 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed3 = new UnhideWhenUsed();

            style5.Append(styleName5);
            style5.Append(uIPriority4);
            style5.Append(semiHidden1);
            style5.Append(unhideWhenUsed3);

            Style style6 = new Style() { Type = StyleValues.Table, StyleId = "TableNormal", Default = true };
            StyleName styleName6 = new StyleName() { Val = "Normal Table" };
            UIPriority uIPriority5 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden2 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed4 = new UnhideWhenUsed();
            PrimaryStyle primaryStyle5 = new PrimaryStyle();

            StyleTableProperties styleTableProperties1 = new StyleTableProperties();
            TableIndentation tableIndentation1 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
            TopMargin topMargin1 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin1 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin1 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin1 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault1.Append(topMargin1);
            tableCellMarginDefault1.Append(tableCellLeftMargin1);
            tableCellMarginDefault1.Append(bottomMargin1);
            tableCellMarginDefault1.Append(tableCellRightMargin1);

            styleTableProperties1.Append(tableIndentation1);
            styleTableProperties1.Append(tableCellMarginDefault1);

            style6.Append(styleName6);
            style6.Append(uIPriority5);
            style6.Append(semiHidden2);
            style6.Append(unhideWhenUsed4);
            style6.Append(primaryStyle5);
            style6.Append(styleTableProperties1);

            Style style7 = new Style() { Type = StyleValues.Numbering, StyleId = "NoList", Default = true };
            StyleName styleName7 = new StyleName() { Val = "No List" };
            UIPriority uIPriority6 = new UIPriority() { Val = 99 };
            SemiHidden semiHidden3 = new SemiHidden();
            UnhideWhenUsed unhideWhenUsed5 = new UnhideWhenUsed();

            style7.Append(styleName7);
            style7.Append(uIPriority6);
            style7.Append(semiHidden3);
            style7.Append(unhideWhenUsed5);

            Style style8 = new Style() { Type = StyleValues.Character, StyleId = "Heading1Char", CustomStyle = true };
            StyleName styleName8 = new StyleName() { Val = "Heading 1 Char" };
            BasedOn basedOn4 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle4 = new LinkedStyle() { Val = "Heading1" };
            UIPriority uIPriority7 = new UIPriority() { Val = 9 };
            Rsid rsid5 = new Rsid() { Val = "0016335E" };

            StyleRunProperties styleRunProperties4 = new StyleRunProperties();
            RunFonts runFonts5 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold4 = new Bold();
            BoldComplexScript boldComplexScript4 = new BoldComplexScript();
            Color color4 = new Color() { Val = "365F91", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "BF" };
            FontSize fontSize4 = new FontSize() { Val = "28" };
            FontSizeComplexScript fontSizeComplexScript4 = new FontSizeComplexScript() { Val = "28" };

            styleRunProperties4.Append(runFonts5);
            styleRunProperties4.Append(bold4);
            styleRunProperties4.Append(boldComplexScript4);
            styleRunProperties4.Append(color4);
            styleRunProperties4.Append(fontSize4);
            styleRunProperties4.Append(fontSizeComplexScript4);

            style8.Append(styleName8);
            style8.Append(basedOn4);
            style8.Append(linkedStyle4);
            style8.Append(uIPriority7);
            style8.Append(rsid5);
            style8.Append(styleRunProperties4);

            Style style9 = new Style() { Type = StyleValues.Character, StyleId = "Heading2Char", CustomStyle = true };
            StyleName styleName9 = new StyleName() { Val = "Heading 2 Char" };
            BasedOn basedOn5 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle5 = new LinkedStyle() { Val = "Heading2" };
            UIPriority uIPriority8 = new UIPriority() { Val = 9 };
            Rsid rsid6 = new Rsid() { Val = "0016335E" };

            StyleRunProperties styleRunProperties5 = new StyleRunProperties();
            RunFonts runFonts6 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold5 = new Bold();
            BoldComplexScript boldComplexScript5 = new BoldComplexScript();
            Color color5 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };
            FontSize fontSize5 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript5 = new FontSizeComplexScript() { Val = "26" };

            styleRunProperties5.Append(runFonts6);
            styleRunProperties5.Append(bold5);
            styleRunProperties5.Append(boldComplexScript5);
            styleRunProperties5.Append(color5);
            styleRunProperties5.Append(fontSize5);
            styleRunProperties5.Append(fontSizeComplexScript5);

            style9.Append(styleName9);
            style9.Append(basedOn5);
            style9.Append(linkedStyle5);
            style9.Append(uIPriority8);
            style9.Append(rsid6);
            style9.Append(styleRunProperties5);

            Style style10 = new Style() { Type = StyleValues.Character, StyleId = "Heading3Char", CustomStyle = true };
            StyleName styleName10 = new StyleName() { Val = "Heading 3 Char" };
            BasedOn basedOn6 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle6 = new LinkedStyle() { Val = "Heading3" };
            UIPriority uIPriority9 = new UIPriority() { Val = 9 };
            Rsid rsid7 = new Rsid() { Val = "0016335E" };

            StyleRunProperties styleRunProperties6 = new StyleRunProperties();
            RunFonts runFonts7 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold6 = new Bold();
            BoldComplexScript boldComplexScript6 = new BoldComplexScript();
            Color color6 = new Color() { Val = "4F81BD", ThemeColor = ThemeColorValues.Accent1 };

            styleRunProperties6.Append(runFonts7);
            styleRunProperties6.Append(bold6);
            styleRunProperties6.Append(boldComplexScript6);
            styleRunProperties6.Append(color6);

            style10.Append(styleName10);
            style10.Append(basedOn6);
            style10.Append(linkedStyle6);
            style10.Append(uIPriority9);
            style10.Append(rsid7);
            style10.Append(styleRunProperties6);

            Style style11 = new Style() { Type = StyleValues.Table, StyleId = "TableGrid" };
            StyleName styleName11 = new StyleName() { Val = "Table Grid" };
            BasedOn basedOn7 = new BasedOn() { Val = "TableNormal" };
            UIPriority uIPriority10 = new UIPriority() { Val = 59 };
            Rsid rsid8 = new Rsid() { Val = "00BA40EF" };

            StyleParagraphProperties styleParagraphProperties5 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines6 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties5.Append(spacingBetweenLines6);

            StyleTableProperties styleTableProperties2 = new StyleTableProperties();
            TableIndentation tableIndentation2 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(bottomBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);

            TableCellMarginDefault tableCellMarginDefault2 = new TableCellMarginDefault();
            TopMargin topMargin2 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin2 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin2 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin2 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault2.Append(topMargin2);
            tableCellMarginDefault2.Append(tableCellLeftMargin2);
            tableCellMarginDefault2.Append(bottomMargin2);
            tableCellMarginDefault2.Append(tableCellRightMargin2);

            styleTableProperties2.Append(tableIndentation2);
            styleTableProperties2.Append(tableBorders1);
            styleTableProperties2.Append(tableCellMarginDefault2);

            style11.Append(styleName11);
            style11.Append(basedOn7);
            style11.Append(uIPriority10);
            style11.Append(rsid8);
            style11.Append(styleParagraphProperties5);
            style11.Append(styleTableProperties2);

            Style style12 = new Style() { Type = StyleValues.Table, StyleId = "LightList-Accent1" };
            StyleName styleName12 = new StyleName() { Val = "Light List Accent 1" };
            BasedOn basedOn8 = new BasedOn() { Val = "TableNormal" };
            UIPriority uIPriority11 = new UIPriority() { Val = 61 };
            Rsid rsid9 = new Rsid() { Val = "00BA40EF" };

            StyleParagraphProperties styleParagraphProperties6 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines7 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties6.Append(spacingBetweenLines7);

            StyleTableProperties styleTableProperties3 = new StyleTableProperties();
            TableStyleRowBandSize tableStyleRowBandSize1 = new TableStyleRowBandSize() { Val = 1 };
            TableStyleColumnBandSize tableStyleColumnBandSize1 = new TableStyleColumnBandSize() { Val = 1 };
            TableIndentation tableIndentation3 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders2 = new TableBorders();
            TopBorder topBorder2 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder2 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder2 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder2 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableBorders2.Append(topBorder2);
            tableBorders2.Append(leftBorder2);
            tableBorders2.Append(bottomBorder2);
            tableBorders2.Append(rightBorder2);

            TableCellMarginDefault tableCellMarginDefault3 = new TableCellMarginDefault();
            TopMargin topMargin3 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin3 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin3 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin3 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault3.Append(topMargin3);
            tableCellMarginDefault3.Append(tableCellLeftMargin3);
            tableCellMarginDefault3.Append(bottomMargin3);
            tableCellMarginDefault3.Append(tableCellRightMargin3);

            styleTableProperties3.Append(tableStyleRowBandSize1);
            styleTableProperties3.Append(tableStyleColumnBandSize1);
            styleTableProperties3.Append(tableIndentation3);
            styleTableProperties3.Append(tableBorders2);
            styleTableProperties3.Append(tableCellMarginDefault3);

            TableStyleProperties tableStyleProperties1 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstRow };

            StyleParagraphProperties styleParagraphProperties7 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines8 = new SpacingBetweenLines() { Before = "0", After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties7.Append(spacingBetweenLines8);

            RunPropertiesBaseStyle runPropertiesBaseStyle2 = new RunPropertiesBaseStyle();
            Bold bold7 = new Bold();
            BoldComplexScript boldComplexScript7 = new BoldComplexScript();
            Color color7 = new Color() { Val = "FFFFFF", ThemeColor = ThemeColorValues.Background1 };

            runPropertiesBaseStyle2.Append(bold7);
            runPropertiesBaseStyle2.Append(boldComplexScript7);
            runPropertiesBaseStyle2.Append(color7);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties1 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties1 = new TableStyleConditionalFormattingTableCellProperties();
            Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "4F81BD", ThemeFill = ThemeColorValues.Accent1 };

            tableStyleConditionalFormattingTableCellProperties1.Append(shading1);

            tableStyleProperties1.Append(styleParagraphProperties7);
            tableStyleProperties1.Append(runPropertiesBaseStyle2);
            tableStyleProperties1.Append(tableStyleConditionalFormattingTableProperties1);
            tableStyleProperties1.Append(tableStyleConditionalFormattingTableCellProperties1);

            TableStyleProperties tableStyleProperties2 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastRow };

            StyleParagraphProperties styleParagraphProperties8 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines9 = new SpacingBetweenLines() { Before = "0", After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties8.Append(spacingBetweenLines9);

            RunPropertiesBaseStyle runPropertiesBaseStyle3 = new RunPropertiesBaseStyle();
            Bold bold8 = new Bold();
            BoldComplexScript boldComplexScript8 = new BoldComplexScript();

            runPropertiesBaseStyle3.Append(bold8);
            runPropertiesBaseStyle3.Append(boldComplexScript8);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties2 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties2 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders1 = new TableCellBorders();
            TopBorder topBorder3 = new TopBorder() { Val = BorderValues.Double, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)6U, Space = (UInt32Value)0U };
            LeftBorder leftBorder3 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder3 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder3 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders1.Append(topBorder3);
            tableCellBorders1.Append(leftBorder3);
            tableCellBorders1.Append(bottomBorder3);
            tableCellBorders1.Append(rightBorder3);

            tableStyleConditionalFormattingTableCellProperties2.Append(tableCellBorders1);

            tableStyleProperties2.Append(styleParagraphProperties8);
            tableStyleProperties2.Append(runPropertiesBaseStyle3);
            tableStyleProperties2.Append(tableStyleConditionalFormattingTableProperties2);
            tableStyleProperties2.Append(tableStyleConditionalFormattingTableCellProperties2);

            TableStyleProperties tableStyleProperties3 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle4 = new RunPropertiesBaseStyle();
            Bold bold9 = new Bold();
            BoldComplexScript boldComplexScript9 = new BoldComplexScript();

            runPropertiesBaseStyle4.Append(bold9);
            runPropertiesBaseStyle4.Append(boldComplexScript9);

            tableStyleProperties3.Append(runPropertiesBaseStyle4);

            TableStyleProperties tableStyleProperties4 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle5 = new RunPropertiesBaseStyle();
            Bold bold10 = new Bold();
            BoldComplexScript boldComplexScript10 = new BoldComplexScript();

            runPropertiesBaseStyle5.Append(bold10);
            runPropertiesBaseStyle5.Append(boldComplexScript10);

            tableStyleProperties4.Append(runPropertiesBaseStyle5);

            TableStyleProperties tableStyleProperties5 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Vertical };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties3 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties3 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders2 = new TableCellBorders();
            TopBorder topBorder4 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder4 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder4 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder4 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders2.Append(topBorder4);
            tableCellBorders2.Append(leftBorder4);
            tableCellBorders2.Append(bottomBorder4);
            tableCellBorders2.Append(rightBorder4);

            tableStyleConditionalFormattingTableCellProperties3.Append(tableCellBorders2);

            tableStyleProperties5.Append(tableStyleConditionalFormattingTableProperties3);
            tableStyleProperties5.Append(tableStyleConditionalFormattingTableCellProperties3);

            TableStyleProperties tableStyleProperties6 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Horizontal };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties4 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties4 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders3 = new TableCellBorders();
            TopBorder topBorder5 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder5 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder5 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder5 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders3.Append(topBorder5);
            tableCellBorders3.Append(leftBorder5);
            tableCellBorders3.Append(bottomBorder5);
            tableCellBorders3.Append(rightBorder5);

            tableStyleConditionalFormattingTableCellProperties4.Append(tableCellBorders3);

            tableStyleProperties6.Append(tableStyleConditionalFormattingTableProperties4);
            tableStyleProperties6.Append(tableStyleConditionalFormattingTableCellProperties4);

            style12.Append(styleName12);
            style12.Append(basedOn8);
            style12.Append(uIPriority11);
            style12.Append(rsid9);
            style12.Append(styleParagraphProperties6);
            style12.Append(styleTableProperties3);
            style12.Append(tableStyleProperties1);
            style12.Append(tableStyleProperties2);
            style12.Append(tableStyleProperties3);
            style12.Append(tableStyleProperties4);
            style12.Append(tableStyleProperties5);
            style12.Append(tableStyleProperties6);

            Style style13 = new Style() { Type = StyleValues.Table, StyleId = "MediumGrid3-Accent1" };
            StyleName styleName13 = new StyleName() { Val = "Medium Grid 3 Accent 1" };
            BasedOn basedOn9 = new BasedOn() { Val = "TableNormal" };
            UIPriority uIPriority12 = new UIPriority() { Val = 69 };
            Rsid rsid10 = new Rsid() { Val = "00BA40EF" };

            StyleParagraphProperties styleParagraphProperties9 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines10 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties9.Append(spacingBetweenLines10);

            StyleTableProperties styleTableProperties4 = new StyleTableProperties();
            TableStyleRowBandSize tableStyleRowBandSize2 = new TableStyleRowBandSize() { Val = 1 };
            TableStyleColumnBandSize tableStyleColumnBandSize2 = new TableStyleColumnBandSize() { Val = 1 };
            TableIndentation tableIndentation4 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders3 = new TableBorders();
            TopBorder topBorder6 = new TopBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder6 = new LeftBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder6 = new BottomBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder6 = new RightBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder2 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)6U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder2 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)6U, Space = (UInt32Value)0U };

            tableBorders3.Append(topBorder6);
            tableBorders3.Append(leftBorder6);
            tableBorders3.Append(bottomBorder6);
            tableBorders3.Append(rightBorder6);
            tableBorders3.Append(insideHorizontalBorder2);
            tableBorders3.Append(insideVerticalBorder2);

            TableCellMarginDefault tableCellMarginDefault4 = new TableCellMarginDefault();
            TopMargin topMargin4 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin4 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin4 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin4 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault4.Append(topMargin4);
            tableCellMarginDefault4.Append(tableCellLeftMargin4);
            tableCellMarginDefault4.Append(bottomMargin4);
            tableCellMarginDefault4.Append(tableCellRightMargin4);

            styleTableProperties4.Append(tableStyleRowBandSize2);
            styleTableProperties4.Append(tableStyleColumnBandSize2);
            styleTableProperties4.Append(tableIndentation4);
            styleTableProperties4.Append(tableBorders3);
            styleTableProperties4.Append(tableCellMarginDefault4);

            StyleTableCellProperties styleTableCellProperties1 = new StyleTableCellProperties();
            Shading shading2 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "D3DFEE", ThemeFill = ThemeColorValues.Accent1, ThemeFillTint = "3F" };

            styleTableCellProperties1.Append(shading2);

            TableStyleProperties tableStyleProperties7 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstRow };

            RunPropertiesBaseStyle runPropertiesBaseStyle6 = new RunPropertiesBaseStyle();
            Bold bold11 = new Bold();
            BoldComplexScript boldComplexScript11 = new BoldComplexScript();
            Italic italic1 = new Italic() { Val = false };
            ItalicComplexScript italicComplexScript1 = new ItalicComplexScript() { Val = false };
            Color color8 = new Color() { Val = "FFFFFF", ThemeColor = ThemeColorValues.Background1 };

            runPropertiesBaseStyle6.Append(bold11);
            runPropertiesBaseStyle6.Append(boldComplexScript11);
            runPropertiesBaseStyle6.Append(italic1);
            runPropertiesBaseStyle6.Append(italicComplexScript1);
            runPropertiesBaseStyle6.Append(color8);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties5 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties5 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders4 = new TableCellBorders();
            TopBorder topBorder7 = new TopBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder7 = new LeftBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder7 = new BottomBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)24U, Space = (UInt32Value)0U };
            RightBorder rightBorder7 = new RightBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder3 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder3 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders4.Append(topBorder7);
            tableCellBorders4.Append(leftBorder7);
            tableCellBorders4.Append(bottomBorder7);
            tableCellBorders4.Append(rightBorder7);
            tableCellBorders4.Append(insideHorizontalBorder3);
            tableCellBorders4.Append(insideVerticalBorder3);
            Shading shading3 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "4F81BD", ThemeFill = ThemeColorValues.Accent1 };

            tableStyleConditionalFormattingTableCellProperties5.Append(tableCellBorders4);
            tableStyleConditionalFormattingTableCellProperties5.Append(shading3);

            tableStyleProperties7.Append(runPropertiesBaseStyle6);
            tableStyleProperties7.Append(tableStyleConditionalFormattingTableProperties5);
            tableStyleProperties7.Append(tableStyleConditionalFormattingTableCellProperties5);

            TableStyleProperties tableStyleProperties8 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastRow };

            RunPropertiesBaseStyle runPropertiesBaseStyle7 = new RunPropertiesBaseStyle();
            Bold bold12 = new Bold();
            BoldComplexScript boldComplexScript12 = new BoldComplexScript();
            Italic italic2 = new Italic() { Val = false };
            ItalicComplexScript italicComplexScript2 = new ItalicComplexScript() { Val = false };
            Color color9 = new Color() { Val = "FFFFFF", ThemeColor = ThemeColorValues.Background1 };

            runPropertiesBaseStyle7.Append(bold12);
            runPropertiesBaseStyle7.Append(boldComplexScript12);
            runPropertiesBaseStyle7.Append(italic2);
            runPropertiesBaseStyle7.Append(italicComplexScript2);
            runPropertiesBaseStyle7.Append(color9);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties6 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties6 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders5 = new TableCellBorders();
            TopBorder topBorder8 = new TopBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)24U, Space = (UInt32Value)0U };
            LeftBorder leftBorder8 = new LeftBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder8 = new BottomBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder8 = new RightBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder4 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder4 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders5.Append(topBorder8);
            tableCellBorders5.Append(leftBorder8);
            tableCellBorders5.Append(bottomBorder8);
            tableCellBorders5.Append(rightBorder8);
            tableCellBorders5.Append(insideHorizontalBorder4);
            tableCellBorders5.Append(insideVerticalBorder4);
            Shading shading4 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "4F81BD", ThemeFill = ThemeColorValues.Accent1 };

            tableStyleConditionalFormattingTableCellProperties6.Append(tableCellBorders5);
            tableStyleConditionalFormattingTableCellProperties6.Append(shading4);

            tableStyleProperties8.Append(runPropertiesBaseStyle7);
            tableStyleProperties8.Append(tableStyleConditionalFormattingTableProperties6);
            tableStyleProperties8.Append(tableStyleConditionalFormattingTableCellProperties6);

            TableStyleProperties tableStyleProperties9 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle8 = new RunPropertiesBaseStyle();
            Bold bold13 = new Bold();
            BoldComplexScript boldComplexScript13 = new BoldComplexScript();
            Italic italic3 = new Italic() { Val = false };
            ItalicComplexScript italicComplexScript3 = new ItalicComplexScript() { Val = false };
            Color color10 = new Color() { Val = "FFFFFF", ThemeColor = ThemeColorValues.Background1 };

            runPropertiesBaseStyle8.Append(bold13);
            runPropertiesBaseStyle8.Append(boldComplexScript13);
            runPropertiesBaseStyle8.Append(italic3);
            runPropertiesBaseStyle8.Append(italicComplexScript3);
            runPropertiesBaseStyle8.Append(color10);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties7 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties7 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders6 = new TableCellBorders();
            LeftBorder leftBorder9 = new LeftBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder9 = new RightBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)24U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder5 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder5 = new InsideVerticalBorder() { Val = BorderValues.Nil };

            tableCellBorders6.Append(leftBorder9);
            tableCellBorders6.Append(rightBorder9);
            tableCellBorders6.Append(insideHorizontalBorder5);
            tableCellBorders6.Append(insideVerticalBorder5);
            Shading shading5 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "4F81BD", ThemeFill = ThemeColorValues.Accent1 };

            tableStyleConditionalFormattingTableCellProperties7.Append(tableCellBorders6);
            tableStyleConditionalFormattingTableCellProperties7.Append(shading5);

            tableStyleProperties9.Append(runPropertiesBaseStyle8);
            tableStyleProperties9.Append(tableStyleConditionalFormattingTableProperties7);
            tableStyleProperties9.Append(tableStyleConditionalFormattingTableCellProperties7);

            TableStyleProperties tableStyleProperties10 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle9 = new RunPropertiesBaseStyle();
            Bold bold14 = new Bold();
            BoldComplexScript boldComplexScript14 = new BoldComplexScript();
            Italic italic4 = new Italic() { Val = false };
            ItalicComplexScript italicComplexScript4 = new ItalicComplexScript() { Val = false };
            Color color11 = new Color() { Val = "FFFFFF", ThemeColor = ThemeColorValues.Background1 };

            runPropertiesBaseStyle9.Append(bold14);
            runPropertiesBaseStyle9.Append(boldComplexScript14);
            runPropertiesBaseStyle9.Append(italic4);
            runPropertiesBaseStyle9.Append(italicComplexScript4);
            runPropertiesBaseStyle9.Append(color11);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties8 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties8 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders7 = new TableCellBorders();
            TopBorder topBorder9 = new TopBorder() { Val = BorderValues.Nil };
            LeftBorder leftBorder10 = new LeftBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)24U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder9 = new BottomBorder() { Val = BorderValues.Nil };
            RightBorder rightBorder10 = new RightBorder() { Val = BorderValues.Nil };
            InsideHorizontalBorder insideHorizontalBorder6 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder6 = new InsideVerticalBorder() { Val = BorderValues.Nil };

            tableCellBorders7.Append(topBorder9);
            tableCellBorders7.Append(leftBorder10);
            tableCellBorders7.Append(bottomBorder9);
            tableCellBorders7.Append(rightBorder10);
            tableCellBorders7.Append(insideHorizontalBorder6);
            tableCellBorders7.Append(insideVerticalBorder6);
            Shading shading6 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "4F81BD", ThemeFill = ThemeColorValues.Accent1 };

            tableStyleConditionalFormattingTableCellProperties8.Append(tableCellBorders7);
            tableStyleConditionalFormattingTableCellProperties8.Append(shading6);

            tableStyleProperties10.Append(runPropertiesBaseStyle9);
            tableStyleProperties10.Append(tableStyleConditionalFormattingTableProperties8);
            tableStyleProperties10.Append(tableStyleConditionalFormattingTableCellProperties8);

            TableStyleProperties tableStyleProperties11 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Vertical };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties9 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties9 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders8 = new TableCellBorders();
            TopBorder topBorder10 = new TopBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder11 = new LeftBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder10 = new BottomBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder11 = new RightBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder7 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder7 = new InsideVerticalBorder() { Val = BorderValues.Nil };

            tableCellBorders8.Append(topBorder10);
            tableCellBorders8.Append(leftBorder11);
            tableCellBorders8.Append(bottomBorder10);
            tableCellBorders8.Append(rightBorder11);
            tableCellBorders8.Append(insideHorizontalBorder7);
            tableCellBorders8.Append(insideVerticalBorder7);
            Shading shading7 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "A7BFDE", ThemeFill = ThemeColorValues.Accent1, ThemeFillTint = "7F" };

            tableStyleConditionalFormattingTableCellProperties9.Append(tableCellBorders8);
            tableStyleConditionalFormattingTableCellProperties9.Append(shading7);

            tableStyleProperties11.Append(tableStyleConditionalFormattingTableProperties9);
            tableStyleProperties11.Append(tableStyleConditionalFormattingTableCellProperties9);

            TableStyleProperties tableStyleProperties12 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Horizontal };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties10 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties10 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders9 = new TableCellBorders();
            TopBorder topBorder11 = new TopBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder12 = new LeftBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder11 = new BottomBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder12 = new RightBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder8 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder8 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "FFFFFF", ThemeColor = ThemeColorValues.Background1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders9.Append(topBorder11);
            tableCellBorders9.Append(leftBorder12);
            tableCellBorders9.Append(bottomBorder11);
            tableCellBorders9.Append(rightBorder12);
            tableCellBorders9.Append(insideHorizontalBorder8);
            tableCellBorders9.Append(insideVerticalBorder8);
            Shading shading8 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "A7BFDE", ThemeFill = ThemeColorValues.Accent1, ThemeFillTint = "7F" };

            tableStyleConditionalFormattingTableCellProperties10.Append(tableCellBorders9);
            tableStyleConditionalFormattingTableCellProperties10.Append(shading8);

            tableStyleProperties12.Append(tableStyleConditionalFormattingTableProperties10);
            tableStyleProperties12.Append(tableStyleConditionalFormattingTableCellProperties10);

            style13.Append(styleName13);
            style13.Append(basedOn9);
            style13.Append(uIPriority12);
            style13.Append(rsid10);
            style13.Append(styleParagraphProperties9);
            style13.Append(styleTableProperties4);
            style13.Append(styleTableCellProperties1);
            style13.Append(tableStyleProperties7);
            style13.Append(tableStyleProperties8);
            style13.Append(tableStyleProperties9);
            style13.Append(tableStyleProperties10);
            style13.Append(tableStyleProperties11);
            style13.Append(tableStyleProperties12);

            Style style14 = new Style() { Type = StyleValues.Table, StyleId = "MediumShading1-Accent1" };
            StyleName styleName14 = new StyleName() { Val = "Medium Shading 1 Accent 1" };
            BasedOn basedOn10 = new BasedOn() { Val = "TableNormal" };
            UIPriority uIPriority13 = new UIPriority() { Val = 63 };
            Rsid rsid11 = new Rsid() { Val = "00BA40EF" };

            StyleParagraphProperties styleParagraphProperties10 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines11 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties10.Append(spacingBetweenLines11);

            StyleTableProperties styleTableProperties5 = new StyleTableProperties();
            TableStyleRowBandSize tableStyleRowBandSize3 = new TableStyleRowBandSize() { Val = 1 };
            TableStyleColumnBandSize tableStyleColumnBandSize3 = new TableStyleColumnBandSize() { Val = 1 };
            TableIndentation tableIndentation5 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders4 = new TableBorders();
            TopBorder topBorder12 = new TopBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder13 = new LeftBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder12 = new BottomBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder13 = new RightBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder9 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableBorders4.Append(topBorder12);
            tableBorders4.Append(leftBorder13);
            tableBorders4.Append(bottomBorder12);
            tableBorders4.Append(rightBorder13);
            tableBorders4.Append(insideHorizontalBorder9);

            TableCellMarginDefault tableCellMarginDefault5 = new TableCellMarginDefault();
            TopMargin topMargin5 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin5 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin5 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin5 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault5.Append(topMargin5);
            tableCellMarginDefault5.Append(tableCellLeftMargin5);
            tableCellMarginDefault5.Append(bottomMargin5);
            tableCellMarginDefault5.Append(tableCellRightMargin5);

            styleTableProperties5.Append(tableStyleRowBandSize3);
            styleTableProperties5.Append(tableStyleColumnBandSize3);
            styleTableProperties5.Append(tableIndentation5);
            styleTableProperties5.Append(tableBorders4);
            styleTableProperties5.Append(tableCellMarginDefault5);

            TableStyleProperties tableStyleProperties13 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstRow };

            StyleParagraphProperties styleParagraphProperties11 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines12 = new SpacingBetweenLines() { Before = "0", After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties11.Append(spacingBetweenLines12);

            RunPropertiesBaseStyle runPropertiesBaseStyle10 = new RunPropertiesBaseStyle();
            Bold bold15 = new Bold();
            BoldComplexScript boldComplexScript15 = new BoldComplexScript();
            Color color12 = new Color() { Val = "FFFFFF", ThemeColor = ThemeColorValues.Background1 };

            runPropertiesBaseStyle10.Append(bold15);
            runPropertiesBaseStyle10.Append(boldComplexScript15);
            runPropertiesBaseStyle10.Append(color12);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties11 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties11 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders10 = new TableCellBorders();
            TopBorder topBorder13 = new TopBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder14 = new LeftBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder13 = new BottomBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder14 = new RightBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder10 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder9 = new InsideVerticalBorder() { Val = BorderValues.Nil };

            tableCellBorders10.Append(topBorder13);
            tableCellBorders10.Append(leftBorder14);
            tableCellBorders10.Append(bottomBorder13);
            tableCellBorders10.Append(rightBorder14);
            tableCellBorders10.Append(insideHorizontalBorder10);
            tableCellBorders10.Append(insideVerticalBorder9);
            Shading shading9 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "4F81BD", ThemeFill = ThemeColorValues.Accent1 };

            tableStyleConditionalFormattingTableCellProperties11.Append(tableCellBorders10);
            tableStyleConditionalFormattingTableCellProperties11.Append(shading9);

            tableStyleProperties13.Append(styleParagraphProperties11);
            tableStyleProperties13.Append(runPropertiesBaseStyle10);
            tableStyleProperties13.Append(tableStyleConditionalFormattingTableProperties11);
            tableStyleProperties13.Append(tableStyleConditionalFormattingTableCellProperties11);

            TableStyleProperties tableStyleProperties14 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastRow };

            StyleParagraphProperties styleParagraphProperties12 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines13 = new SpacingBetweenLines() { Before = "0", After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties12.Append(spacingBetweenLines13);

            RunPropertiesBaseStyle runPropertiesBaseStyle11 = new RunPropertiesBaseStyle();
            Bold bold16 = new Bold();
            BoldComplexScript boldComplexScript16 = new BoldComplexScript();

            runPropertiesBaseStyle11.Append(bold16);
            runPropertiesBaseStyle11.Append(boldComplexScript16);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties12 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties12 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders11 = new TableCellBorders();
            TopBorder topBorder14 = new TopBorder() { Val = BorderValues.Double, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)6U, Space = (UInt32Value)0U };
            LeftBorder leftBorder15 = new LeftBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder14 = new BottomBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder15 = new RightBorder() { Val = BorderValues.Single, Color = "7BA0CD", ThemeColor = ThemeColorValues.Accent1, ThemeTint = "BF", Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder11 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder10 = new InsideVerticalBorder() { Val = BorderValues.Nil };

            tableCellBorders11.Append(topBorder14);
            tableCellBorders11.Append(leftBorder15);
            tableCellBorders11.Append(bottomBorder14);
            tableCellBorders11.Append(rightBorder15);
            tableCellBorders11.Append(insideHorizontalBorder11);
            tableCellBorders11.Append(insideVerticalBorder10);

            tableStyleConditionalFormattingTableCellProperties12.Append(tableCellBorders11);

            tableStyleProperties14.Append(styleParagraphProperties12);
            tableStyleProperties14.Append(runPropertiesBaseStyle11);
            tableStyleProperties14.Append(tableStyleConditionalFormattingTableProperties12);
            tableStyleProperties14.Append(tableStyleConditionalFormattingTableCellProperties12);

            TableStyleProperties tableStyleProperties15 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle12 = new RunPropertiesBaseStyle();
            Bold bold17 = new Bold();
            BoldComplexScript boldComplexScript17 = new BoldComplexScript();

            runPropertiesBaseStyle12.Append(bold17);
            runPropertiesBaseStyle12.Append(boldComplexScript17);

            tableStyleProperties15.Append(runPropertiesBaseStyle12);

            TableStyleProperties tableStyleProperties16 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle13 = new RunPropertiesBaseStyle();
            Bold bold18 = new Bold();
            BoldComplexScript boldComplexScript18 = new BoldComplexScript();

            runPropertiesBaseStyle13.Append(bold18);
            runPropertiesBaseStyle13.Append(boldComplexScript18);

            tableStyleProperties16.Append(runPropertiesBaseStyle13);

            TableStyleProperties tableStyleProperties17 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Vertical };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties13 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties13 = new TableStyleConditionalFormattingTableCellProperties();
            Shading shading10 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "D3DFEE", ThemeFill = ThemeColorValues.Accent1, ThemeFillTint = "3F" };

            tableStyleConditionalFormattingTableCellProperties13.Append(shading10);

            tableStyleProperties17.Append(tableStyleConditionalFormattingTableProperties13);
            tableStyleProperties17.Append(tableStyleConditionalFormattingTableCellProperties13);

            TableStyleProperties tableStyleProperties18 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Horizontal };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties14 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties14 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders12 = new TableCellBorders();
            InsideHorizontalBorder insideHorizontalBorder12 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder11 = new InsideVerticalBorder() { Val = BorderValues.Nil };

            tableCellBorders12.Append(insideHorizontalBorder12);
            tableCellBorders12.Append(insideVerticalBorder11);
            Shading shading11 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "D3DFEE", ThemeFill = ThemeColorValues.Accent1, ThemeFillTint = "3F" };

            tableStyleConditionalFormattingTableCellProperties14.Append(tableCellBorders12);
            tableStyleConditionalFormattingTableCellProperties14.Append(shading11);

            tableStyleProperties18.Append(tableStyleConditionalFormattingTableProperties14);
            tableStyleProperties18.Append(tableStyleConditionalFormattingTableCellProperties14);

            TableStyleProperties tableStyleProperties19 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band2Horizontal };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties15 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties15 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders13 = new TableCellBorders();
            InsideHorizontalBorder insideHorizontalBorder13 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder12 = new InsideVerticalBorder() { Val = BorderValues.Nil };

            tableCellBorders13.Append(insideHorizontalBorder13);
            tableCellBorders13.Append(insideVerticalBorder12);

            tableStyleConditionalFormattingTableCellProperties15.Append(tableCellBorders13);

            tableStyleProperties19.Append(tableStyleConditionalFormattingTableProperties15);
            tableStyleProperties19.Append(tableStyleConditionalFormattingTableCellProperties15);

            style14.Append(styleName14);
            style14.Append(basedOn10);
            style14.Append(uIPriority13);
            style14.Append(rsid11);
            style14.Append(styleParagraphProperties10);
            style14.Append(styleTableProperties5);
            style14.Append(tableStyleProperties13);
            style14.Append(tableStyleProperties14);
            style14.Append(tableStyleProperties15);
            style14.Append(tableStyleProperties16);
            style14.Append(tableStyleProperties17);
            style14.Append(tableStyleProperties18);
            style14.Append(tableStyleProperties19);

            Style style15 = new Style() { Type = StyleValues.Table, StyleId = "LightGrid-Accent1" };
            StyleName styleName15 = new StyleName() { Val = "Light Grid Accent 1" };
            BasedOn basedOn11 = new BasedOn() { Val = "TableNormal" };
            UIPriority uIPriority14 = new UIPriority() { Val = 62 };
            Rsid rsid12 = new Rsid() { Val = "00BA40EF" };

            StyleParagraphProperties styleParagraphProperties13 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines14 = new SpacingBetweenLines() { After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties13.Append(spacingBetweenLines14);

            StyleTableProperties styleTableProperties6 = new StyleTableProperties();
            TableStyleRowBandSize tableStyleRowBandSize4 = new TableStyleRowBandSize() { Val = 1 };
            TableStyleColumnBandSize tableStyleColumnBandSize4 = new TableStyleColumnBandSize() { Val = 1 };
            TableIndentation tableIndentation6 = new TableIndentation() { Width = 0, Type = TableWidthUnitValues.Dxa };

            TableBorders tableBorders5 = new TableBorders();
            TopBorder topBorder15 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder16 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder15 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder16 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder14 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder13 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableBorders5.Append(topBorder15);
            tableBorders5.Append(leftBorder16);
            tableBorders5.Append(bottomBorder15);
            tableBorders5.Append(rightBorder16);
            tableBorders5.Append(insideHorizontalBorder14);
            tableBorders5.Append(insideVerticalBorder13);

            TableCellMarginDefault tableCellMarginDefault6 = new TableCellMarginDefault();
            TopMargin topMargin6 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellLeftMargin tableCellLeftMargin6 = new TableCellLeftMargin() { Width = 108, Type = TableWidthValues.Dxa };
            BottomMargin bottomMargin6 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            TableCellRightMargin tableCellRightMargin6 = new TableCellRightMargin() { Width = 108, Type = TableWidthValues.Dxa };

            tableCellMarginDefault6.Append(topMargin6);
            tableCellMarginDefault6.Append(tableCellLeftMargin6);
            tableCellMarginDefault6.Append(bottomMargin6);
            tableCellMarginDefault6.Append(tableCellRightMargin6);

            styleTableProperties6.Append(tableStyleRowBandSize4);
            styleTableProperties6.Append(tableStyleColumnBandSize4);
            styleTableProperties6.Append(tableIndentation6);
            styleTableProperties6.Append(tableBorders5);
            styleTableProperties6.Append(tableCellMarginDefault6);

            TableStyleProperties tableStyleProperties20 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstRow };

            StyleParagraphProperties styleParagraphProperties14 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines15 = new SpacingBetweenLines() { Before = "0", After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties14.Append(spacingBetweenLines15);

            RunPropertiesBaseStyle runPropertiesBaseStyle14 = new RunPropertiesBaseStyle();
            RunFonts runFonts8 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold19 = new Bold();
            BoldComplexScript boldComplexScript19 = new BoldComplexScript();

            runPropertiesBaseStyle14.Append(runFonts8);
            runPropertiesBaseStyle14.Append(bold19);
            runPropertiesBaseStyle14.Append(boldComplexScript19);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties16 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties16 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders14 = new TableCellBorders();
            TopBorder topBorder16 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder17 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder16 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)18U, Space = (UInt32Value)0U };
            RightBorder rightBorder17 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder15 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder14 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders14.Append(topBorder16);
            tableCellBorders14.Append(leftBorder17);
            tableCellBorders14.Append(bottomBorder16);
            tableCellBorders14.Append(rightBorder17);
            tableCellBorders14.Append(insideHorizontalBorder15);
            tableCellBorders14.Append(insideVerticalBorder14);

            tableStyleConditionalFormattingTableCellProperties16.Append(tableCellBorders14);

            tableStyleProperties20.Append(styleParagraphProperties14);
            tableStyleProperties20.Append(runPropertiesBaseStyle14);
            tableStyleProperties20.Append(tableStyleConditionalFormattingTableProperties16);
            tableStyleProperties20.Append(tableStyleConditionalFormattingTableCellProperties16);

            TableStyleProperties tableStyleProperties21 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastRow };

            StyleParagraphProperties styleParagraphProperties15 = new StyleParagraphProperties();
            SpacingBetweenLines spacingBetweenLines16 = new SpacingBetweenLines() { Before = "0", After = "0", Line = "240", LineRule = LineSpacingRuleValues.Auto };

            styleParagraphProperties15.Append(spacingBetweenLines16);

            RunPropertiesBaseStyle runPropertiesBaseStyle15 = new RunPropertiesBaseStyle();
            RunFonts runFonts9 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold20 = new Bold();
            BoldComplexScript boldComplexScript20 = new BoldComplexScript();

            runPropertiesBaseStyle15.Append(runFonts9);
            runPropertiesBaseStyle15.Append(bold20);
            runPropertiesBaseStyle15.Append(boldComplexScript20);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties17 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties17 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders15 = new TableCellBorders();
            TopBorder topBorder17 = new TopBorder() { Val = BorderValues.Double, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)6U, Space = (UInt32Value)0U };
            LeftBorder leftBorder18 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder17 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder18 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideHorizontalBorder insideHorizontalBorder16 = new InsideHorizontalBorder() { Val = BorderValues.Nil };
            InsideVerticalBorder insideVerticalBorder15 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders15.Append(topBorder17);
            tableCellBorders15.Append(leftBorder18);
            tableCellBorders15.Append(bottomBorder17);
            tableCellBorders15.Append(rightBorder18);
            tableCellBorders15.Append(insideHorizontalBorder16);
            tableCellBorders15.Append(insideVerticalBorder15);

            tableStyleConditionalFormattingTableCellProperties17.Append(tableCellBorders15);

            tableStyleProperties21.Append(styleParagraphProperties15);
            tableStyleProperties21.Append(runPropertiesBaseStyle15);
            tableStyleProperties21.Append(tableStyleConditionalFormattingTableProperties17);
            tableStyleProperties21.Append(tableStyleConditionalFormattingTableCellProperties17);

            TableStyleProperties tableStyleProperties22 = new TableStyleProperties() { Type = TableStyleOverrideValues.FirstColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle16 = new RunPropertiesBaseStyle();
            RunFonts runFonts10 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold21 = new Bold();
            BoldComplexScript boldComplexScript21 = new BoldComplexScript();

            runPropertiesBaseStyle16.Append(runFonts10);
            runPropertiesBaseStyle16.Append(bold21);
            runPropertiesBaseStyle16.Append(boldComplexScript21);

            tableStyleProperties22.Append(runPropertiesBaseStyle16);

            TableStyleProperties tableStyleProperties23 = new TableStyleProperties() { Type = TableStyleOverrideValues.LastColumn };

            RunPropertiesBaseStyle runPropertiesBaseStyle17 = new RunPropertiesBaseStyle();
            RunFonts runFonts11 = new RunFonts() { AsciiTheme = ThemeFontValues.MajorHighAnsi, HighAnsiTheme = ThemeFontValues.MajorHighAnsi, EastAsiaTheme = ThemeFontValues.MajorEastAsia, ComplexScriptTheme = ThemeFontValues.MajorBidi };
            Bold bold22 = new Bold();
            BoldComplexScript boldComplexScript22 = new BoldComplexScript();

            runPropertiesBaseStyle17.Append(runFonts11);
            runPropertiesBaseStyle17.Append(bold22);
            runPropertiesBaseStyle17.Append(boldComplexScript22);
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties18 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties18 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders16 = new TableCellBorders();
            TopBorder topBorder18 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder19 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder18 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder19 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders16.Append(topBorder18);
            tableCellBorders16.Append(leftBorder19);
            tableCellBorders16.Append(bottomBorder18);
            tableCellBorders16.Append(rightBorder19);

            tableStyleConditionalFormattingTableCellProperties18.Append(tableCellBorders16);

            tableStyleProperties23.Append(runPropertiesBaseStyle17);
            tableStyleProperties23.Append(tableStyleConditionalFormattingTableProperties18);
            tableStyleProperties23.Append(tableStyleConditionalFormattingTableCellProperties18);

            TableStyleProperties tableStyleProperties24 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Vertical };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties19 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties19 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders17 = new TableCellBorders();
            TopBorder topBorder19 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder20 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder19 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder20 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders17.Append(topBorder19);
            tableCellBorders17.Append(leftBorder20);
            tableCellBorders17.Append(bottomBorder19);
            tableCellBorders17.Append(rightBorder20);
            Shading shading12 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "D3DFEE", ThemeFill = ThemeColorValues.Accent1, ThemeFillTint = "3F" };

            tableStyleConditionalFormattingTableCellProperties19.Append(tableCellBorders17);
            tableStyleConditionalFormattingTableCellProperties19.Append(shading12);

            tableStyleProperties24.Append(tableStyleConditionalFormattingTableProperties19);
            tableStyleProperties24.Append(tableStyleConditionalFormattingTableCellProperties19);

            TableStyleProperties tableStyleProperties25 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band1Horizontal };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties20 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties20 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders18 = new TableCellBorders();
            TopBorder topBorder20 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder21 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder20 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder21 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder16 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders18.Append(topBorder20);
            tableCellBorders18.Append(leftBorder21);
            tableCellBorders18.Append(bottomBorder20);
            tableCellBorders18.Append(rightBorder21);
            tableCellBorders18.Append(insideVerticalBorder16);
            Shading shading13 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "D3DFEE", ThemeFill = ThemeColorValues.Accent1, ThemeFillTint = "3F" };

            tableStyleConditionalFormattingTableCellProperties20.Append(tableCellBorders18);
            tableStyleConditionalFormattingTableCellProperties20.Append(shading13);

            tableStyleProperties25.Append(tableStyleConditionalFormattingTableProperties20);
            tableStyleProperties25.Append(tableStyleConditionalFormattingTableCellProperties20);

            TableStyleProperties tableStyleProperties26 = new TableStyleProperties() { Type = TableStyleOverrideValues.Band2Horizontal };
            TableStyleConditionalFormattingTableProperties tableStyleConditionalFormattingTableProperties21 = new TableStyleConditionalFormattingTableProperties();

            TableStyleConditionalFormattingTableCellProperties tableStyleConditionalFormattingTableCellProperties21 = new TableStyleConditionalFormattingTableCellProperties();

            TableCellBorders tableCellBorders19 = new TableCellBorders();
            TopBorder topBorder21 = new TopBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            LeftBorder leftBorder22 = new LeftBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            BottomBorder bottomBorder21 = new BottomBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            RightBorder rightBorder22 = new RightBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };
            InsideVerticalBorder insideVerticalBorder17 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "4F81BD", ThemeColor = ThemeColorValues.Accent1, Size = (UInt32Value)8U, Space = (UInt32Value)0U };

            tableCellBorders19.Append(topBorder21);
            tableCellBorders19.Append(leftBorder22);
            tableCellBorders19.Append(bottomBorder21);
            tableCellBorders19.Append(rightBorder22);
            tableCellBorders19.Append(insideVerticalBorder17);

            tableStyleConditionalFormattingTableCellProperties21.Append(tableCellBorders19);

            tableStyleProperties26.Append(tableStyleConditionalFormattingTableProperties21);
            tableStyleProperties26.Append(tableStyleConditionalFormattingTableCellProperties21);

            style15.Append(styleName15);
            style15.Append(basedOn11);
            style15.Append(uIPriority14);
            style15.Append(rsid12);
            style15.Append(styleParagraphProperties13);
            style15.Append(styleTableProperties6);
            style15.Append(tableStyleProperties20);
            style15.Append(tableStyleProperties21);
            style15.Append(tableStyleProperties22);
            style15.Append(tableStyleProperties23);
            style15.Append(tableStyleProperties24);
            style15.Append(tableStyleProperties25);
            style15.Append(tableStyleProperties26);

            Style style16 = new Style() { Type = StyleValues.Paragraph, StyleId = "Quote" };
            StyleName styleName16 = new StyleName() { Val = "Quote" };
            BasedOn basedOn12 = new BasedOn() { Val = "Normal" };
            NextParagraphStyle nextParagraphStyle4 = new NextParagraphStyle() { Val = "Normal" };
            LinkedStyle linkedStyle7 = new LinkedStyle() { Val = "QuoteChar" };
            UIPriority uIPriority15 = new UIPriority() { Val = 29 };
            PrimaryStyle primaryStyle6 = new PrimaryStyle();
            Rsid rsid13 = new Rsid() { Val = "00851582" };

            StyleParagraphProperties styleParagraphProperties16 = new StyleParagraphProperties();

            ParagraphBorders paragraphBorders1 = new ParagraphBorders();
            TopBorder topBorder22 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)1U };
            LeftBorder leftBorder23 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)4U };
            BottomBorder bottomBorder22 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)1U };
            RightBorder rightBorder23 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)4U };

            paragraphBorders1.Append(topBorder22);
            paragraphBorders1.Append(leftBorder23);
            paragraphBorders1.Append(bottomBorder22);
            paragraphBorders1.Append(rightBorder23);

            styleParagraphProperties16.Append(paragraphBorders1);

            StyleRunProperties styleRunProperties7 = new StyleRunProperties();
            Italic italic5 = new Italic();
            ItalicComplexScript italicComplexScript5 = new ItalicComplexScript();
            Color color13 = new Color() { Val = "000000", ThemeColor = ThemeColorValues.Text1 };

            styleRunProperties7.Append(italic5);
            styleRunProperties7.Append(italicComplexScript5);
            styleRunProperties7.Append(color13);

            style16.Append(styleName16);
            style16.Append(basedOn12);
            style16.Append(nextParagraphStyle4);
            style16.Append(linkedStyle7);
            style16.Append(uIPriority15);
            style16.Append(primaryStyle6);
            style16.Append(rsid13);
            style16.Append(styleParagraphProperties16);
            style16.Append(styleRunProperties7);

            Style style17 = new Style() { Type = StyleValues.Character, StyleId = "QuoteChar", CustomStyle = true };
            StyleName styleName17 = new StyleName() { Val = "Quote Char" };
            BasedOn basedOn13 = new BasedOn() { Val = "DefaultParagraphFont" };
            LinkedStyle linkedStyle8 = new LinkedStyle() { Val = "Quote" };
            UIPriority uIPriority16 = new UIPriority() { Val = 29 };
            Rsid rsid14 = new Rsid() { Val = "00851582" };

            StyleRunProperties styleRunProperties8 = new StyleRunProperties();
            Italic italic6 = new Italic();
            ItalicComplexScript italicComplexScript6 = new ItalicComplexScript();
            Color color14 = new Color() { Val = "000000", ThemeColor = ThemeColorValues.Text1 };

            styleRunProperties8.Append(italic6);
            styleRunProperties8.Append(italicComplexScript6);
            styleRunProperties8.Append(color14);

            style17.Append(styleName17);
            style17.Append(basedOn13);
            style17.Append(linkedStyle8);
            style17.Append(uIPriority16);
            style17.Append(rsid14);
            style17.Append(styleRunProperties8);

            styles1.Append(docDefaults1);
            styles1.Append(latentStyles1);
            styles1.Append(style1);
            styles1.Append(style2);
            styles1.Append(style3);
            styles1.Append(style4);
            styles1.Append(style5);
            styles1.Append(style6);
            styles1.Append(style7);
            styles1.Append(style8);
            styles1.Append(style9);
            styles1.Append(style10);
            styles1.Append(style11);
            styles1.Append(style12);
            styles1.Append(style13);
            styles1.Append(style14);
            styles1.Append(style15);
            styles1.Append(style16);
            styles1.Append(style17);

            part.Styles = styles1;

        }
    }
}
