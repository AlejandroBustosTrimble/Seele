using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Behaviors
{
    public class IntToGridLengthConverter : IValueConverter
    {
        public IntToGridLengthConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
			var strValue = value.ToString();
			// -- Si contiene el caracter *
			if (strValue.Contains("*"))
			{
				// -- Quitamos el *
				strValue = strValue.Replace("*", "");
				// -- Tomamos el valor entero
				var intValue = System.Convert.ToInt32(strValue);
				// -- Ponemos el tipo como "star"
				return new GridLength(intValue, GridUnitType.Star);
			}
			else
			{
				// -- Tomamos el valor entero
				var intValue = System.Convert.ToInt32(value);
				// -- Ponemos el tipo como Absolute
				return new GridLength(intValue, GridUnitType.Absolute);
			}

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
