using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMoneyWithIndicator : ContentView
    {

        /// <summary>
        /// Lista de Acciones a ejecutar cuando cambia el estado
        /// Se setean con el metodo AddActionToExecuteOnChangeState
        /// </summary>
        List<Action> ActionsToExecuteOnChangeState { get; set; } = new List<Action>();

        /// <summary>
		/// Binding Property -> Money
		/// </summary>
		public static readonly BindableProperty ValidateMoneyAvailableProperty = BindableProperty.Create(
            propertyName: "ValidateMoneyAvailable",
            returnType: typeof(bool),
            declaringType: typeof(CustomMoneyWithIndicator),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay
			,
			propertyChanged: ValidateMoneyAvailablePropertyChanged
			);

        /// <summary>
        /// Dinero disponible
        /// </summary>
        public bool ValidateMoneyAvailable
        {
            get
            {
                var value = (bool)GetValue(ValidateMoneyAvailableProperty);
                return value;
            }
            set
            {
                SetValue(ValidateMoneyAvailableProperty, value);
            }
        }

		private static void ValidateMoneyAvailablePropertyChanged(BindableObject bindable, object oldVal, object newVal)
		{
			var control = (CustomMoneyWithIndicator)bindable;
			control.lblNoCredit.IsVisible = (bool)newVal;
		}

		/// <summary>
		/// Binding Property -> Money
		/// </summary>
		public static readonly BindableProperty MoneyAvailableProperty = BindableProperty.Create(
            propertyName: "MoneyAvailable",
            returnType: typeof(decimal),
            declaringType: typeof(CustomMoneyWithIndicator),
            defaultValue: 0m,
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// Dinero disponible
        /// </summary>
        public decimal MoneyAvailable
        {
            get
            {
                var value = (decimal)GetValue(MoneyAvailableProperty);
                return value;
            }
            set
            {
                SetValue(MoneyAvailableProperty, value);
            }
        }

        /// <summary>
        /// Binding Property -> Monto ingresado
        /// </summary>
        public static readonly BindableProperty AmountProperty = BindableProperty.Create(
            propertyName: "Amount",
            returnType: typeof(decimal?),
            declaringType: typeof(CustomMoneyWithIndicator),
            defaultValue: 0m,
            defaultBindingMode: BindingMode.TwoWay
            );

        /// <summary>
        /// Monto ingresado
        /// </summary>
        public decimal? Amount
        {
            get
            {
                var val = GetValue(AmountProperty);
                if (val != null)
                {
                    return (decimal?)GetValue(AmountProperty);
                }
                else
                    return default(decimal?);
            }
            set => SetValue(AmountProperty, value);
        }

        #region Events

        public event ChangeStateEventHandler ChangeStateEvent = delegate { };
        public delegate void ChangeStateEventHandler(bool validateOK);

        #endregion

        /// <summary>
        /// Ctor
        /// </summary>
        public CustomMoneyWithIndicator()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == MoneyAvailableProperty.PropertyName)
            {
                lblNoCredit.Text = $"De ${string.Format("{0:0.00}", MoneyAvailable)} disponible";
			}
			//if (propertyName == ValidateMoneyAvailableProperty.PropertyName)
			//{
			//	lblNoCredit.IsVisible = ValidateMoneyAvailable;
			//}
		}

        /// <summary>
        /// Evento (Cada vez que cambia)
        /// </summary>
        private void CustomEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = ((Entry)sender);
            string NewText = e.NewTextValue;

            int count = NewText.Count(f => f == '.' || f == ',');
            // -- Si hay mas de un punto quitamos el ultimo
            if (count > 1)
            {
                textBox.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                return;
            }
            //textBox.Text = count <= 1 ? e.NewTextValue : e.NewTextValue.Remove(e.NewTextValue.Length - 1);

            var punto = '.';
            var coma = ',';

            if (!string.IsNullOrEmpty(NewText))
            {

                // -- Si el ultimo caracter no es el separador
                if (NewText[NewText.Length - 1].ToString() != punto.ToString() && NewText[NewText.Length - 1].ToString() != coma.ToString())
                {
                    decimal decimalValue;

                    if (decimal.TryParse(NewText, out decimalValue))
                    {
                        char[] array = { punto, coma };
                        // -- Si hay mas de 2 decimales quitamos el ultimo
                        if (!this.QuantityDecimals(NewText, array))
                        {
                            textBox.Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
                            return;
                        }
                    }
                }
            }
            if (this.ValidateMoneyAvailable)
            {
                if (!string.IsNullOrEmpty(NewText))
                {
                    decimal tryRes;

                    if (decimal.TryParse(NewText, NumberStyles.AllowDecimalPoint, CultureInfo.CurrentCulture, out tryRes))
                    {
                        var enterAmount = tryRes;
                        if (enterAmount > this.MoneyAvailable)
                        {
                            SetLblNoCredit(Color.FromHex("#ff5955"));
                            ShakeLabelAsync(lblNoCredit);
                            ChangeStateEvent(false);
                        }
                        else
                        {
                            SetLblNoCredit(Color.Gray);
                            ChangeStateEvent(true);
                        }
                    }
                }
                else
                {
                    SetLblNoCredit(Color.Gray);
                    ChangeStateEvent(true);
                }
            }
        }

        private void SetLblNoCredit(Color color)
        {
            lblNoCredit.Text = $"De ${string.Format("{0:0.00}", MoneyAvailable)} disponible";
            lblNoCredit.FontSize = 13;
            lblNoCredit.TextColor = color;
            lblNoCredit.Margin = new Thickness(25, 0, 25, 10);
        }

        private async System.Threading.Tasks.Task ShakeLabelAsync(Label label)
        {
            uint timeout = 50;
            await label.TranslateTo(-15, 0, timeout);
            await label.TranslateTo(15, 0, timeout);
            await label.TranslateTo(-10, 0, timeout);
            await label.TranslateTo(10, 0, timeout);
            await label.TranslateTo(-5, 0, timeout);
            await label.TranslateTo(5, 0, timeout);
            label.TranslationX = 0;
        }

        /// <summary>
        /// Setea foco en caja de texto 
        /// </summary>
        public void SetAmountFocus()
        {
            this.txtAmount.Focus();
        }

        protected bool QuantityDecimals(string value, char[] separators)
        {
            int quantity;
            bool sep;
            sep = false;
            quantity = 0;
            foreach (char c in value)
            {
                if (c == separators[0] || c == separators[1])
                {
                    sep = true;
                }
                if (sep)
                {
                    quantity++;
                }
                if (quantity > 3)
                {
                    return false;
                }
            }
            return true;
        }

    }

}