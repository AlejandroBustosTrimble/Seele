using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{

    /// <summary>
    /// Crop Image con posibilidad de enviarme desde afuera el tipo de Button
    /// </summary>
    /// <typeparam name="B">Boton que queremos asociar al Control</typeparam>
    public class CropImageWithConfirm<B> : CropImageCustomView
    where B : Button, new()
    {

        /// <summary>
        /// Ctor
        /// </summary>
		public CropImageWithConfirm ()
		{
            B AssociateButton = new B();
            AssociateButton.Text = "Confirmar";

            LayoutToAddControl.Children.Add(
                 AssociateButton
            );

            AssociateButton.Clicked += AssociateButton_Clicked;
        }

        /// <summary>
        /// Eventos para atacharnos desde afuera a los resultados del click
        /// Retorna los bytes de la imagen
        /// </summary>
        public event OnConfirmCropImageButton OnCropViewConfirm = delegate { };
        public delegate void OnConfirmCropImageButton(byte[] ImageBytes);

        /// <summary>
        /// Evento Click del Boton
        /// </summary>
        private async void AssociateButton_Clicked(object sender, System.EventArgs e)
        {
            var result = await base.GetBytesFromCropView();
            OnCropViewConfirm(result);
        }
    }

}