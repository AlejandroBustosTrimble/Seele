using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class GradientColorStack : StackLayout
    {
        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor),

       typeof(Color), typeof(GradientColorStack), Color.Default);

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }

            set { SetValue(StartColorProperty, value); }
        }


        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor),

      typeof(Color), typeof(GradientColorStack), Color.Default);

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }

            set { SetValue(EndColorProperty, value); }
        }

        public static readonly BindableProperty HorizontalProperty = BindableProperty.Create(nameof(Horizontal),

      typeof(bool), typeof(GradientColorStack), false);

        public bool Horizontal
        {
            get { return (bool)GetValue(HorizontalProperty); }

            set { SetValue(HorizontalProperty, value); }
        }
    }
}
