using RAK.Core.UI.Xam.Controls.ModalLoading;
using RAK.Core.UI.Xam.ViewModel;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Page
{
    public class PopUpBase<VM> : PopupPage
        where VM : ViewModel.ViewModelBase, new()
    {
        /// <summary>
        /// View Model de la vista
        /// </summary>
        public VM viewModel;

        private TaskCompletionSource<bool> taskCompletionSource;
        public Task PopupClosedTask { get { return taskCompletionSource.Task; } }

        public PopUpBase()
        {
            // -- Iniciamos el viewModel
            viewModel = new VM();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            if (!this.viewModel.Inicializado)
            {
                this.viewModel.Inicializar();
                this.viewModel.Inicializado = true;
            }
            taskCompletionSource = new TaskCompletionSource<bool>();
        }

             protected override void OnDisappearing()
     {
          base.OnDisappearing();
          taskCompletionSource.SetResult(true);
     }

        // -- Ejecuta post
        public async Task<T> ExecuteServicePost<T>(string url, object param)
            where T : RequestViewModelBase
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopUp());

                // -- Ejecutamos en el view model
                var res = await this.viewModel.ExecuteServicePost<T>(url, param);

                await PopupNavigation.Instance.PopAsync();

                // -- Recorremos los mensajes
                foreach (var msg in this.viewModel.Alerts)
                {
                    // -- Mostramos las alertas
                    await DisplayAlert("Alerta!", msg.Message, "Aceptar");
                }

                // -- Retornamos el response vm
                return res;
            }
            catch (Exception ex)
            {
                // -- Mostramos las alertas
                await DisplayAlert("Alerta!", "Ha ocurrido un error al realizar la operación", "Aceptar");

                return null;
            }
        }

        // -- Ejecuta get
        public async Task<T> ExecuteServiceGet<T>(string url, object param)
           where T : RequestViewModelBase
        {
            try
            {
                // -- Ejecutamos en el view model
                var res = await this.viewModel.ExecuteServiceGet<T>(url, param);

                // -- Recorremos los mensajes
                foreach (var msg in this.viewModel.Alerts)
                {
                    // -- Mostramos las alertas
                    await DisplayAlert("Alerta!", msg.Message, "Aceptar");
                }

                // -- Retornamos el response vm
                return res;
            }
            catch (Exception ex)
            {
                // -- Mostramos las alertas
                await DisplayAlert("Alerta!", "Ha ocurrido un error al realizar la operación", "Aceptar");

                return null;
            }
        }

        // -- Ejecuta post
        public async Task<List<T>> ExecuteServiceListPost<T>(string url, object param)
            where T : RequestViewModelBase
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopUp());

                // -- Ejecutamos en el view model
                var res = await this.viewModel.ExecuteServiceListPost<T>(url, param);

                await PopupNavigation.Instance.PopAsync();

                // -- Recorremos los mensajes
                foreach (var msg in this.viewModel.Alerts)
                {
                    // -- Mostramos las alertas
                    await DisplayAlert("Alerta!", msg.Message, "Aceptar");
                }

                // -- Retornamos el response vm
                return res;
            }
            catch (Exception ex)
            {
                // -- Mostramos las alertas
                await DisplayAlert("Alerta!", "Ha ocurrido un error al realizar la operación", "Aceptar");

                return null;
            }
        }

        // -- Ejecuta get
        public async Task<List<T>> ExecuteServiceListGet<T>(string url, object param)
           where T : RequestViewModelBase
        {
            try
            {
                // -- Ejecutamos en el view model
                var res = await this.viewModel.ExecuteServiceListGet<T>(url, param);

                // -- Recorremos los mensajes
                foreach (var msg in this.viewModel.Alerts)
                {
                    // -- Mostramos las alertas
                    await DisplayAlert("Alerta!", msg.Message, "Aceptar");
                }

                // -- Retornamos el response vm
                return res;
            }
            catch (Exception ex)
            {
                // -- Mostramos las alertas
                await DisplayAlert("Alerta!", "Ha ocurrido un error al realizar la operación", "Aceptar");

                return null;
            }
        }

        /// <summary>
        /// Post file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<T> ExecuteServiceFilePost<T>(string url, byte[] file, string extension)
            where T : RequestViewModelBase
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoadingPopUp());

                // -- Ejecutamos en el view model
                var res = await this.viewModel.ExecuteServiceFilePost<T>(url, file, extension);

                await PopupNavigation.Instance.PopAsync();

                // -- Recorremos los mensajes
                foreach (var msg in this.viewModel.Alerts)
                {
                    // -- Mostramos las alertas
                    await DisplayAlert("Alerta!", msg.Message, "Aceptar");
                }

                // -- Retornamos el response vm
                return res;
            }
            catch (Exception ex)
            {
                // -- Mostramos las alertas
                await DisplayAlert("Alerta!", "Ha ocurrido un error al realizar la operación", "Aceptar");

                return null;
            }
        }
    }
}
