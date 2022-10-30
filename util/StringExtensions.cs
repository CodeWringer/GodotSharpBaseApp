using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaViewer.Application.Utility
{
    public static class StringExtensions
    {
        /// <summary>
        /// An array of brace, bracket, parentheses, etc. mappings. 
        /// <br></br>
        /// They key is always the 'opening' char and the value is the corresponding 'closing' char. 
        /// </summary>
        internal static readonly KeyValuePair<char, char>[] matchingChars = new KeyValuePair<char, char>[]
        {
            new KeyValuePair<char, char>('{', '}'),
            new KeyValuePair<char, char>('[', ']'),
            new KeyValuePair<char, char>('(', ')'),
            new KeyValuePair<char, char>('<', '>'),
        };

        /// <summary>
        /// Returns true, if the string is null or consists only of whitespace characters. 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrBlank(this string str)
        {
            if (str == null)
                return true;
            else if (str.Trim().Length == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns the index of the matching given opening brace '{', square bracket '[', angle bracket '<' or parenthesis '(' 
        /// within the string. Returns -1, if no match could be found. 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="c">The char to check against. Allowed values: '{', '[', '(', '<'</param>
        /// <param name="startAt">Index to start searching, within the string. Default 0. </param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Thrown, if an invalid character is specified. </exception>
        public static int GetMatchingIndexOf(this string str, char c, int startAt = 0)
        {
            var mappings = matchingChars.Where(it => it.Key == c).ToList();
            if (mappings.Count == 0)
                throw new ArgumentException("Bad character! Allowed values: '{', '[', '(', '<'");

            var mapping = mappings.First();

            int expectedEnds = 1;
            for (int i = startAt + 1; i < str.Length; i++)
            {
                var currentChar = str[i];
                if (currentChar == mapping.Key)
                {
                    expectedEnds++;
                }
                else if (currentChar == mapping.Value)
                {
                    expectedEnds--;

                    if (expectedEnds == 0)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}
