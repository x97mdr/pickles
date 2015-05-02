using System;
using System.Collections.Generic;
using System.Linq;

namespace Gherkin3
{
    public class GherkinLine : IGherkinLine
    {
        private readonly string lineText;
        private readonly string trimmedLineText;
        public int LineNumber { get; private set; }

        public GherkinLine(string line, int lineNumber)
        {
            this.LineNumber = lineNumber;

            this.lineText = line;
            this.trimmedLineText = this.lineText.TrimStart();
        }

        public void Detach()
        {
            //nop
        }

        public int Indent
        {
            get { return this.lineText.Length - this.trimmedLineText.Length; }
        }

        public bool IsEmpty()
        {
            return this.trimmedLineText.Length == 0;
        }

        public bool StartsWith(string text)
        {
            return this.trimmedLineText.StartsWith(text);
        }

        public bool StartsWithTitleKeyword(string text)
        {
            int textLength = text.Length;
            return this.trimmedLineText.Length > textLength &&
                   this.trimmedLineText.StartsWith(text) &&
                   StartsWithFrom(this.trimmedLineText, textLength, GherkinLanguageConstants.TITLE_KEYWORD_SEPARATOR);
        }

        private static bool StartsWithFrom(string text, int textIndex, string value)
        {
            return string.CompareOrdinal(text, textIndex, value, 0, value.Length) == 0;
        }

        public string GetLineText(int indentToRemove)
        {
            if (indentToRemove < 0 || indentToRemove > this.Indent)
                return this.trimmedLineText;

            return this.lineText.Substring(indentToRemove);
        }

        public string GetRestTrimmed(int length)
        {
            return this.trimmedLineText.Substring(length).Trim();
        }

        public IEnumerable<GherkinLineSpan> GetTags()
        {
            int position = this.Indent;
            foreach (string item in this.trimmedLineText.Split())
            {
                if (item.Length > 0)
                {
                    yield return new GherkinLineSpan(position + 1, item);
                    position += item.Length;
                }
                position++; // separator
            }
        }
        public IEnumerable<GherkinLineSpan> GetTableCells()
        {
            int position = this.Indent;
            var items = this.trimmedLineText.Split(new [] { GherkinLanguageConstants.TABLE_CELL_SEPARATOR }, StringSplitOptions.None);
            bool isBeforeFirst = true;
            foreach (var item in items.Take(items.Length - 1)) // skipping the one after last
            {
                if (!isBeforeFirst)
                {
                    int trimmedStart;
                    var cellText = this.Trim(item, out trimmedStart);
                    var cellPosition = position + trimmedStart;

                    if (cellText.Length == 0)
                        cellPosition = position;

                    yield return new GherkinLineSpan(cellPosition + 1, cellText);
                }

                isBeforeFirst = false;
                position += item.Length;
                position++; // separator
            }
        }

        private string Trim(string s, out int trimmedStart)
        {
            trimmedStart = 0;
            while (trimmedStart < s.Length && char.IsWhiteSpace(s[trimmedStart]))
                trimmedStart++;

            return s.Trim();
        }
    }
}
