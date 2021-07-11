using CoreGraphics;
using Fwk.XAM.Controls;
using Fwk.XAM.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(CustomEntry),typeof(CustomEntryRendererIOS))]
namespace Fwk.XAM.iOS
{
    public class CustomEntryRendererIOS: EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Layer.CornerRadius = 5;
                Control.Layer.BorderWidth = 1;
                Control.Layer.BorderColor = Color.LightGray.ToCGColor(); //.G.ToCGColor();
                Control.Layer.BackgroundColor = Color.LightGray.ToCGColor();

                Control.LeftView = new UIView(new CGRect(0, 0, 10, 0));
                Control.LeftViewMode = UIKit.UITextFieldViewMode.Always;
            }
        }
    }
}