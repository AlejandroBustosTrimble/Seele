using System;
using System.Globalization;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Converters
{

    /// <summary>
    /// Permite null y trabaja con separador
    /// </summary>
    public class NullableMoneyConverter : IValueConverter
    {

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var nullable = value as decimal?;
			var result = string.Empty;

			if (nullable.HasValue)
			{
				result = nullable.Value.ToString();
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var stringValue = value as string;
			decimal decimalValue;
			var result = value;

			var punto = '.';
			var coma = ',';

			if (value != null)
			{

				// -- Si el ultimo caracter no es el separador
				if (stringValue.Length > 0 && stringValue[stringValue.Length - 1].ToString() != punto.ToString() && stringValue[stringValue.Length - 1].ToString() != coma.ToString())
				{
					//stringValue = this.QuantityDecimals(stringValue, separator)


					if (decimal.TryParse(stringValue, out decimalValue))
					{
						char[] array = { punto, coma };
						if (!this.QuantityDecimals(stringValue, array))
						{
							stringValue = stringValue.Remove(stringValue.Length - 1);
							decimal.TryParse(stringValue, out decimalValue);
						}
						result = new Nullable<decimal>(decimalValue);
					}
				}
			}
			return result;
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
