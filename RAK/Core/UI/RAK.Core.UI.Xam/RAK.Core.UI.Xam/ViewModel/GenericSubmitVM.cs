using RAK.Core.UI.Xam.Helpers;
using RAK.Core.UI.Xam.Model;
using RAK.Core.UI.Xam.MsgCenter;
using RAK.Core.UI.Xam.Page;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.ViewModel
{
	public abstract class GenericSubmitVM<ReqXam, ResXam, M> : GenericVM, IGenericSubmit
	where ReqXam : IRequestXam
	where ResXam : IResponseXam
	where M : IGenericModel<ReqXam, ResXam>, new()
	{
        protected virtual bool ValidateIdentity { get; set; } = false;

		/// <summary>
		/// Msj Center Messasing
		/// </summary>
		public virtual string MessasingCenterMsg { get; set; } = "GenericMsgResponse";

		/// <summary>
		/// Rta Generca para el Msg Center
		/// </summary>
		protected MessagingCenterGenericResponse<ResXam> MsgCenterResponse = new MessagingCenterGenericResponse<ResXam>();

		/// <summary>
		/// Acceso a Modelo
		/// </summary>
		protected M Model = new M();

		/// <summary>
		/// Submit Command
		/// </summary>
		public ICommand SubmitCommand { get { return new Command(Submit); } }

		/// <summary>
		/// Ctor
		/// </summary>
		public GenericSubmitVM()
		{

		}

		/// <summary>
		/// Accion Submit asociada a Comando
		/// </summary>
		public virtual async void Submit()
		{
			try
			{
				MsgCenterResponse.InputValidation = false;
				bool UnauthorizedRES = false;

                var ValidateResponse = ValidateSubmit();
                if (ValidateResponse)
                {
                    var confirmResult = await ConfirmSubmit();
                    if (confirmResult)
                    {
                        if (this.ValidateIdentity)
                        {
                            var IdentityValidator = DependencyService.Get<IIdentityValidation>();
                            // -- Obtenemos el validador y si no es nulo validamos
                            if (IdentityValidator != null)
                            {
                                var resp = await IdentityValidator.ValidateIdentity();
                                if (!resp.Result)
                                {
                                    await ActionHelper.BeginInvokeOnMainThreadAsync(() => this.AssociatePage.MostrarAlerta("Error", resp.Message, "Aceptar"));
                                    return;
                                }
                            }
                        }


                        // -- Mostramos PopUp antes de viajar a servidor
                        if (this.AssociatePage.ShouldShowPopUpOnSubmit)
                        {
                            var popUpResponse = await this.ShowPopUp();

							if (!popUpResponse)
							{
								return;
							}
						}

                        var Req = this.BuildRequest();
                        this.PreSubmitAction();
                        var XAMRes = Model.Submit(Req);
                        this.PostSubmitAction();

						// -- Si no hay conectividad
						if (!XAMRes.HasInternet)
						{
							await this.CustomActionWhenNotInternet();
							return;
						}

						// -- Verificacion de autorizacion al hacer el Request (Si no hay, el Client Http te redirecciona al Login)
						UnauthorizedRES = XAMRes.Unauthorized;
						if (!UnauthorizedRES)
						{
							this.RegisterMessagingCenter();
							if (XAMRes.Alerts != null && XAMRes.Alerts.Count > 0)
							{
								if (XAMRes.Alerts.Any(a => a.Style == ValidationStyle.Error))
								{
									MsgCenterResponse.Result = false;
									MsgCenterResponse.Data = XAMRes.ResponseVM;
									MsgCenterResponse.ErrorMessage = XAMRes.Alerts.Where(a => a.Style == ValidationStyle.Error).FirstOrDefault().Message;
								}
								else
								{
									if (XAMRes.Alerts.Any(a => a.Style == ValidationStyle.Warning))
									{
										MsgCenterResponse.WarningMessage = XAMRes.Alerts.Where(a => a.Style == ValidationStyle.Warning).FirstOrDefault().Message;
									}
									else if (XAMRes.Alerts.Any(a => a.Style == ValidationStyle.Information))
									{
										MsgCenterResponse.WarningMessage = XAMRes.Alerts.Where(a => a.Style == ValidationStyle.Information).FirstOrDefault().Message;
									}
									else
									{
										MsgCenterResponse.SuccessMessage = XAMRes.Alerts.Where(a => a.Style == ValidationStyle.Success).FirstOrDefault().Message;
									}

									MsgCenterResponse.Result = true;
									MsgCenterResponse.Data = XAMRes.ResponseVM;
									AdditionalActionOnSubmitSuccess();
								}
							}
							else
							{
								MsgCenterResponse.Result = true;
								MsgCenterResponse.Data = XAMRes.ResponseVM;
								AdditionalActionOnSubmitSuccess();
							}
						}
					}
				}
				else
				{
					this.RegisterMessagingCenter();
					MsgCenterResponse.InputValidation = true;
					MsgCenterResponse.Result = false;
				}

				if (this.IsPopUpOpened)
				{
					this.ClosePopUp();
				}

				if (!UnauthorizedRES)
					this.MessagingCenterSend();			
            }
            catch (Exception ex)
            {
                await this.OnException();
            }
        }

        protected virtual void PreSubmitAction()
        {

        }

        protected virtual void PostSubmitAction()
        {

        }

		/// <summary>
		/// Accion a realizar cuando no hay internet
		/// </summary>
		protected virtual async Task<bool> CustomActionWhenNotInternet()
		{
			await ActionHelper.BeginInvokeOnMainThreadAsync(() => { this.AssociatePage.MostrarAlerta("Error", NO_CONNECTIVITY_MSG, "Aceptar"); this.ClosePopUp(); });
			return true;
		}

		/// <summary>
		/// Caso confirmacion previo al Submit
		/// </summary>
		/// <returns></returns>
		protected async virtual Task<bool> ConfirmSubmit()
		{
			return true;
		}

		/// <summary>
		/// Msg a enviar al ejecutar funcion Submit
		/// </summary>
		protected virtual void MessagingCenterSend()
		{
			MessagingCenter.Send(MsgCenterResponse, MessasingCenterMsg);
		}

		/// <summary>
		/// Valida el Submit
		/// </summary>
		/// <returns></returns>
		protected virtual bool ValidateSubmit()
		{
			return true;
		}

		/// <summary>
		/// Accion adicional si luego del submit no existen errores
		/// </summary>
		protected virtual void AdditionalActionOnSubmitSuccess()
		{

		}

		/// <summary>
		/// Agrega un error de validacion (ValidateSubmit)
		/// </summary>
		protected void AddValidateError(string msgError)
		{
			this.MsgCenterResponse.ErrorMessage = msgError;
		}

		/// <summary>
		/// Mape de datos: VM (this), a Request de Modelo
		/// </summary>
		protected abstract ReqXam BuildRequest();

		/// <summary>
		/// Caso de error
		/// </summary>
		protected async virtual Task<bool> OnException()
		{
			if (this.IsPopUpOpened)
			{
				await AssociatePage.ClosePopUp();
				IsPopUpOpened = false;
			}



			await ActionHelper.BeginInvokeOnMainThreadAsync(() => this.AssociatePage.MostrarAlerta("Error", "Ha ocurrido un error intentando conectar con el servicio. Por favor verifique que su conectivad funcione correctamente e intente nuevamente", "Aceptar"));



			return true;
		}

		public void RegisterMessagingCenter()
		{
			((ISubmitContentPage)this.AssociatePage).RegisterMessagingCenter();
		}

		/// <summary>
		/// Verifica si un Response tiene alertas y las muestra
		/// </summary>
		public virtual bool VerifyAlertsToShow<R>(IResponseXamPackage<R> Package)
		where R : IResponseXam
		{
			if (Package.Alerts != null && Package.Alerts.Count > 0)
			{
				this.AssociatePage.MostrarAlerta("Error", Package.Alerts[0].Message, "Aceptar");
				return true;
			}
			else
				return false;
		}
	}

	public abstract class GenericSubmitListVM<ReqXam, ResXam, M> : GenericVM, IGenericSubmit
	where ReqXam : IRequestXam
	where ResXam : IResponseXam
	where M : IGenericListModel<ReqXam, ResXam>, new()
	{

		/// <summary>
		/// Msj Center Messasing
		/// </summary>
		public virtual string MessasingCenterMsg { get; set; } = "GenericMsgResponse";

		/// <summary>
		/// Rta Generca para el Msg Center
		/// </summary>
		MessagingCenterGenericListResponse<ResXam> MsgCenterResponse = new MessagingCenterGenericListResponse<ResXam>();

		/// <summary>
		/// Acceso a Modelo
		/// </summary>
		M Model = new M();

		/// <summary>
		/// Submit Command
		/// </summary>
		public ICommand SubmitCommand { get { return new Command(Submit); } }

		/// <summary>
		/// Accion Submit asociada a Comando
		/// </summary>
		public virtual async void Submit()
		{
			await AssociatePage.ShowPopUp();
			var ValidateResponse = ValidateSubmit();
			if (ValidateResponse)
			{
				var Req = this.BuildRequest();
				this.RegisterMessagingCenter();
				var XAMRes = Model.Submit(Req);
				if (XAMRes.Alerts != null && XAMRes.Alerts.Count > 0)
				{
					MsgCenterResponse.Result = false;
					MsgCenterResponse.Error = XAMRes.Alerts[0].Message;
				}
				else
				{
					MsgCenterResponse.Result = true;
					MsgCenterResponse.Data = XAMRes.ResponseVM;
				}

			}
			await AssociatePage.ClosePopUp();
			this.MessagingCenterSend();
		}

		/// <summary>
		/// Msg a enviar al ejecutar funcion Submit
		/// </summary>
		protected virtual void MessagingCenterSend()
		{
			MessagingCenter.Send(MsgCenterResponse, MessasingCenterMsg);
		}

		/// <summary>
		/// Valida el Submit
		/// </summary>
		/// <returns></returns>
		protected virtual bool ValidateSubmit()
		{
			return true;
		}

		/// <summary>
		/// Agrega un error de validacion (ValidateSubmit)
		/// </summary>
		protected void AddValidateError(string msgError)
		{
			this.MsgCenterResponse.Error = msgError;
		}

		/// <summary>
		/// Mape de datos: VM (this), a Request de Modelo
		/// </summary>
		protected abstract ReqXam BuildRequest();

		public void RegisterMessagingCenter()
		{
			throw new System.NotImplementedException();
		}
	}

	/// <summary>
	/// Interfaz GenericSubmitVM
	/// </summary>
	public interface IGenericSubmit : IGenericVM
	{
		void Submit();
		void RegisterMessagingCenter();
	}

}
