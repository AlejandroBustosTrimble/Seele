using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RAK.Core.UI.Xam.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Valida si todos los caracteres de un string son iguales
        /// </summary>
        public static bool ValidateSameCharsInString(string Texto)
        {
            var fc = Texto[0];
            for (int i = 0; i < Texto.Length; i++)
            {
                if (Texto[i] != fc)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Obtiene el HashMD5 para un string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Formatea un decimal para mostrarlo
        /// </summary>
        /// <param name="value"></param>
        /// <param name="decimalPlaces"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public static string FormatDecimal(Decimal value, Int32 decimalPlaces = 2, CultureInfo cultureInfo = null)
        {
            if(cultureInfo == null)
            {
                cultureInfo = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                cultureInfo.NumberFormat.NumberDecimalSeparator = ",";
                cultureInfo.NumberFormat.NumberGroupSeparator = ".";
            }

            var decimalPlacesStr = new String('0', decimalPlaces);

            var formattedAmount = value.ToString($"0.{decimalPlacesStr}", cultureInfo);

            return formattedAmount;
        }

        /// <summary>
        /// Obtiene si tiene letras mayusculas
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Boolean HasCapitalLetter(string input)
        {
            var result = false;
            if (!String.IsNullOrEmpty(input))
            {
                Regex regExCapitalLetter = new Regex("[A-Z]+");
                result = regExCapitalLetter.IsMatch(input);
            }

            return result;
        }
    }
}
