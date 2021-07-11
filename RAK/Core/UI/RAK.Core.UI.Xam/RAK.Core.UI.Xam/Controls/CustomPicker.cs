using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Controls
{
    public class CustomPicker : Picker
    {
        /// <summary>
        /// Texto que se muestra cuando el picker no tiene elementos
        /// </summary>
        public string EmptyText { get; set; }

		#region Icon

		/// <summary> 
		/// The font property 
		/// </summary> 
		public static readonly BindableProperty IconProperty = BindableProperty.Create(nameof(Icon), typeof(string), typeof(CustomPicker), string.Empty);

		/// <summary>
		/// Icon file used in Entry
		/// </summary>
		public string Icon
		{
			get => (string)GetValue(IconProperty);
			set => SetValue(IconProperty, value);
		}

		#endregion
	}
}
