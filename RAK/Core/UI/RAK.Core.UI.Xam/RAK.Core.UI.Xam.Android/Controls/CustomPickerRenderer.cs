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

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
	public class CustomPickerRenderer : PickerRenderer
	{
		private const string EMPTY_TEXT = "-";

		Picker element;

		public CustomPickerRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			element = (Picker)this.Element;

			var typedElement = this.Element as CustomPicker;

			Control.Background = AddPickerStyles("arrowdown");

			if (element.Items.Count == 0)
			{
				var typedItemList = element.Items as LockableObservableListWrapper;

				if (typedItemList == null || (typedItemList != null && !typedItemList.IsLocked))
				{
					if (typedElement != null && !String.IsNullOrEmpty(typedElement.EmptyText))
					{
						element.Items.Add(typedElement.EmptyText);
					}
					else
					{
						element.Items.Add(EMPTY_TEXT);
					}
				}
			}

			var view = (CustomPicker)Element;

			if (view != null)
			{
				SetIcon(view);
			}
		}

		/// <summary>
		/// Event for Element Property Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var view = (CustomPicker)Element;
			if (e.PropertyName == CustomPicker.IconProperty.PropertyName)
				SetIcon(view);
		}

		private void SetIcon(CustomPicker view)
		{
			if (!string.IsNullOrEmpty(view.Icon))
			{
				try
				{
					var context = Android.App.Application.Context;
					var resId = context.Resources.GetIdentifier(System.IO.Path.GetFileNameWithoutExtension(view.Icon), "drawable", context.PackageName);



					if (resId != 0)
					{
						#region Cambiando de color el icono

						var dra = context.GetDrawable(resId);

						#endregion

						Control.SetCompoundDrawablesWithIntrinsicBounds(resId, 0, 0, 0);
						Control.CompoundDrawablePadding = Control.CompoundDrawablePadding + 25;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
			else
			{
				Control.SetCompoundDrawablesWithIntrinsicBounds(0, 0, 0, 0);
			}
		}

		public LayerDrawable AddPickerStyles(string imagePath)
		{
			ShapeDrawable border = new ShapeDrawable();
			border.Paint.Color = Android.Graphics.Color.Gray;
			border.SetPadding(10, 10, 10, 10);
			border.Paint.SetStyle(Paint.Style.Stroke);

			var gradientDrawable = new GradientDrawable();

			//gradientDrawable.SetCornerRadius(10f);
			gradientDrawable.SetStroke(1, Android.Graphics.Color.LightGray);
			gradientDrawable.SetColor(Android.Graphics.Color.White);

			Drawable[] layers = { gradientDrawable, GetDrawable(imagePath) };
			LayerDrawable layerDrawable = new LayerDrawable(layers);
			layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

			return layerDrawable;
		}

		private BitmapDrawable GetDrawable(string imagePath)
		{
			int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
			var drawable = ContextCompat.GetDrawable(this.Context, resID);
			var bitmap = ((BitmapDrawable)drawable).Bitmap;


			var result = new BitmapDrawable(Resources, Bitmap.CreateBitmap(bitmap));
			//var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 15, 15, true));
			result.Gravity = Android.Views.GravityFlags.Right;

			return result;
		}


	}
}