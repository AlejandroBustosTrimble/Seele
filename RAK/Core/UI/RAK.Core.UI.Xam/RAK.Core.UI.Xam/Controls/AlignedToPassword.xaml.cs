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
    public partial class AlignedToPassword : ContentView
    {

        #region Binding for Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(AlignedToPassword),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay
            );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region Binding for PlaceHolder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: "Placeholder",
            returnType: typeof(string),
            declaringType: typeof(AlignedToPassword),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: PlaceholderChanged
            );

        private static void PlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AlignedToPassword)bindable;
            control.entryText.Placeholder = newValue.ToString();
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        #endregion

        #region Binding for FontSize

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: "FontSize",
            returnType: typeof(double),
            declaringType: typeof(AlignedToPassword),
            defaultValue: Device.GetNamedSize(NamedSize.Default, typeof(Entry)),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: FontSizeChanged
            );

        private static void FontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (AlignedToPassword)bindable;
            control.entryText.FontSize = Convert.ToDouble(newValue);
        }

        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        #endregion

        public AlignedToPassword()
        {
            InitializeComponent();
        }
    }
}