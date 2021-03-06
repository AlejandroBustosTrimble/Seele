using RAK.Core.UI.Xam.Controls.ModalLoading;
using RAK.Core.UI.Xam.Model;
using RAK.Core.UI.Xam.MsgCenter;
using RAK.Core.UI.Xam.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Page
{
    /// <summary>
    /// ContentPage base para trabajar con VM's Submits
    /// </summary>
    public abstract class GenericSubmitPage<SubmitBindingContextVM, SubmitResponseVM> : GenericContentPage<SubmitBindingContextVM>, ISubmitContentPage
    where SubmitBindingContextVM : IGenericSubmit, new()
    where SubmitResponseVM : IResponseXam
    {
        /// <summary>
        /// Msj que muestra en caso de un callback exitoso
        /// </summary>
        protected virtual string CallbackSuccessMsg { get; set; } = "La operacion se realizo correctamente";

        /// <summary>
        /// Mensaje texto boton caso de exito
        /// </summary>
        protected virtual string CallbackSuccessButtonText { get; set; } = "Aceptar";

        /// <summary>
        /// Titulo del Alert en caso exito
        /// </summary>
        protected virtual string CallbackSuccessTitleText { get; set; } = "Exito";

        /// <summary>
        /// Titulo del Alert en caso Error
        /// </summary>
        protected virtual string CallbackErrorTitleText { get; set; } = "Error";

        protected virtual string CallbackSuccessWarningTitleText { get; set; } = "Alerta";

        protected virtual string CallbackSuccessInfoTitleText { get; set; } = "Información";

        /// <summary>
        /// Mensaje texto boton caso de Error
        /// </summary>
        protected virtual string CallbackErrorButtonText { get; set; } = "Aceptar";

        /// <summary>
        /// Ctor
        /// </summary>
        public GenericSubmitPage()
        {
           
        }

        /// <summary>
        /// Registro para recibir mensajes
        /// </summary>
        public virtual void RegisterMessagingCenter()
        {
            MessagingCenter.Subscribe<MessagingCenterGenericResponse<SubmitResponseVM>>(this.bindingContextVM, "GenericMsgResponse", (cback) =>
            {
                this.SubmitCallback(cback);
            });
        }

        /// <summary>
        /// Callback de Respuesta 
        /// </summary>
        public virtual void SubmitCallback(MessagingCenterGenericResponse<SubmitResponseVM> MessagingCallback)
        {
            this.Unsuscribe();
            if (MessagingCallback.Result)
            {
                this.SubmitCallbackSuccess(MessagingCallback.Data, MessagingCallback);
            }
            else
            {
                this.SubmitCallbackError(MessagingCallback);
            }
        }

        /// <summary>
        /// Caso de exito dentro del submit callback
        /// </summary>
        public virtual async void SubmitCallbackSuccess(SubmitResponseVM VM, MessagingCenterGenericResponse<SubmitResponseVM> messagingCallback)
        {
            var msg = CallbackSuccessMsg;
            var title = CallbackSuccessTitleText;
            if (!String.IsNullOrEmpty(messagingCallback.SuccessMessage))
            {
                msg = messagingCallback.SuccessMessage;
            }
            else if(!String.IsNullOrEmpty(messagingCallback.WarningMessage))
            {
                msg = messagingCallback.WarningMessage;
                title = CallbackSuccessWarningTitleText;
            }
            else if(!String.IsNullOrEmpty(messagingCallback.InfoMessage))
            {
                msg = messagingCallback.InfoMessage;
                title = CallbackSuccessInfoTitleText;
            }


            await DisplayAlert(title, msg, CallbackSuccessButtonText);
            this.Redirect();
        }

        /// <summary>
        /// Caso fracaso dentro de submit callback
        /// </summary>
        public virtual void SubmitCallbackError(MessagingCenterGenericResponse<SubmitResponseVM> MessagingCallback)
        {
            DisplayAlert(CallbackErrorTitleText, MessagingCallback.ErrorMessage, CallbackErrorButtonText);
        }

        /// <summary>
        /// Se quita suscripcion de MessagingCenter
        /// </summary>
        public virtual void Unsuscribe()
        {
            MessagingCenter.Unsubscribe<MessagingCenterGenericResponse<SubmitResponseVM>>(this.bindingContextVM, "GenericMsgResponse");
        }

        /// <summary>
        /// Redireccion final en caso de exito
        /// </summary>
        public virtual void Redirect()
        {

        }

    }
}
