using RAK.Core.UI.Xam.Model;
using RAK.Core.UI.Xam.MsgCenter;
using RAK.Core.UI.Xam.ViewModel;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Page
{
    public class GenericSubmitPopUp<SubmitBindingContextVM, SubmitResponseVM> : GenericPopUp<SubmitBindingContextVM>, ISubmitContentPage
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

        /// <summary>
        /// Mensaje texto boton caso de Error
        /// </summary>
        protected virtual string CallbackErrorButtonText { get; set; } = "Aceptar";

        /// <summary>
        /// Ctor
        /// </summary>
        public GenericSubmitPopUp()
        {
            //RegisterMessagingCenter();
        }

        /// <summary>
        /// Registro para recibir mensajes
        /// </summary>
        public virtual void RegisterMessagingCenter()
        {
            MessagingCenter.Subscribe<MessagingCenterGenericResponse<SubmitResponseVM>>(bindingContextVM, "GenericMsgResponse", (cback) =>
            {
                this.SubmitCallback(cback);
            });
        }

        /// <summary>
        /// Callback de Respuesta 
        /// </summary>
        public virtual void SubmitCallback(MessagingCenterGenericResponse<SubmitResponseVM> MessagingCallback)
        {
            if (MessagingCallback.Result)
            {
                this.SubmitCallbackSuccess(MessagingCallback.Data);
            }
            else
            {
                this.SubmitCallbackError(MessagingCallback);
            }
            this.Unsuscribe();
        }

        /// <summary>
        /// Caso de exito dentro del submit callback
        /// </summary>
        public virtual async void SubmitCallbackSuccess(SubmitResponseVM VM)
        {
            await DisplayAlert(CallbackSuccessTitleText, CallbackSuccessMsg, CallbackSuccessButtonText);
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
        public virtual async void Unsuscribe()
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
