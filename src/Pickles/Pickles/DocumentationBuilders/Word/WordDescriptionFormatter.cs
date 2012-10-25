using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;
using Pickles.Extensions;

namespace Pickles.DocumentationBuilders.Word
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
