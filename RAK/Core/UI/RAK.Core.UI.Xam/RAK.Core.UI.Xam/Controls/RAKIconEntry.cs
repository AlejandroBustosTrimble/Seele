using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
	public class RAKIconEntry : Entry
	{
		#region Icon

		/// <summary> 
		/// The font property 
		/// </summary> 
		public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(RAKIconEntry), string.Empty);

		/// <summary>
		/// Icon file used in Entry
		/// </summary>
		public string Icon
		{
			get => (string)GetValue(IconProperty);
			set => SetValue(IconProperty, value);
		}

		#endregion

		#region Height ios

		/// <summary> 
		/// Height para ios
		/// </summary> 
		public static readonly BindableProperty HeightIosProperty = BindableProperty.Create(nameof(HeightIos), typeof(double), typeof(RAKIconEntry), null, BindingMode.TwoWay,
		propertyChanged: (b, o, n) =>
		{
			var entry = (RAKIconEntry)b;

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

		public RAKIconEntry()
		{
			//if (Device.RuntimePlatform == Device.iOS && this.HeightIos.HasValue && this.HeightIos != 0)
			//{
			//	this.HeightRequest = this.HeightIos.Value;
			//}
		}
	}
}
