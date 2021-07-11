using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class CircularSlider : View
    {
        public static readonly BindableProperty MinProperty = BindableProperty.Create(nameof(Min), typeof(float), typeof(CircularSlider), default(float), BindingMode.TwoWay);
        public static readonly BindableProperty MaxProperty = BindableProperty.Create(nameof(Max), typeof(float), typeof(CircularSlider), default(float), BindingMode.TwoWay);
        public static readonly BindableProperty CurrentProperty = BindableProperty.Create(nameof(Current), typeof(float), typeof(CircularSlider), default(float), BindingMode.TwoWay);
        public static readonly BindableProperty BoundsProperty = BindableProperty.Create(nameof(Bounds), typeof(Rectangle), typeof(CircularSlider), new Rectangle(0, 0, 300, 300), BindingMode.TwoWay);
        public static readonly BindableProperty ShowTouchPathProperty = BindableProperty.Create(nameof(ShowTouchPath), typeof(bool), typeof(CircularSlider), default(bool), BindingMode.TwoWay);

        //public static readonly BindableProperty MinProperty = BindableProperty.Create<CircularSlider, float>(p => p.Min, 0);

        //public static readonly BindableProperty MaxProperty = BindableProperty.Create<CircularSlider, float>(p => p.Max, 100);

        //public static readonly BindableProperty CurrentProperty = BindableProperty.Create<CircularSlider, float>(p => p.Current, 50, BindingMode.TwoWay);

        //public static readonly BindableProperty BoundsProperty = BindableProperty.Create<CircularSlider, Rectangle>(p => p.Bounds, new Rectangle(0, 0, 300, 300));

        //public static readonly BindableProperty ShowTouchPathProperty = BindableProperty.Create<CircularSlider, bool>(p => p.ShowTouchPath, false);

        public float Min
        {
            get { return (float)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); OnPropertyChanged(); }
        }

        public float Max
        {
            get { return (float)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); OnPropertyChanged(); }
        }

        public float Current
        {
            get { return (float)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); OnPropertyChanged(); }
        }

        public new Rectangle Bounds
        {
            get { return (Rectangle)GetValue(BoundsProperty); }
            set { SetValue(BoundsProperty, value); }
        }

        public bool ShowTouchPath
        {
            get { return (bool)GetValue(ShowTouchPathProperty); }
            set { SetValue(ShowTouchPathProperty, value); }
        }

        public bool IsRotationEndedRegistered { get { return RotationEnded != null; } }

        public Action<float> RotationStarted;
        public Action<float> RotationEnded;

        public CircularSlider(Rectangle frame, float min, float max, float current, bool showTouchPath = false)
        {
            SetValue(MinProperty, min);
            SetValue(MaxProperty, max);
            SetValue(CurrentProperty, current);
            SetValue(BoundsProperty, frame);
            SetValue(ShowTouchPathProperty, showTouchPath);
        }

        public CircularSlider(Rectangle frame)
        {
            Bounds = frame;
        }
    }
}
