using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Converters
{
    public class NullableLongConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var nullable = value as long?;
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
            long longValue;
            long? result = null;

            if (long.TryParse(stringValue, out longValue))
            {
                result = new Nullable<long>(longValue);
            }

            return result;
        }
    }
}
