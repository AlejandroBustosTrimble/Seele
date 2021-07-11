
using RAK.Core.UI.Xam.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BoxViewKeyboardHeight), typeof(RAK.Core.UI.Xam.iOS.Renders.BoxViewKeyboardHeightRenderer))]
namespace RAK.Core.UI.Xam.iOS.Renders
{
    public class BoxViewKeyboardHeightRenderer : BoxRenderer
    {
        // FUENTE: https://michaeldimoudis.com/blog/2015/11/14/taming-the-keyboard-in-native-app-development-using-xamarin-forms

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);

            if (Element != null)
            {
                Element.HeightRequest = 0;
            }

            UIKeyboard.Notifications.ObserveWillShow((sender, args) => {

                // Cuando se muestra el teclado llego el tamaño del BoxView al tamaño del teclado asi ocupa ese espacio y puedo
                // ver bien el contenido de la pagina (viendo el cuadro de texto y el boton)
                if (Element != null)
                {
                    Element.HeightRequest = args.FrameEnd.Height;
                }

            });

            UIKeyboard.Notifications.ObserveWillHide((sender, args) => {

                if (Element != null)
                {
                    Element.HeightRequest = 0;
                }

            });
        }
    }
}