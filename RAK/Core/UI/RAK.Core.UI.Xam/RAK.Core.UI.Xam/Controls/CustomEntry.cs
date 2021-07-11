using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace RAK.Core.UI.Xam.Controls
{
    public class CustomEntry : Entry
    {
		#region Height ios

		/// <summary> 
		/// Height para ios
		/// </summary> 
		public static readonly BindableProperty HeightIosProperty = BindableProperty.Create(nameof(HeightIos), typeof(double), typeof(CustomEntry), null, BindingMode.TwoWay,
		propertyChanged: (b, o, n) =>
		{
			var entry = (CustomEntry)b;

			if (n != null)
			{
				var height = (double)n;

				if (Device.RuntimePlatform == Device.iOS && height != 0)
				{
					entry.HeightRequest = height;
				}
			}

			//do something with spv
			//o is the old value of the property
			//n is the new value
		});

		/// <summary>
		/// Height para ios
		/// </summary>
		public double? HeightIos
		{
			get => (double)GetValue(HeightIosProperty);
			set => SetValue(HeightIosProperty, value);
		}

		#endregion
	}
}
