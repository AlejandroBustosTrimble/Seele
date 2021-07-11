using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace RAK.Core.UI.Xam.Controls.ModalLoading
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPopUp : PopupPage, ILoadingPopUp
    {
        public LoadingPopUp()
        {
            this.CloseWhenBackgroundIsClicked = false;
            InitializeComponent();
        }
    }
}