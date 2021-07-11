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

[assembly: ExportRenderer(typeof(CustomSlider), typeof(MySliderRenderer))]
namespace RAK.Core.UI.Xam.Droid.Renders
{
    public class MySliderRenderer : SliderRenderer
    {
        private static readonly bool _isTintSupported = Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;

        private CustomSlider view;

        public MySliderRenderer(Context context) : base(context)
        {
            AutoPackage = false;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || e.NewElement == null)
                return;
            view = (CustomSlider)Element;

            if (view.ThumbColor != forms.Color.Default || view.MaxColor != forms.Color.Default || view.MinColor != forms.Color.Default)
                Control.Thumb.SetColorFilter(view.ThumbColor.ToAndroid(), PorterDuff.Mode.SrcIn);

            if (_isTintSupported)
            {
                //  ProgressTintList is only available >= api level 21.
                Control.ProgressTintList = Android.Content.Res.ColorStateList.ValueOf(view.MinColor.ToAndroid());
                Control.ProgressTintMode = PorterDuff.Mode.SrcIn;

                //Esto es para la línea máxima del control deslizante
                Control.ProgressBackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(view.MaxColor.ToAndroid());
                Control.ProgressBackgroundTintMode = PorterDuff.Mode.SrcIn;
            }
            else
            {
                Drawable progressDrawable = Control.ProgressDrawable.Mutate();
                progressDrawable.SetColorFilter(view.MinColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                Control.ProgressDrawable = progressDrawable;
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.JellyBean)
            {
                if (Control == null)

                { return; }

                SeekBar ctrl = Control;

                Drawable thumb = ctrl.Thumb;

                int thumbTop = ctrl.Height / 2 - thumb.IntrinsicHeight / 2;

                thumb.SetBounds(thumb.Bounds.Left, thumbTop,

                thumb.Bounds.Left + thumb.IntrinsicWidth, thumbTop + thumb.IntrinsicHeight);
            }
        }
    }
}