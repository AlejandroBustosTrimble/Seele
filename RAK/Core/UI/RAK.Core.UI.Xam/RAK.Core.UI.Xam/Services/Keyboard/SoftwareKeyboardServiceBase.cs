using RAK.Core.UI.Xam.Extension;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Services.Keyboard
{
    /// <summary>
    /// Servicio para el teclado virtual
    /// </summary>
    public abstract class SoftwareKeyboardServiceBase : ISoftwareKeyboardService
    {
        // FUENTE: https://internetexception.com/post/2017/02/04/detecing-software-keyboard-events-in-xamarin-android
        // Se podria hacer con lo que esta para el BoxView usando en las paginas de mensaje para iOS el render asi completar la implementacion de esto

        /*
         Uso:
         https://internetexception.com/post/2017/02/04/detecing-software-keyboard-events-in-xamarin-android
            public MainPageViewModel()
            {
                var keyboardService = TinyIoCContainer.Current.Resolve<ISoftwareKeyboardService>();
                keyboardService.Hide += _keyboardService_Hide;
                keyboardService.Show += _keyboardService_Show;
            }
 
            private void _keyboardService_Show(object sender, SoftwareKeyboardEventArgs args)
            {
                Event = "Show event handler invoked";
            }
 
            private void _keyboardService_Hide(object sender, SoftwareKeyboardEventArgs args)
            {
                Event = "Hide event handler invoked";
            }

             */

        public virtual event SoftwareKeyboardEventHandler Hide;

        public virtual event SoftwareKeyboardEventHandler Show;

        public void InvokeKeyboardHide(SoftwareKeyboardEventArgs args)
        {
            OnHide();
            var handler = Hide;
            handler?.Invoke(this, args);
        }

        public void InvokeKeyboardShow(SoftwareKeyboardEventArgs args)
        {
            var handler = Show;
            handler?.Invoke(this, args);
        }

        private void OnHide()
        {
            if (Application.Current.MainPage != null)
            {
                Application.Current.MainPage.SetTranslation(0);
            }
        }
    }
}
