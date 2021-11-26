using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quantum.ClassLibrary.ExtensionMethod
{
    /// <summary>
    /// String Extenstion Methods
    /// </summary>
    public static class StringExtenstion
    {
        /// <summary>
        /// Check if the valueString Matches Any in List
        /// </summary>
        /// <param name="valueComment"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool MatchAny(this string valueString, string list)
        {
            return !string.IsNullOrWhiteSpace(valueString) && list.Trim(' ', ',').Split(',').Contains(valueString.Trim());
        }
        /// <summary>
        /// Partial Match Check if the valueString Matches Contains Partial String
        /// </summary>
        /// <param name="valueString"></param>
        /// <param name="partialString"></param>
        /// <returns></returns>
        public static bool MatchPartial(this string valueString, string partialString)
        {
            return !string.IsNullOrWhiteSpace(valueString) && valueString.IndexOf(partialString) != -1;
        }
        /// <summary>
        /// Full Match Check if the valueString Matches Equal find string
        /// </summary>
        /// <param name="valueString"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        public static bool MatchFull(this string valueString, string find)
        {
            return !string.IsNullOrWhiteSpace(valueString) && valueString.Equals(find);
        }
    }
}
