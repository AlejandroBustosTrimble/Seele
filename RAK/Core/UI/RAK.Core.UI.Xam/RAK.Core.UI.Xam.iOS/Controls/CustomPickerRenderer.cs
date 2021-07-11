using Fwk.XAM.Controls;
using Fwk.XAM.iOS.Control;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace Fwk.XAM.iOS.Control
{
    public class CustomPickerRenderer : PickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            var element = (Picker)this.Element;

            if (element != null)
            {

                base.OnElementChanged(e);


                var downarrow = UIImage.FromBundle("arrowdown");
                Control.RightViewMode = UITextFieldViewMode.Always;
                Control.RightView = new UIImageView(downarrow);
            }
        }
    }
}