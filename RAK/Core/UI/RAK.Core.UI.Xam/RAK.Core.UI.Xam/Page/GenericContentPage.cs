using RAK.Core.UI.Xam.Controls.ModalLoading;
using RAK.Core.UI.Xam.DependenciesInterfaces;
using RAK.Core.UI.Xam.ViewModel;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.Page
{
	public abstract class GenericContentPage<BindingContextVM> : ContentPage, IGenericContentPage
	where BindingContextVM : IGenericVM, new()
	{
		public bool Inicializado = false;

		/// <summary>
		/// VM asociado al Contexto
		/// </summary>
		protected BindingContextVM bindingContextVM { get; set; }

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
		/// Ctor
		/// </summary>
		public GenericContentPage()
		{
			this.CreateBindingContext();
			this.BindingContext = bindingContextVM;
			bindingContextVM.AssociatePage = this;

			#region Back button text

			var backButtonImplementation = DependencyService.Get<IBackButtonText>();

			if (backButtonImplementation != null)
				NavigationPage.SetBackButtonTitle(this, backButtonImplementation.GetText());

			#endregion
		}

		/// <summary>
		/// Instancia  el bindingContextVM. 
		/// Se lo pasa a un metodo, dado que en ciertas paginas el contexto va a venir de otra (Varias paginas, mismo viewmodel). 
		/// </summary>
		protected virtual void CreateBindingContext()
		{
			bindingContextVM = new BindingContextVM();
		}

		/// <summary>
		/// Obtiene si tiene que mostrar el popup al submitear
		/// </summary>
		public virtual bool ShouldShowPopUpOnSubmit
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// ShowPopUp
		/// Por defecto muestra el Loading. 
		/// En caso de querer uno especifico creamos dependencia en proyecto concreto
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
			this.bindingContextVM.AssociatePage = this;
		}

		protected override bool OnBackButtonPressed()
		{
			var dependecy = DependencyService.Get<IMinimize>();
			// -- Validamos que este implementada la interfaz
			// -- Se agrega validacion porque no se podia llamar al comportamiento base de ContentPage desde una pagina que herede de esta base
			if (dependecy != null)
			{
				Device.BeginInvokeOnMainThread(new Action(async () =>
				{
					dependecy.Minimize();
				}));

				return true;
			}
			else
			{
				return base.OnBackButtonPressed();
			}
		}

	}

	public interface ISubmitContentPage : IGenericContentPage
	{
		void RegisterMessagingCenter();
	}

	public interface IGenericContentPage
	{
		bool ShouldShowPopUpOnSubmit { get; }
		Task<bool> ShowPopUp();
		Task<bool> ClosePopUp();
		Task<bool> MostrarAlertaAceptar(string title, string message, string accept, string cancel, string icon = "");
		void MostrarAlerta(string title, string message, string accept, string icon = "");
	}
}
