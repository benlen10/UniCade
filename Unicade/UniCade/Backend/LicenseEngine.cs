﻿using System.Security.Cryptography;
using System.Text;

namespace UniCade.Backend
{
    internal class LicenseEngine
    {
        #region Properties

        /// <summary>
        /// The user name for the current license holder
        /// </summary>
        public static string UserLicenseName;

        /// <summary>
        /// The curent license key
        /// </summary>
        public static string UserLicenseKey;

        /// <summary>
        /// True if the current license key is valid
        /// </summary>
        public static bool IsLicenseValid = false;

        /// <summary>
        /// The current hash key used to generate the license key
        /// </summary>
        public static string HashKey { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Hashes a string using SHA256 algorthim 
        /// </summary>
        /// <param name="data"> The input string to be hashed</param>
        /// <returns>Hashed string using SHA256</returns>
        public static string Sha256Hash(string data)
        {
            if (data == null)
            {
                return null;
            }
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashData = sha256.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            foreach (byte t in hashData)
            {
                returnValue.Append(t.ToString());
            }
            return returnValue.ToString();
        }

        /// <summary>
        /// Return true if the input hash matches the stored hash data
        /// </summary>
        public static bool ValidateSha256(string input, string storedHashData)
        {
            string getHashInputData = Sha256Hash(input);
            return (string.CompareOrdinal(getHashInputData, storedHashData) == 0);
        }

        #endregion

    }
}
