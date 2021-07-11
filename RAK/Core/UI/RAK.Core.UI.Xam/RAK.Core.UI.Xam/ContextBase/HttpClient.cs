using RAK.Core.UI.Xam.Helpers;
using RAK.Core.UI.Xam.Model;
using RAK.Fwk.Common.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RAK.Core.UI.Xam.ContextBase
{
    public abstract class BasicXamarinHttpClient : BasicHttpClient<IRequestXam, IResponseXamPackageCommon>, IXamarinHttpClient
    {

        /// <summary>
        /// Timeout de la plataforma (llamadas Api)
        /// </summary>
        const int AppTimeOutDefault = 120000;
        public override int? GlobalTimeout { get; set; } = AppTimeOutDefault;

        protected override bool UseToken { get; set; } = true;
        protected override bool UseBasicAuthentication { get { return false; } }
        protected override string TokenKEY { get; set; } = "Token";

        public BasicXamarinHttpClient()
        {

        }
        /// <summary>
        /// Ctor
        /// </summary>
        public BasicXamarinHttpClient(string pUrl) : base(pUrl)
        {
            base.UseToken = true;
        }

        /// <summary>
        /// Se setea el Token luego del Login
        /// Dado que el primer llamado al Api es sin seguridad de Token
        /// Se llama desde el Contexto
        /// </summary>
        public void SetTokenValues(string value)
        {
            this.TokenValue = value;
        }

        /// <summary>
        /// Obtiene el valor del token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            return this.TokenValue;
        }


        /// <summary>
        /// Override de ExecutePOST para manejo de ciertas excepciones
        /// </summary>
        public override Res ExecutePOST<Req, Res>(Req Request, string action)
        {
            try
            {
                if (ConnectivityHelper.HasInternet)
                    return base.ExecutePOST<Req, Res>(Request, action);
                else
                {
                    Res Response = new Res();
                    ((IResponseXamPackageCommon)Response).HasInternet = false;
                    return Response;
                }

            }
            catch (Exception ex)
            {
                var result = ExceptionHelper.isUnauthorized(ex);
                if (result)
                {
                    Res Response = new Res();
                    ((IResponseXamPackageCommon)Response).Unauthorized = result;
                    RedirectToLogin();
                    return Response;
                }
                else
                {
                    OnException(ex);
                }
                throw ex;
            }
        }

        /// <summary>
        /// Override de ExecuteGET para manejo de ciertas excepciones
        /// </summary>
        public override Res ExecuteGET<Req, Res>(Req Request, string action)
        {
            try
            {
                if (ConnectivityHelper.HasInternet)
                    return base.ExecuteGET<Req, Res>(Request, action);
                else
                {
                    Res Response = new Res();
                    ((IResponseXamPackageCommon)Response).HasInternet = false;
                    return Response;
                }
            }
            catch (Exception ex)
            {
                var result = ExceptionHelper.isUnauthorized(ex);
                if (result)
                {
                    Res Response = new Res();
                    ((IResponseXamPackageCommon)Response).Unauthorized = result;
                    RedirectToLogin();
                    return Response;
                }
                else
                {
                    OnException(ex);
                }
                throw ex;
            }
        }

        /// <summary>
        /// Override de ExecuteGET para manejo de ciertas excepciones
        /// </summary>
        public override Res ExecuteGET<Res>(string action)
        {
            try
            {
                if (ConnectivityHelper.HasInternet)
                    return base.ExecuteGET<Res>(action);
                else
                {
                    Res Response = new Res();
                    ((IResponseXamPackageCommon)Response).HasInternet = false;
                    return Response;
                }
            }
            catch (Exception ex)
            {
                var result = ExceptionHelper.isUnauthorized(ex);
                if (result)
                {
                    Res Response = new Res();
                    ((IResponseXamPackageCommon)Response).Unauthorized = result;
                    RedirectToLogin();
                    return Response;
                }
                else
                {
                    OnException(ex);
                }
                throw ex;
            }
        }

        /// <summary>
        /// Redireciona al login y limpia el contexto en caso de que no haya autorizacion (token vencido)
        /// </summary>
        protected virtual async void RedirectToLogin()
        {

        }

        protected virtual void OnException(Exception ex)
        {
        }
    }

    public interface IXamarinHttpClient
    {
        void SetTokenValues(string Token);
    }
}
