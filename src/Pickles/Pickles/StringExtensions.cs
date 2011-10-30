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
