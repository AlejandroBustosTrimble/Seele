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

[assembly: ExportRenderer(typeof(LabelNotPadding), typeof(LabelNotPaddingRender))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
	public class LabelNotPaddingRender : LabelRenderer
	{
		public LabelNotPaddingRender(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			Control?.SetIncludeFontPadding(false);
		}
	}
}