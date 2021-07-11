using RAK.Core.UI.Xam.Controls.Toast;
using RAK.Core.UI.Xam.Model;
using RAK.Core.UI.Xam.Page;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.ViewModel
{

    /// <summary>
    /// VM Basico
    /// </summary>
    public abstract class GenericVM : INotifyPropertyChanged, IGenericVM
    {
        /// <summary>
        /// Indica si la PopUp (Cargando..) fue abierta
        /// </summary>
        protected bool IsPopUpOpened { get; set; } = false;

        private object syncLock = new object();

        public async Task<Boolean> ShowPopUp()
        {
            lock (syncLock)
            {
                if (IsPopUpOpened)
                {
                    return false;
                }

                IsPopUpOpened = true;
            }

            await this.AssociatePage.ShowPopUp();

            return IsPopUpOpened;
        }

        public async void ClosePopUp()
        {
            await this.AssociatePage.ClosePopUp();
            lock (syncLock)
            {
                IsPopUpOpened = false;
            }
        }

        protected string NO_CONNECTIVITY_MSG = "Ups, no se encuentra conectado a Internet";
        protected string GENERIC_ERROR_MSG = "En este momento no se puede realizar la operación";

        /// <summary>
        /// Indica si al ejecutar la primera accion hay o no autorizacion
        /// Sirve para verificar en caso de tener mas de una, si hay que seguir o no
        /// </summary>
        public bool Unauthorized { get; set; } = false;

        bool ejecutando;
        /// <summary>
        /// Indica si se esta ejecutando el ejecute action async
        /// </summary>
        public bool EjecutandoAsync { get => ejecutando; set { ejecutando = value; RaisePropertyChanged(); } }

        public IGenericContentPage AssociatePage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        /// <summary>
        /// Al iniciar el VM
        /// </summary>
        public virtual void OnInit()
        {
            this.ExecuteAction(this.OnPreInit, this.OnPostInit);
        }

        /// <summary>
        /// Ejecuta acciones y verifica que exista autorizacion en el contexto del Request
        /// Caso de que no haya autorizacion no ejecuta el 'PostInit'
        /// </summary>
        /// <param name="mainFuncToExecute">Caso por defecto PreInit</param>
        /// <param name="funcToExecuteIfSuccess">Caso por defecto PostInit</param>
        protected virtual void ExecuteAction(Func<IResponseXamPackageCommon> mainFuncToExecute, Action<IResponseXamPackageCommon> funcToExecuteIfSuccess)
        {
            try
            {
                if (!this.Unauthorized)
                {
                    var response = mainFuncToExecute();
                    if (response != null)
                    {
                        if (response.HasInternet)
                        {
                            this.Unauthorized = response.Unauthorized;
                            if (!response.Unauthorized)
                            {
                                funcToExecuteIfSuccess(response);
                            }
                        }
                        else
                        {
                            // -- Usamos Toast en vez Alerta por problema llamada OnInit en OnAppering (no llega a renderizarse).
                            Helpers.ActionHelper.BeginInvokeOnMainThreadAsync(() =>
                            {
                                DependencyService.Get<IRAKToast>().ShowLong(NO_CONNECTIVITY_MSG);
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }

        /// <summary>
        /// Ejecuta acciones y verifica que exista autorizacion en el contexto del Request
        /// Caso de que no haya autorizacion no ejecuta el 'PostInit'
        /// </summary>
        /// <param name="mainFuncToExecute">Caso por defecto PreInit</param>
        /// <param name="funcToExecuteIfSuccess">Caso por defecto PostInit</param>
        protected virtual void ExecuteActionAsync(Func<IResponseXamPackageCommon> mainFuncToExecute, Action<IResponseXamPackageCommon> funcToExecuteIfSuccess)
        {
            Task.Factory.StartNew(() =>
            {
                this.ExecuteActionAsyncOnPreExecute();
                this.ExecuteAction(mainFuncToExecute, funcToExecuteIfSuccess);
            }).ContinueWith((t) =>
            {
                ExecuteActionAsyncOnFinish();
                if (t.Exception != null)
                {
                    var ex = t.Exception.GetBaseException();
                }

            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.Default);
        }


        /// <summary>
        /// PreInit
        /// Actualmente utilizado para validar si existe o no autorizacion 
        /// </summary>
        /// <returns></returns>
        protected virtual IResponseXamPackageCommon OnPreInit()
        {
            return null;
        }

        /// <summary>
        /// En caso de autorizacion se continua con la ejecucion
        /// </summary>
        protected virtual void OnPostInit(IResponseXamPackageCommon Response)
        {
        }

        /// <summary>
        /// Posibilidad de agregar alguna accion antes de que ExecuteActionAsync ejecute el primer mainFunc
        /// </summary>
        protected virtual void ExecuteActionAsyncOnPreExecute()
        {
            EjecutandoAsync = true;
        }

        /// <summary>
        /// Posibilidad de agregar alguna accion una vez ejecutado ExecuteActionAsync (ContinueWith).
        /// </summary>
        protected virtual void ExecuteActionAsyncOnFinish()
        {
            EjecutandoAsync = false;
        }

        /// <summary>
        /// Caso de Excepcion
        /// </summary>
        protected virtual void HandleException(Exception ex)
        {
            Helpers.ActionHelper.BeginInvokeOnMainThreadAsync(() =>
            {
                DependencyService.Get<IRAKToast>().ShowLong(GENERIC_ERROR_MSG);
            });
        }
    }

    /// <summary>
    /// Interfaz mas base de todas para Vms
    /// </summary>
    public interface IGenericVM
    {
        IGenericContentPage AssociatePage { get; set; }
        void OnInit();
    }
}
