using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RAK.Core.UI.Xam.Helpers
{
    public class PrefijosHelper
    {
        public static List<string> Prefijos = new List<string>()
        {
           "11","221","223","236","237","249","260","261","263","266","280",
           "291","299","336","341","342","351","362","364","370","376","379",
        };

        public static bool EsPrefijoValido(string numero)
        {
            return Prefijos.FirstOrDefault(x => x.Trim() == numero) != null || numero.Length == 4;
        }
    }
}
