using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Helpers
{
    /// <summary>
    /// Autocomplete para prefix y salto al textbox que pide el numero
    /// </summary>
    public static class AutoCompletePrefix
    {

        /// <summary>
        /// Verifica y en caso de requerir cambio de foco lo hace
        /// </summary>
        /// <param name="e">TextChangedEventArgs de la caja de texto del prefijo</param>
        /// <param name="elementToFocus">Elemento al cual se cambia el foco</param>
        public static void Verify(TextChangedEventArgs e, VisualElement elementToFocus)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                if (PrefijosHelper.EsPrefijoValido(e.NewTextValue))
                    elementToFocus.Focus();
            }
        }

    }
}
