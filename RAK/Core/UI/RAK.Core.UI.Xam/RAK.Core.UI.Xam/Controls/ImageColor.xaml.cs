using FFImageLoading.Transformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageColor : ContentView
	{
		public ImageColor()
		{
			InitializeComponent();

		}

		public string Image
		{
			set { img.Source = value; }
		}

        public string ColorWithAlpha
        {
            set
            {
                img.Transformations.Clear();
                img.Transformations.Add(new TintTransformation() { HexColor = value, EnableSolidColor = true });
            }
        }


        #region Binding for Color

        public static readonly BindableProperty ColorBindingProperty = BindableProperty.Create(
            nameof(ColorBinding),
            returnType: typeof(string),
            declaringType: typeof(ImageColor),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: colorChange
            );

        private static void colorChange(BindableObject bindable, object oldValue, object newValue)
        {
            ((ImageColor)bindable).ColorWithAlpha = (string)newValue;
        }

        public string ColorBinding
        {
            get
            {
                return (string)GetValue(ColorBindingProperty);
            }
            set
            {
                SetValue(ColorBindingProperty, value);
                ColorWithAlpha = value;
            }
        }


        public static readonly BindableProperty ImageBindingProperty = BindableProperty.Create(
            propertyName: "ImageBinding",
            returnType: typeof(string),
            declaringType: typeof(ImageColor),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
             propertyChanged: imageChange
            );

        private static void imageChange(BindableObject bindable, object oldValue, object newValue)
        {
            ((ImageColor)bindable).img.Source = (string)newValue;
        }

        public string ImageBinding
        {
            get
            {
                return (string)GetValue(ImageBindingProperty);
            }
            set
            {
                Image = value;
                SetValue(ImageBindingProperty, value);
            }
        }

        #endregion
    }
}