using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Converters
{
	public class NullableDecimalConverter : IValueConverter
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
			var stringValue = (value as string).Replace(",", ".");

			int count = stringValue.Count(f => f == '.');
			// -- Si hay mas de un punto quitamos el ultimo
			if (count > 1)
			{
				stringValue = stringValue.Remove(stringValue.Length - 1);
			}

			decimal decimalValue;
			decimal? result = null;



			if (decimal.TryParse(stringValue, NumberStyles.AllowDecimalPoint, culture, out decimalValue))
			{
				result = new Nullable<decimal>(decimalValue);
			}

			return result;
		}
	}
}
