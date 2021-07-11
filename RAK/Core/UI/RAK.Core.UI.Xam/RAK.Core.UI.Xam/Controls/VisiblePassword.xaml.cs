using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisiblePassword : ContentView
    {
        #region Prop changed

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        #endregion

        #region Binding for Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(VisiblePassword),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay
            );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        #endregion

        #region Binding for Icon

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            propertyName: "Icon",
            returnType: typeof(string),
            declaringType: typeof(VisiblePassword),
            defaultValue: FontAwesome.FAEye,
            defaultBindingMode: BindingMode.TwoWay
            );

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        #endregion

        #region Binding for Hidden

        public static readonly BindableProperty HiddenProperty = BindableProperty.Create(
            propertyName: "Hidden",
            returnType: typeof(bool),
            declaringType: typeof(VisiblePassword),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay
            );

        public bool Hidden
        {
            get => (bool)GetValue(HiddenProperty);
            set
            {
                SetValue(HiddenProperty, value);
                SetValue(IconProperty, value ? FontAwesome.FAEye : FontAwesome.FAEyeSlash);
            }
        }

        #endregion

        #region Binding for PlaceHolder

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            propertyName: "Placeholder",
            returnType: typeof(string),
            declaringType: typeof(VisiblePassword),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: PlaceholderChanged
            );

        private static void PlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (VisiblePassword)bindable;
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
            declaringType: typeof(VisiblePassword),
            defaultValue: Device.GetNamedSize(NamedSize.Default, typeof(Entry)),
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: FontSizeChanged
            );

        private static void FontSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (VisiblePassword)bindable;
            control.entryText.FontSize = Convert.ToDouble(newValue);
            control.btnVisible.FontSize = Convert.ToDouble(newValue);
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

        public VisiblePassword()
        {
            InitializeComponent();

            var btnVisible = this.FindByName<Label>("btnVisible");
            var entryText = this.FindByName<Entry>("entryText");

            entryText.FontSize = this.FontSize;

            btnVisible.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => { this.Hidden = !this.Hidden; }),
            });
        }
    }
}