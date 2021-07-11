using RAK.Core.UI.Xam.Controls.ModalLoading;
using RAK.Core.UI.Xam.DependenciesInterfaces;
using RAK.Core.UI.Xam.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Page
{
    public class GenericPopUp<BindingContextVM> : PopupPage, IGenericContentPage
    where BindingContextVM : IGenericVM, new()
    {

        private bool Inicializado = false;

        /// <summary>
        /// Retorna la dependency concreta de IKitsLoadingPopUp
        /// </summary>
        protected IRAKLoadingPopUp PopUpDependencyService
        {
            get
            {
                return DependencyService.Get<IRAKLoadingPopUp>();
            }
        }

        /// <summary>
        /// VM asociado al Contexto
        /// </summary>
        protected BindingContextVM bindingContextVM { get; set; } = new BindingContextVM();

        /// <summary>
        /// Ctor
        /// </summary>
        public GenericPopUp()
        {
            this.BindingContext = bindingContextVM;
            bindingContextVM.AssociatePage = this;
        }

        /// <summary>
        /// ShowPopUp
        /// </summary>
        public virtual async Task<bool> ShowPopUp()
        {
            var PopUpDependency = PopUpDependencyService;
            if (PopUpDependency == null)
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopUp());
                return true;
            }
            else
            {
                await PopUpDependency.ShowPopUp();
                return true;
            }
        }

        /// <summary>
        /// ClosePopUp
        /// </summary>
        /// <returns></returns>
        public virtual async Task<bool> ClosePopUp()
        {
            var PopUpDependency = PopUpDependencyService;
            if (PopUpDependency == null)
            {
                await PopupNavigation.Instance.PopAsync();
                return true;
            }
            else
            {
                await PopUpDependency.ClosePopUp();
                return true;
            }
        }

        public virtual async Task<bool> MostrarAlertaAceptar(string title, string message, string accept, string cancel, string icon = "")
        {
            var alertHelper = DependencyService.Get<IAlertHelper>();
            if (alertHelper != null)
            {
                return await alertHelper.CreateIconAlertAccept(icon, title, message, cancel, accept);
            }
            else
            {
                var res = await DisplayAlert(title, message, accept, cancel);
                return res;
            }
        }

        public virtual async void MostrarAlerta(string title, string message, string accept, string icon = "")
        {
            var alertHelper = DependencyService.Get<IAlertHelper>();
            if (alertHelper != null)
            {
                await alertHelper.CreateIconAlert(icon, title, message, accept);
            }
            else
            {
                await DisplayAlert(title, message, accept);
            }
        }

        protected override void OnAppearing()
        {
            if (!this.Inicializado)
            {
                this.bindingContextVM.OnInit();
                this.Inicializado = true;
            }
            taskCompletionSource = new TaskCompletionSource<bool>();
        }

        private TaskCompletionSource<bool> taskCompletionSource;
        public Task PopupClosedTask { get { return taskCompletionSource.Task; } }

        public bool ShouldShowPopUpOnSubmit
        {
            get
            {
                return true;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            taskCompletionSource.SetResult(true);
        }
    }
}
