using RAK.Core.UI.Xam.Controls.Toast;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DateTimeRange : DateRange
	{
		#region Date from

		public static readonly BindableProperty DateFromProperty = BindableProperty.Create(
			propertyName: nameof(DateFrom),
			returnType: typeof(DateTime),
			declaringType: typeof(DateTimeRange),
			defaultValue: DateTime.Now,
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				var control = (DateTimeRange)bindable;

				if (control.shouldChangeDateFrom)
					control.DateTimeFrom = control.DateFrom.Date + control.TimeFrom;
				else
					control.shouldChangeDateFrom = true;
			}
			);

		public DateTime DateFrom
		{
			get => (DateTime)GetValue(DateFromProperty);
			set => SetValue(DateFromProperty, value);
		}

		#endregion

		#region Time from

		public static readonly BindableProperty TimeFromProperty = BindableProperty.Create(
			propertyName: nameof(TimeFrom),
			returnType: typeof(TimeSpan),
			declaringType: typeof(DateTimeRange),
			defaultValue: new TimeSpan(),
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				var control = (DateTimeRange)bindable;

				if (control.shouldChangeTimeFrom)
					control.DateTimeFrom = control.DateFrom.Date + control.TimeFrom;
				else
					control.shouldChangeTimeFrom = true;
			}
			);

		public TimeSpan TimeFrom
		{
			get => (TimeSpan)GetValue(TimeFromProperty);
			set => SetValue(TimeFromProperty, value);
		}

		#endregion

		#region Date To

		public static readonly BindableProperty DateToProperty = BindableProperty.Create(
			propertyName: nameof(DateTo),
			returnType: typeof(DateTime),
			declaringType: typeof(DateTimeRange),
			defaultValue: DateTime.Now,
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				var control = (DateTimeRange)bindable;

				if (control.shouldChangeDateTo)
					control.DateTimeTo = control.DateTo.Date + control.TimeTo;
				else
					control.shouldChangeDateTo = true;
			}
			);

		public DateTime DateTo
		{
			get => (DateTime)GetValue(DateToProperty);
			set => SetValue(DateToProperty, value);
		}

		#endregion

		#region Time to

		public static readonly BindableProperty TimeToProperty = BindableProperty.Create(
			propertyName: nameof(TimeTo),
			returnType: typeof(TimeSpan),
			declaringType: typeof(DateTimeRange),
			defaultValue: new TimeSpan(),
			defaultBindingMode: BindingMode.TwoWay,
			propertyChanged: (bindable, oldValue, newValue) =>
			{
				var control = (DateTimeRange)bindable;

				if (control.shouldChangeTimeTo)
					control.DateTimeTo = control.DateTo.Date + control.TimeTo;
				else
					control.shouldChangeTimeTo = true;
			}
			);

		public TimeSpan TimeTo
		{
			get => (TimeSpan)GetValue(TimeToProperty);
			set => SetValue(TimeToProperty, value);
		}

		#endregion

		private bool shouldChangeDateFrom { get; set; }
		private bool shouldChangeTimeFrom { get; set; }
		private bool shouldChangeDateTo { get; set; }
		private bool shouldChangeTimeTo { get; set; }

		public override void DateTimeFromChanged(object oldValue, object newValue)
		{
			var control = this;

			// -- Si la fecha no coincide es cambio desde implementacion asi que hay q cambiarlo
			if (control.DateTimeFrom.Date != control.DateFrom || control.DateTimeFrom.TimeOfDay != control.TimeFrom)
			{
				if (control.DateTimeFrom.Date != control.DateFrom)
					shouldChangeDateFrom = false;
				if (control.DateTimeFrom.TimeOfDay != control.TimeFrom)
					shouldChangeTimeFrom = false;
				control.DateFrom = control.DateTimeFrom.Date;
				control.TimeFrom = control.DateTimeFrom.TimeOfDay;

				return;
			}

			base.DateTimeFromChanged(oldValue, newValue);
		}

		public override void DateTimeToChanged(object oldValue, object newValue)
		{
			var control = this;

			// -- Si la fecha no coincide es cambio desde implementacion asi que hay q cambiarlo
			if (control.DateTimeTo.Date != control.DateTo || control.DateTimeTo.TimeOfDay != control.TimeTo)
			{
				if (control.DateTimeTo.Date != control.DateTo)
					shouldChangeDateTo = false;
				if (control.DateTimeTo.TimeOfDay != control.TimeTo)
					shouldChangeTimeTo = false;
				control.DateTo = control.DateTimeTo.Date;
				control.TimeTo = control.DateTimeTo.TimeOfDay;
				return;
			}

			base.DateTimeToChanged(oldValue, newValue);
		}

		public DateTimeRange()
		{
			InitializeComponent();
		}
	}
}