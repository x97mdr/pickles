//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="StringExtensions.cs" company="PicklesDoc">
//  Copyright 2011 Jeffrey Cameron
//  Copyright 2012-present PicklesDoc team and community contributors
//
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//  </copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Text;

namespace PicklesDoc.Pickles.Extensions
{
    public static class StringExtensions
    {
        public static string ExpandWikiWord(this string word)
        {
            var sb = new StringBuilder();
            char previous = Char.MinValue;
            foreach (char current in word.Where(x => Char.IsLetterOrDigit(x)))
            {
                if (previous != Char.MinValue && sb.Length > 1 &&
                    ((Char.IsUpper(current) || Char.IsDigit(current)) && Char.IsLower(previous)))
                {
                    sb.Append(' ');
                }
                else if (Char.IsNumber(previous) && Char.IsUpper(current))
                {
                    sb.Append(' ');
                }

                sb.Append(current);
                previous = current;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Takes a string and lowercases it, removing newline characters and replacing tabs with spaces
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ComparisonNormalize(this string text)
        {
            return
                text.Trim().ToLowerInvariant().Replace("\r", string.Empty).Replace("\n", Environment.NewLine).Replace(
                    "\t", "    ");
        }

        public static bool IsNullOrWhiteSpace(this string text)
        {
            if (string.IsNullOrEmpty(text) || text.Trim().Length == 0) return true;

            return false;
        }
    }
}