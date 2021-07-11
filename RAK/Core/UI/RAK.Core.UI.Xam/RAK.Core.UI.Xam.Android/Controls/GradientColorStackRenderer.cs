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

[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]
namespace RAK.Core.UI.Xam.Droid.Controls
{
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
    public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }

        private bool Horizontal { get; set; }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            Android.Graphics.Shader gradient;

            if (this.Horizontal)
            {
                gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0, this.StartColor.ToAndroid(),
                   this.EndColor.ToAndroid(),
                   Android.Graphics.Shader.TileMode.Mirror);
            }
            else
            {
                gradient = new Android.Graphics.LinearGradient(0, 0, 0, Height,

                       this.StartColor.ToAndroid(),
                       this.EndColor.ToAndroid(),
                       Android.Graphics.Shader.TileMode.Mirror);
            }

            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
            try
            {
                var stack = e.NewElement as GradientColorStack;
                this.StartColor = stack.StartColor;
                this.EndColor = stack.EndColor;
                this.Horizontal = stack.Horizontal;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"ERROR:", ex.Message);
            }
        }
    }
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
}