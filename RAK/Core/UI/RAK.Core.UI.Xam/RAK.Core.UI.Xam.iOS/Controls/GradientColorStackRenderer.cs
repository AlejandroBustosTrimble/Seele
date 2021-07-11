using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using CoreAnimation;
using CoreGraphics;
using Fwk.XAM.Controls;
using Fwk.XAM.iOS.Control;

[assembly: ExportRenderer(typeof(GradientColorStack), typeof(GradientColorStackRenderer))]

namespace Fwk.XAM.iOS.Control
{
    public class GradientColorStackRenderer : VisualElementRenderer<StackLayout>
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            GradientColorStack stack = (GradientColorStack)this.Element;
            CGColor startColor = stack.StartColor.ToCGColor();

            CGColor endColor = stack.EndColor.ToCGColor();

            CAGradientLayer gradientLayer;

            if (stack.Horizontal)
            {
                gradientLayer = new CAGradientLayer();
            }
            else
            {
                gradientLayer = new CAGradientLayer()
                {
                    StartPoint = new CGPoint(0, 0.5),
                    EndPoint = new CGPoint(1, 0.5)
                };
            }


			gradientLayer.Frame = rect;
            gradientLayer.Colors = new CGColor[] { startColor, endColor
        };

            NativeView.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}