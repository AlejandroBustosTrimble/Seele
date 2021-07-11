using CoreGraphics;
using Fwk.XAM.Controls;
using Fwk.XAM.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(CustomExtendedPickerRendererIOS))]
namespace Fwk.XAM.iOS
{
    /// <summary>
    ///  Custom Renderer para KITS.Xamarin.Framework.Controls.ExtendedDatePicker
    /// </summary>
    public class CustomExtendedPickerRendererIOS : DatePickerRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            //if (e.NewElement != null && this.Control != null)
            //{
            //    this.Control.BorderStyle = UITextBorderStyle.Line;
            //    Control.Layer.BorderColor = UIColor.LightGray.CGColor;
            //    Control.Layer.BorderWidth = 1;

            //    //if (Device.Idiom == TargetIdiom.Tablet)
            //    //{
            //    //    this.Control.Font = UIFont.SystemFontOfSize(25);
            //    //}
            //}

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