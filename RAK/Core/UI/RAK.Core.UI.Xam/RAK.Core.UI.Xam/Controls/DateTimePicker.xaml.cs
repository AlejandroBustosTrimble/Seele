using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAK.Core.UI.Xam.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateTimePicker : ContentView
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        #region Binding for DateTime

        public static readonly BindableProperty DateTimeProperty = BindableProperty.Create(
            propertyName: "DateTime",
            returnType: typeof(DateTime),
            declaringType: typeof(DateTimePicker),
            defaultValue: DateTime.Now,
            defaultBindingMode: BindingMode.TwoWay
            );

        public DateTime DateTime
        {
            get => (DateTime)GetValue(DateTimeProperty);
            set => SetValue(DateTimeProperty, value);
        }


        #endregion

        private bool Initialized { get; set; }

        private DateTime _defaultValue { get; set; }

        public DateTime DefaultValue
        {
            get
            {
                return this._defaultValue;
            }
            set
            {
                this._defaultValue = value;

                this.Initialized = false;
                this.Date = value.Date;
                this.Initialized = true;
                this.Time = value.TimeOfDay;
            }
        }

        #region Binding for Date

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            propertyName: "Date",
            returnType: typeof(DateTime),
            declaringType: typeof(DateTimePicker),
            defaultValue: DateTime.Now,
            defaultBindingMode: BindingMode.TwoWay
            );

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set
            {
                if (this.Initialized)
                    // -- Seteamos el valor del dia + hora
                    this.DateTime = value.Date + this.Time;
                SetValue(DateProperty, value);
            }
        }

        #endregion

        #region Binding for Time

        public static readonly BindableProperty TimeProperty = BindableProperty.Create(
            propertyName: "Time",
            returnType: typeof(TimeSpan),
            declaringType: typeof(DateTimePicker),
            defaultValue: DateTime.Now.TimeOfDay,
            defaultBindingMode: BindingMode.TwoWay
            );

        public TimeSpan Time
        {
            get => (TimeSpan)GetValue(TimeProperty);
            set
            {
                if (this.Initialized)
                    // -- Seteamos el valor del dia + hora
                    this.DateTime = this.Date + value;
                SetValue(TimeProperty, value);
            }
        }

        #endregion


        public DateTimePicker()
        {
            this.Date = this.DateTime.Date;
            this.Initialized = true;
            this.Time = this.DateTime.TimeOfDay;
            InitializeComponent();
        }
    }
}