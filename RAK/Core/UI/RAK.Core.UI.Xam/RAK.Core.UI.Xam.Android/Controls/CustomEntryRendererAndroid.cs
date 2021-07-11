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

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRendererAndroid))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
    public class CustomEntryRendererAndroid : EntryRenderer
    {
        public CustomEntryRendererAndroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();

                gradientDrawable.SetCornerRadius(10f);
                gradientDrawable.SetStroke(1, Android.Graphics.Color.LightGray);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                gradientDrawable.SetCornerRadius(Android.Graphics.Color.LightGray);
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(10, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }

        }
    }
}