using RAK.Core.UI.Xam.Page;
using Rg.Plugins.Popup.Services;
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
	public partial class IconAlert : GenericPopUp<IconAlertViewModel>
	{
		#region Task

		private TaskCompletionSource<bool> taskCompletionSource;

		/// <summary>
		/// Muestra el alert con icono
		/// </summary>
		/// <param name="icon">Icono</param>
		/// <param name="title">Titulo</param>
		/// <param name="text">Contenido</param>
		/// <param name="cancel">Texto de boton cancelar</param>
		/// <param name="ok">Texto de boton aceptar. Mandar vacio si no debe tener este boton</param>
		/// <param name="color">Color de botones. Mandar vacio si no son necesarios</param>
		/// <returns></returns>
		public static async Task<bool> CreateIconAlertAccept(string icon, string title, string text, string cancel, string ok, string color = "")
		{
			// -- Instanciamos el pop up
			var popup = new IconAlert(icon, title, text, cancel, ok, color);
			// -- Se agrega el pop up
			await PopupNavigation.Instance.PushAsync(popup);
			// -- Esperamos el resultado
			var result = await popup.Process();
			// -- Cerramos el popup
			await PopupNavigation.Instance.PopAsync();

			return result;
		}

		/// <summary>
		/// Muestra el alert con icono
		/// </summary>
		/// <param name="icon">Icono</param>
		/// <param name="title">Titulo</param>
		/// <param name="text">Contenido</param>
		/// <param name="cancel">Texto de boton cancelar</param>
		/// <param name="ok">Texto de boton aceptar. Mandar vacio si no debe tener este boton</param>
		/// <param name="color">Color de botones. Mandar vacio si no son necesarios</param>
		/// <returns></returns>
		public static async Task CreateIconAlert(string icon, string title, string text, string cancel, string color = "")
		{
			// -- Instanciamos el pop up con el boton ok como vacio
			var popup = new IconAlert(icon, title, text, cancel, string.Empty, color);
			// -- Se agrega el pop up
			await PopupNavigation.Instance.PushAsync(popup);
		}

		/// <summary>
		/// Genera la task
		/// </summary>
		/// <returns></returns>
		private Task<bool> Process()
		{
			taskCompletionSource = new TaskCompletionSource<bool>();
			return taskCompletionSource.Task;
		}

		protected override bool OnBackgroundClicked()
		{
			return false;
		}

		public async void SetResult(bool result)
		{
			// -- Si tiene boton aceptar debemos setear el resultado
			if (this.bindingContextVM.HasAccept)
			{
				taskCompletionSource.SetResult(result);
				taskCompletionSource = null;
			}
			// -- Si no tiene boton aceptar solamente tenemos que cerrar el popup
			else
			{
				// -- Cerramos el popup
				await PopupNavigation.Instance.PopAsync();
			}
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="icon">Icono que tendra el popup</param>
		/// <param name="title">Titulo</param>
		/// <param name="text">Contenido</param>
		/// <param name="ok">Texto boton ok</param>
		/// <param name="cancel">Texto boton cancelar</param>
		/// <param name="color">Color para botones</param>
		/// <param name=""></param>
		public IconAlert(string icon, string title, string text, string cancel, string ok, string color)
		{
			InitializeComponent();
			this.bindingContextVM.Icon = icon;
			this.bindingContextVM.Title = title;
			this.bindingContextVM.Text = text;
			this.bindingContextVM.OK = ok.ToUpper();
			this.bindingContextVM.Cancel = cancel.ToUpper();
			if (!string.IsNullOrEmpty(color))
				this.bindingContextVM.ButtonColor = color;
			if (!string.IsNullOrEmpty(ok))
			{
				this.bindingContextVM.HasAccept = true;
			}

		}
	}
}