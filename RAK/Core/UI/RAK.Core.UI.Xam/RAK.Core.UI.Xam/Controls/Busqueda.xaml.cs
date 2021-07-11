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
	public partial class Busqueda : ContentView
    {
        #region Binding for Text

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(string),
            declaringType: typeof(Busqueda),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay
            );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

		#endregion

		#region Binding for PlaceHolder

		public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
			propertyName: "Placeholder",
			returnType: typeof(string),
			declaringType: typeof(Busqueda),
			defaultValue: "",
			defaultBindingMode: BindingMode.TwoWay
			);

		public string Placeholder
		{
			get => (string)GetValue(PlaceholderProperty);
			set => SetValue(PlaceholderProperty, value);
		}

		#endregion

		#region Binding for ID

		public static readonly BindableProperty IDProperty = BindableProperty.Create(
            propertyName: "ID",
            returnType: typeof(long?),
            declaringType: typeof(Busqueda),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay
            );

        public long? ID
        {
            get
            {
                var val = GetValue(IDProperty);
                if (val != null)
                    return (long)val;

                return null;
            }
            set => SetValue(IDProperty, value);
        }

        #endregion

        #region Binding for Parameters

        public static readonly BindableProperty ParamProperty = BindableProperty.Create(
            propertyName: "Param",
            returnType: typeof(object),
            declaringType: typeof(Busqueda),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay
            );

        public object Param
        {
            get => (object)GetValue(ParamProperty);
            set => SetValue(ParamProperty, value);
        }

        #endregion

        #region Binding for PermiteBuscar

        public static readonly BindableProperty PermiteBuscarProperty = BindableProperty.Create(
            propertyName: "PermiteBuscar",
            returnType: typeof(bool),
            declaringType: typeof(Busqueda),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay
            );

        public bool PermiteBuscar
        {
            get
            {
                var val = GetValue(PermiteBuscarProperty);
                return (bool)val;
            }
            set => SetValue(PermiteBuscarProperty, value);
        }

        #endregion

        public string Url { get; set; }


        public Busqueda()
        {
            InitializeComponent();

            var btnModal = this.FindByName<Button>("btnModal");

            btnModal.Clicked += async (sender, e) =>
            {
                var entity = await ModalSelector.Entity(Navigation, Url, RequestType.Get, PermiteBuscar ,this.Param);
                this.Text = entity.Text;
                this.ID = entity.ID;
            };
        }
    }
}