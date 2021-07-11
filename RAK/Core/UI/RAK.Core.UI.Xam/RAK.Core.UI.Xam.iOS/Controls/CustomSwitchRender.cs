using Fwk.XAM.Controls;
using Fwk.XAM.iOS.Control;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRender))]
namespace Fwk.XAM.iOS.Control
{
	public class CustomSwitchRender : SwitchRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
		{
			if (Element != null)
			{
				Element.Toggled += ElementToggled;

				base.OnElementChanged(e);

				if (Control != null)
				{
					UpdateUiSwitchColor();
				}
			}
		}

		private void ElementToggled(object sender, ToggledEventArgs e)
		{
			UpdateUiSwitchColor();
		}

		private void UpdateUiSwitchColor()
		{
			var temp = Element as Switch;

			if (temp.IsToggled)
			{
				Control.ThumbTintColor = Color.FromHex("ff5955").ToUIColor();
				Control.OnTintColor = Color.FromHex("eaeaea").ToUIColor();
			}
			else
			{
				Control.ThumbTintColor = Color.FromHex("D7DADC").ToUIColor();
			}
		}
	}
}