using RAK.Core.UI.Xam.Controls.Toast;
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
	public partial class DateRange : ContentView
	{
		private const string MSG_DIF_MAX = "La diferencia entre fechas no puede superar los {0} días";
		private const string MSG_SUP = "La fecha desde debe ser inferior a la fecha hasta";

		#region Binding for date from

		public static readonly BindableProperty DateTimeFromProperty = BindableProperty.Create(
			propertyName: nameof(DateTimeFrom),
			returnType: typeof(DateTime),
			declaringType: typeof(DateRange),
			defaultValue: DateTime.Now,
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
				{
					// -- Casteamos el control
					var control = (DateRange)bindable;

					// -- LLamamos a la funcion
					control.DateTimeFromChanged(oldValue, newValue);
				}
			);

		public DateTime DateTimeFrom
		{
			get => (DateTime)GetValue(DateTimeFromProperty);
			set => SetValue(DateTimeFromProperty, value);
		}


		#endregion

		#region Binding for date to

		public static readonly BindableProperty DateTimeToProperty = BindableProperty.Create(
			propertyName: nameof(DateTimeTo),
			returnType: typeof(DateTime),
			declaringType: typeof(DateRange),
			defaultValue: DateTime.Now,
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				// -- Casteamos el control
				var control = (DateRange)bindable;

				// -- LLamamos a la funcion
				control.DateTimeToChanged(oldValue, newValue);
			}
			);

		public DateTime DateTimeTo
		{
			get => (DateTime)GetValue(DateTimeToProperty);
			set => SetValue(DateTimeToProperty, value);
		}


		#endregion

		#region Diferencia de dias

		public static readonly BindableProperty DifferenceProperty = BindableProperty.Create(
			propertyName: nameof(Difference),
			returnType: typeof(int),
			declaringType: typeof(DateRange),
			defaultValue: 31,
			defaultBindingMode: BindingMode.OneWay
			);

		public int Difference
		{
			get => (int)GetValue(DifferenceProperty);
			set => SetValue(DifferenceProperty, value);
		}

		#endregion

		#region FontSize

		public static readonly BindableProperty SizeProperty = BindableProperty.Create(
			propertyName: nameof(Size),
			returnType: typeof(double),
			declaringType: typeof(DateRange),
			defaultValue: Device.GetNamedSize(NamedSize.Default, typeof(Label)),
			defaultBindingMode: BindingMode.OneWay
			);

		[System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
		public double Size
		{
			get => (double)GetValue(SizeProperty);
			set => SetValue(SizeProperty, value);
		}

		#endregion

		#region Width

		public static readonly BindableProperty LabelColumnWidthProperty = BindableProperty.Create(
			propertyName: nameof(LabelColumnWidth),
			returnType: typeof(string),
			declaringType: typeof(DateRange),
			defaultValue: "70",
			defaultBindingMode: BindingMode.OneWay
			);

		public string LabelColumnWidth
		{
			get => (string)GetValue(LabelColumnWidthProperty);
			set => SetValue(LabelColumnWidthProperty, value);
		}

		#endregion

		#region Desde texto

		public static readonly BindableProperty FromTextProperty = BindableProperty.Create(
			propertyName: nameof(FromText),
			returnType: typeof(string),
			declaringType: typeof(DateRange),
			defaultValue: "Desde",
			defaultBindingMode: BindingMode.OneWay
			);

		public string FromText
		{
			get => (string)GetValue(FromTextProperty);
			set => SetValue(FromTextProperty, value);
		}

		#endregion

		#region Hasta texto

		public static readonly BindableProperty ToTextProperty = BindableProperty.Create(
			propertyName: nameof(ToText),
			returnType: typeof(string),
			declaringType: typeof(DateRange),
			defaultValue: "Hasta",
			defaultBindingMode: BindingMode.OneWay
			);

		public string ToText
		{
			get => (string)GetValue(ToTextProperty);
			set => SetValue(ToTextProperty, value);
		}

		#endregion

		public virtual void DateTimeFromChanged(object oldValue, object newValue)
		{
			var control = this;

			// -- Si el desde supera al hasta, ponemos el hasta igual al desde
			if (control.DateTimeFrom > control.DateTimeTo)
			{
				control.DateTimeTo = control.DateTimeFrom;
				DependencyService.Get<IRAKToast>().ShowLong(MSG_SUP);
			}
			// -- Si supera la diferencia, seteamos al hasta como el maximo posible
			if ((control.DateTimeTo - control.DateTimeFrom).TotalDays > control.Difference)
			{
				control.DateTimeTo = control.DateTimeFrom.AddDays(control.Difference);
				DependencyService.Get<IRAKToast>().ShowLong(string.Format(MSG_DIF_MAX, control.Difference));
			}
		}

		public virtual void DateTimeToChanged(object oldValue, object newValue)
		{
			var control = this;

			// -- Si el desde supera al hasta, ponemos el desde igual al hasta
			if (control.DateTimeFrom > control.DateTimeTo)
			{
				control.DateTimeFrom = control.DateTimeTo;
				DependencyService.Get<IRAKToast>().ShowLong(MSG_SUP);
			}
			// -- Si supera la diferencia, seteamos al hasta como el maximo posible
			if ((control.DateTimeTo - control.DateTimeFrom).TotalDays > control.Difference)
			{
				control.DateTimeFrom = control.DateTimeTo.AddDays(-control.Difference);
				DependencyService.Get<IRAKToast>().ShowLong(string.Format(MSG_DIF_MAX, control.Difference));
			}
		}

		public DateRange()
		{
			InitializeComponent();
		}
	}
}