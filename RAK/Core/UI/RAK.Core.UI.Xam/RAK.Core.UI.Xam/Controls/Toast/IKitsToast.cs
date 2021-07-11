namespace RAK.Core.UI.Xam.Controls.Toast
{

    /// <summary>
    /// Interface para Toast's (Android/IOS), a utilizar en cada caso con Dependency de Xamarin
    /// </summary>
    public interface IRAKToast
    {
        /// <summary>
        /// Muestra mensaje con mas larga
        /// </summary>
        void ShowLong(string message);

        /// <summary>
        /// Muestra mensajes con duracion mas corta
        /// </summary        
        void ShowShort(string message);
    }
}
