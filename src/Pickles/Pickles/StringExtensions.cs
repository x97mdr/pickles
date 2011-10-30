using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pickles
{
    public static class StringExtensions
    {
        public static string ExpandWikiWord(this string word)
        {
            var sb = new StringBuilder();
            char previous = Char.MinValue;
            foreach (var c in word.Where(x => Char.IsLetterOrDigit(x)))
            {
                if (previous != Char.MinValue && (Char.IsUpper(c) || (Char.IsDigit(c) && !Char.IsDigit(previous)))) sb.Append(' ');
                sb.Append(c);
                previous = c;
            }
            return sb.ToString();
        }
    }
}
