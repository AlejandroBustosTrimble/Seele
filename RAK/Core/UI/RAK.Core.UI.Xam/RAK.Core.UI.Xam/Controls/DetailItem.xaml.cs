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
    public partial class DetailItem : ContentView
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public DetailItem()
        {
            InitializeComponent();
        }

        #region Binding for Visible

        public static readonly BindableProperty VisibleProperty = BindableProperty.Create(
            propertyName: "Visible",
            returnType: typeof(bool),
            declaringType: typeof(DetailItem),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay
            );

        public bool Visible
        {
            get => (bool)GetValue(VisibleProperty);
            set
            {
                SetValue(VisibleProperty, value);
            }
        }

        #endregion

        #region Binding for Title

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: "Title",
            returnType: typeof(string),
            declaringType: typeof(DetailItem),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay
            );

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        #endregion

        #region Binding for Importe

        public static readonly BindableProperty ImporteProperty = BindableProperty.Create(
            propertyName: "Importe",
            returnType: typeof(string),
            declaringType: typeof(DetailItem),
            defaultValue: string.Empty,
            defaultBindingMode: BindingMode.TwoWay
            );

        public string Importe
        {
            get => (string)GetValue(ImporteProperty);
            set
            {
                SetValue(ImporteProperty, value);
            }
        }

        #endregion
    }
}