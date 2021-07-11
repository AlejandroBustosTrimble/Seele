using RAK.Core.UI.Xam.ViewModel;
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
    public partial class PinEntry : ContentView
    {
        public PinEntry()
        {
            InitializeComponent();
        }

        /// <summary>
		/// Binding Property -> Pin
		/// </summary>
		public static readonly BindableProperty PinProperty = BindableProperty.Create(
            propertyName: "Pin",
            returnType: typeof(string),
            declaringType: typeof(CustomMoneyWithIndicator),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// Pin Ingresado
        /// </summary>
        public string Pin
        {
            get
            {
                var value = (string)GetValue(PinProperty);
                return value;
            }
            set
            {
                SetValue(PinProperty, value);
            }
        }
        public void SetFocusOnFirst()
        {
            this.pinPago1.Focus();
        }

        public void ClearText()
        {
            this.pinPago1.Text = "";
            this.pinPago2.Text = "";
            this.pinPago3.Text = "";
            this.pinPago4.Text = "";
            this.Pin = "";
        }

        private void PinPago1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.pinPago1.Text))
                pinPago2.Focus();
        }

        private void PinPago2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.pinPago2.Text))
                pinPago3.Focus();
        }

        private void PinPago3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.pinPago3.Text))
                pinPago4.Focus();
        }

        private void PinPago4_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.pinPago4.Text))
            {
                this.Pin = $"{this.pinPago1.Text}{this.pinPago2.Text}{this.pinPago3.Text}{this.pinPago4.Text}";
                this.pinPago4.Unfocus();
            }
        }
    }
}