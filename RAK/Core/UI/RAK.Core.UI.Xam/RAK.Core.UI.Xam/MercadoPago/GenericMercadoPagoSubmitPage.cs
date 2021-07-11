using RAK.Core.UI.Xam.Model;
using RAK.Core.UI.Xam.Page;
using RAK.Core.UI.Xam.ViewModel;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Xam.Plugin.WebView.Abstractions;
using Xam.Plugin.WebView.Abstractions.Enumerations;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.MercadoPago
{

    /// <summary>
    /// Pagina Base Generica para MercadoPago
    /// Contiene WebView con Source
    /// </summary>
    public abstract class GenericMercadoPagoSubmitPage<SubmitBindingContextVM, SubmitResponseVM> : GenericSubmitPage<SubmitBindingContextVM, SubmitResponseVM>
    where SubmitBindingContextVM : IGenericSubmit, new()
    where SubmitResponseVM : IResponseXam
    {

        /// <summary>
        /// Key para MercadoPago
        /// </summary>
        protected abstract string MERCADO_PAGO_PUBLIC_KEY { get; set; }

        /// <summary>
        /// MainLayout -> A setear en cada implementacion
        /// </summary>
        protected abstract StackLayout MainLayout { get; }

        /// <summary>
        /// WebView MercadoPago
        /// </summary>
        public FormsWebView webViewMercadoPago { get; set; } = new FormsWebView();

        /// <summary>
        /// Tipo de Contenido del WebView. Por defecto StringData, se puede cambiar haciendo Override
        /// </summary>
        public virtual WebViewContentType CurrentContentType { get; set; } = WebViewContentType.LocalFile;


        /// <summary>
        /// Ctor
        /// </summary>
        public GenericMercadoPagoSubmitPage()
        {
            
        }

        /// <summary>
        /// Inicia el Proceso de MercadoPago
        /// </summary>
        public void Start()
        {
            this.webViewMercadoPago.OnContentLoaded += WebViewMercadoPago_OnContentLoaded;

            // -- Asociamos WebView a Pagina
            this.webViewMercadoPago.IsVisible = false;
            this.webViewMercadoPago.ContentType = CurrentContentType;
            this.webViewMercadoPago.HeightRequest = 0;
            this.webViewMercadoPago.Source = SetWebViewSource();
            this.MainLayout.Children.Add(webViewMercadoPago);
        }

        /// <summary>
        /// Evento de WebView -> OnContentLoad
        /// </summary>
        private void WebViewMercadoPago_OnContentLoaded(object sender, EventArgs e)
        {
            // -- Iniciamos MercadoPago con PublicKey
            this.ExecuteInit();

            // -- Acciones extras
            this.OnLoadWebViewAction();
        }

        /// <summary>
        /// Acciones a realizar cuando el WebView se termina de Cargar
        /// </summary>
        protected virtual void OnLoadWebViewAction()
        {

        }

        /// <summary>
        /// Retorna string con Javascript basico de MercadoPago
        /// </summary>
        protected virtual string SetWebViewSource()
        {
            return "MercadoPago.html";
        }

        /// <summary>
        /// Registra un Callback MercadoPago
        /// </summary>
        protected virtual void RegisterCallBack(MercadoPagoCallBackKeys CallbackKey, Action<string> Action)
        {
            this.webViewMercadoPago.AddLocalCallback(CallbackKey.ToString(), Action);
        }

        #region Metodos MercadoPago

        /// <summary>
        /// Inicia el proceso de MercadoPago, ejecutando la funcion de MercadoPago: Init
        /// </summary>
        public virtual void ExecuteInit()
        {
            this.ExecuteScript($"{MercadoPagoConstKeys.INIT_JS_FUNCTION}('{this.MERCADO_PAGO_PUBLIC_KEY}');");
        }

        /// <summary>
        /// Ejecuta el metodo de MercadoPago: GuessingPaymentMethod
        /// </summary>
        public virtual void ExecuteGuessing(string cardNumber)
        {
            this.ExecuteScript($"{MercadoPagoConstKeys.GUESSING_PAYMENT_METHOD_JS_FUNCTION}('{cardNumber}');");
        }

        /// <summary>
        /// Ejecuta el metodo de MercadoPago: ClearSession
        /// </summary>
        public virtual void ExecuteClearSession()
        {
            this.ExecuteScript($"{MercadoPagoConstKeys.CLEAR_SESSION_JS_FUNCTION}();");
        }

        /// <summary>
        /// Ejecuta el metodo de MercadoPago: CreateSimpleCardFormToken
        /// </summary>
        public virtual void ExecuteSimpleCardForm(string CardID, string SecurityCode)
        {
            this.ExecuteScript($@"{MercadoPagoConstKeys.CREATE_SIMPLE_CARD_FORM_TOKEN_JS_FUNCTION}('{CardID}','{SecurityCode}');");
        }

        /// <summary>
        /// Ejecuta el metodo de MercadoPago: CreateFullCardFormToken
        /// </summary>
        public virtual void ExecuteFullCardForm(string Email, string CardNumber, string SecurityCode, string Month, string Year, string CardholderName, string DocType, string DocNumber)
        {
            var selectedMonthFormatted = Month.PadLeft(2, '0');
            var script = $@"{MercadoPagoConstKeys.CREATE_FULL_CARD_FORM_TOKEN_JS_FUNCTION}('{Email}', '{CardNumber}', '{SecurityCode}', '{selectedMonthFormatted}', '{Year}', '{CardholderName}', '{DocType}', '{DocNumber}');";

            this.ExecuteScript(script);
        }

        /// <summary>
        /// Ejecuta un script JS
        /// </summary>
        protected void ExecuteScript(string Script)
        {
            this.webViewMercadoPago.InjectJavascriptAsync(Script);
        }

        #endregion

    }

    /// <summary>
    /// Callbacks
    /// </summary>
    public enum MercadoPagoCallBackKeys
    {
        InitDocumentTypesDropDownCallbackd,
        GuessingPaymentMethodCallbackd,
        CreateTokenCallbackd
    }

    /// <summary>
    /// MP Constantes
    /// </summary>
    public class MercadoPagoConstKeys
    {
        // -- Keys para callbacks
        public const string INIT_DOC_TYPES_DROPDOWN_CALLBACK = "InitDocumentTypesDropDownCallbackd";
        public const string GUESSING_PAYMENT_METHOD_CALLBACK = "GuessingPaymentMethodCallbackd";
        public const string CREATE_TOKEN_CALLBACK = "CreateTokenCallbackd";

        public const string INIT_JS_FUNCTION = "Init";
        public const string GUESSING_PAYMENT_METHOD_JS_FUNCTION = "GuessingPaymentMethod";
        public const string CLEAR_SESSION_JS_FUNCTION = "ClearSession";
        public const string CREATE_FULL_CARD_FORM_TOKEN_JS_FUNCTION = "CreateFullCardFormToken";
        public const string CREATE_SIMPLE_CARD_FORM_TOKEN_JS_FUNCTION = "CreateSimpleCardFormToken";
    }

}
