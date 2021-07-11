using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RAK.Core.UI.Xam.Helpers
{
    public static class MailHelper
    {
        /// <summary>
        /// Valida q un mail sea valido
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var emailRegExExpression = @"(?<firstPartMail>^[a-zA-Z0-9]+[a-zA-Z0-9\.\-_]+)@(?<domain>[a-zA-Z0-9]+([a-zA-Z0-9\.\-]+)((\.([a-zA-Z0-9]){2,3})+)$)";
                var regEx = new Regex(emailRegExExpression, RegexOptions.IgnoreCase);

                return regEx.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
    }
}
