using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Pickles.DocumentationBuilders.Word
{
    public class WordFontApplicator
    {
        public FontTablePart AddFontTablePartToPackage(WordprocessingDocument doc)
        {
            var part = doc.MainDocumentPart.AddNewPart<FontTablePart>();
            Fonts root = new Fonts();
            root.Save(part);
            return part;
        }
    }
}
