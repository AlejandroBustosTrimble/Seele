using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using CoreGraphics;
using Foundation;
using Fwk.XAM.Controls;
using Fwk.XAM.iOS.Control;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NumericInput), typeof(NumericInputRenderer))]
namespace Fwk.XAM.iOS.Control
{
	public class NumericInputRenderer : EntryRenderer
	{
		private UITextField _native = null;

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

			if (e.NewElement == null)
				return;

			_native = Control as UITextField;

			_native.KeyboardType = UIKeyboardType.NumberPad;

			if ((e.NewElement as NumericInput).AllowNegative == true && (e.NewElement as NumericInput).AllowFraction == true)
			{
				_native.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
			}
			else if ((e.NewElement as NumericInput).AllowNegative == true)
			{
				_native.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
			}
			else if ((e.NewElement as NumericInput).AllowFraction == true)
			{
				_native.KeyboardType = UIKeyboardType.DecimalPad;
			}
			else
			{
				_native.KeyboardType = UIKeyboardType.NumberPad;
			}
			if (e.NewElement.FontFamily != null)
			{
				e.NewElement.FontFamily = e.NewElement.FontFamily.Replace(".ttf", "");
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (_native == null)
				return;
		}
	}
}