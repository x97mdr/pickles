using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Pickles.Extensions
{
    public static class BodyExtensions
    {
        public static void GenerateParagraph(this Body body, string text, string styleId)
        {
            Paragraph paragraph = new Paragraph() { RsidParagraphAddition = "00CC1B7A", RsidParagraphProperties = "0016335E", RsidRunAdditionDefault = "0016335E" };

            ParagraphProperties paragraphProperties = new ParagraphProperties();
            ParagraphStyleId paragraphStyleId = new ParagraphStyleId() { Val = styleId };

            paragraphProperties.Append(paragraphStyleId);

            Run run1 = new Run();
            Text text1 = new Text();
            text1.Text = text;

            run1.Append(text1);

            paragraph.Append(paragraphProperties);
            paragraph.Append(run1);

            body.Append(paragraph);
        }

        public static void InsertPageBreak(this Body body)
        {
            body.Append(new Paragraph(new Run(new Break { Type = BreakValues.Page })));
        }
    }
}
