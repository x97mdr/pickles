using System;
using DocumentFormat.OpenXml.Wordprocessing;
using PicklesDoc.Pickles.Extensions;

namespace PicklesDoc.Pickles.DocumentationBuilders.Word
{
    public class WordDescriptionFormatter
    {
        public void Format(Body body, string description)
        {
            foreach (var paragraph in description.Split(new string[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries))
            {
                body.GenerateParagraph(paragraph, "Normal");
            }
        }
    }
}
