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

[assembly: ExportRenderer(typeof(NumericInput), typeof(NumericInputRenderer))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
	public class NumericInputRenderer : EntryRenderer
	{
		public NumericInputRenderer(Context context) : base(context)
		{

		}

		private EditText _native = null;

		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				var gradientDrawable = new GradientDrawable();

				gradientDrawable.SetCornerRadius(10f);
				gradientDrawable.SetStroke(1, Android.Graphics.Color.LightGray);
				gradientDrawable.SetColor(Android.Graphics.Color.White);
				//gradientDrawable.SetCornerRadius(Android.Graphics.Color.LightGray);
				Control.SetBackground(gradientDrawable);

				Control.SetPadding(10, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
			}

			if (e.NewElement == null)
				return;

			_native = Control as EditText;
			_native.InputType = Android.Text.InputTypes.ClassNumber;
			if ((e.NewElement as NumericInput).AllowNegative == true)
				_native.InputType |= InputTypes.NumberFlagSigned;
			if ((e.NewElement as NumericInput).AllowFraction == true)
			{
				_native.InputType |= InputTypes.NumberFlagDecimal;
				var separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
				separator = ".";
				_native.KeyListener = DigitsKeyListener.GetInstance(string.Format("1234567890{0}", separator));
			}
			if (e.NewElement.FontFamily != null)
			{
				var font = Typeface.CreateFromAsset(Android.App.Application.Context.Assets, e.NewElement.FontFamily);
				_native.Typeface = font;
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (_native == null)
				return;

			if (e.PropertyName == NumericInput.AllowNegativeProperty.PropertyName)
			{
				if ((sender as NumericInput).AllowNegative == true)
				{
					// Add Signed flag
					_native.InputType |= InputTypes.NumberFlagSigned;
				}
				else
				{
					// Remove Signed flag
					_native.InputType &= ~InputTypes.NumberFlagSigned;
				}
			}
			if (e.PropertyName == NumericInput.AllowFractionProperty.PropertyName)
			{
				if ((sender as NumericInput).AllowFraction == true)
				{
					// Add Decimal flag
					_native.InputType |= InputTypes.NumberFlagDecimal;
					var separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
					separator = ".";
					_native.KeyListener = DigitsKeyListener.GetInstance(string.Format("1234567890{0}", separator));
				}
				else
				{
					// Remove Decimal flag
					_native.InputType &= ~InputTypes.NumberFlagDecimal;
					_native.KeyListener = DigitsKeyListener.GetInstance(string.Format("1234567890"));
				}
			}
		}
	}
}