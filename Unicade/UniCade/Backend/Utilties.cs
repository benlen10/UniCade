﻿using System;
using System.Text.RegularExpressions;
using UniCade.Constants;
using UniCade.Interfaces;

namespace UniCade.Backend
{
    internal class Utilties
    {
        /// <summary>
        /// Verify that a string contains only numeric chars
        /// </summary>
        public static bool IsAllDigits(string str)
        {
            foreach (var c in str)
            {
                if (!Char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Check if a string contains any invalid chars
        /// </summary>
        /// <param name="str">The string to validate</param>
        /// <returns>false if the string contains any invalid characters</returns>
        public static bool CheckForInvalidChars(string str)
        {
            return (str.IndexOfAny(ConstValues.InvalidChars) != -1);
        }


        /// <summary>
        /// Check if a string contains any invalid chars
        /// </summary>
        /// <param name="str">The string to validate</param>
        /// <returns>false if the string contains any invalid characters</returns>
        public static bool CheckForInvalidSplitChars(string str)
        {
            return (str.IndexOfAny(new[] { '|' }) != -1);
        }

        /// <summary>
        /// Remove and replace all invalid chars from the input string
        /// </summary>
        internal static string RemoveInvalidChars(string str)
        {
            str = Regex.Replace(str, @"\t|\n|\r", " ");
            return str.Replace("\"", "");
        }

        /// <summary>
        /// Attempt to parse an ESRB descriptor enum from a string
        /// Return Null enum is not found
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static Enums.EsrbDescriptors ParseEsrbDescriptor(string str)
        {
            foreach (Enums.EsrbDescriptors descriptor in Enum.GetValues(typeof(Enums.EsrbDescriptors)))
            {
                if (str.IndexOf(descriptor.GetStringValue(), StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return descriptor;
                }
            }
            return Enums.EsrbDescriptors.Null;
        }


    }
}
