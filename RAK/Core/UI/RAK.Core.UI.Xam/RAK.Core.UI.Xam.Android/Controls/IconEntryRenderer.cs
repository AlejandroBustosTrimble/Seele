using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RAK.Core.UI.Xam.Controls;
using RAK.Core.UI.Xam.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RAKIconEntry), typeof(RAKIconEntryRenderer))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
	public class RAKIconEntryRenderer : EntryRenderer
	{
		/// <summary>
		/// Context
		/// </summary>
		/// <param name="context"></param>
		public RAKIconEntryRenderer(Context context)
			: base(context)
		{
		}

		/// <summary>
		/// Init
		/// </summary>
		public async static void Init()
		{
			var temp = DateTime.Now;
		}

		/// <summary>
		/// Event for ElementChanged
		/// </summary>
		/// <param name="e"></param>
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			#region Para que el entry sea como el de ios

			if (e.OldElement == null)
			{
				var gradientDrawable = new GradientDrawable();

				gradientDrawable.SetCornerRadius(10f);
				gradientDrawable.SetStroke(1, Android.Graphics.Color.LightGray);
				gradientDrawable.SetColor(Android.Graphics.Color.White);
				Control.SetBackground(gradientDrawable);

				//Control.SetPadding(10, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
			}

			#endregion

			var view = (RAKIconEntry)Element;

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

			var view = (RAKIconEntry)Element;
			if (e.PropertyName == RAKIconEntry.IconProperty.PropertyName)
				SetIcon(view);
		}

		private void SetIcon(RAKIconEntry view)
		{
			if (!string.IsNullOrEmpty(view.Icon))
			{
				//var resId = Resources.GetIdentifier(view.Icon,"drawable", PackageName)
				//var resId = (int)typeof(Resource.Drawable).GetField(Path.GetFileNameWithoutExtension(view.Icon)).GetValue(null);

				try
				{
					//Context context => CrossCurrentActivity.Current.Activity;

					var context = Android.App.Application.Context;
					var resId = context.Resources.GetIdentifier(Path.GetFileNameWithoutExtension(view.Icon), "drawable", context.PackageName);



					if (resId != 0)
					{
						#region Cambiando de color el icono

						var dra = context.GetDrawable(resId);

						//dra.SetTint(Android.Graphics.Color.LightGray);
						//dra.SetColorFilter(Android.Graphics.Color.LightGray, PorterDuff.Mode.SrcIn);

						//dra.SetColorFilter(Android.Graphics.Color.LightGray);

						#endregion

						Control.SetCompoundDrawablesWithIntrinsicBounds(resId, 0, 0, 0);
						//Control.SetCompoundDrawables(dra, null, null, null);
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
	}
}