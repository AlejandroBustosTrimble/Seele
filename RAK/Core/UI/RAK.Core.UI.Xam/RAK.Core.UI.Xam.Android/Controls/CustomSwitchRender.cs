using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRender))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
	public class CustomSwitchRender : SwitchRenderer
	{
		private Android.Graphics.Color greyColor = new Android.Graphics.Color(215, 218, 220);
		private Android.Graphics.Color redColor = new Android.Graphics.Color(255, 89, 85);

		public CustomSwitchRender(Context context) : base(context)
		{
			//var color = this.Control.TrackDrawable.ColorFilter;
			//AutoPackage = false;
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				this.Control.CheckedChange -= this.OnCheckedChange;
				base.Dispose(disposing);
			}
			catch
			{

			}
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
		{
			base.OnElementChanged(e);
			try
			{
				if (this.Control != null)
				{
					var color = this.Control.TrackDrawable.ColorFilter;
					if (this.Control.Checked)
					{
						this.Control.ThumbDrawable.SetColorFilter(redColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
						this.Control.TrackDrawable.SetColorFilter(greyColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
					}
					else
					{
						this.Control.ThumbDrawable.SetColorFilter(greyColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
						this.Control.TrackDrawable.SetColorFilter(greyColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
					}

					this.Control.CheckedChange += this.OnCheckedChange;
				}
			}
			catch
			{

			}
		}

		private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
		{
			try
			{
				var color = this.Control.TrackDrawable.ColorFilter;
				if (this.Control.Checked)
				{
					this.Control.ThumbDrawable.SetColorFilter(redColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
					this.Control.TrackDrawable.SetColorFilter(greyColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
				}
				else
				{
					this.Control.ThumbDrawable.SetColorFilter(greyColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
					this.Control.TrackDrawable.SetColorFilter(greyColor, Android.Graphics.PorterDuff.Mode.SrcAtop);
				}
				((IElementController)base.Element).SetValueFromRenderer(Xamarin.Forms.Switch.IsToggledProperty, e.IsChecked);
			}
			catch
			{

			}

		}
	}
}