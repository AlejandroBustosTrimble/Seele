using RAK.Core.UI.Xam.Controls;
using RAK.Core.UI.Xam.iOS.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSlider), typeof(MySliderRenderer))]
namespace RAK.Core.UI.Xam.iOS.Renders
{
    public class MySliderRenderer : SliderRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;

            var view = (CustomSlider)Element;
            if (view.ThumbColor != Color.Default || view.MaxColor != Color.Default || view.MinColor != Color.Default)
                // Establece el color a la barra de progreso
                Control.ThumbTintColor = view.ThumbColor.ToUIColor();

            //Establece el color para el estado minimo
            Control.MinimumTrackTintColor = view.MinColor.ToUIColor();

            //Establece el color para el estado maximo
            Control.MaximumTrackTintColor = view.MaxColor.ToUIColor();
        }

    }
}