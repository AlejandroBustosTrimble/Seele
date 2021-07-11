using RAK.Core.UI.Xam;
using RAK.Core.UI.Xam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.ContextBase
{
    public abstract class GenericXamarinAppContext<A, H, D, U> : IXamarinAppContext
    where A : class, IXamarinAppContext, new()
    where H : class, IXamarinHttpClient, new()
    where D : class, IRAKXamarinDatabase, new()
    where U : class, IXamarinAppUser, new()
    {

        public abstract string ApiURL { get; }
        public abstract string DbUri { get; }
        /// <summary>
        /// Version de la app
        /// </summary>
        public string Version
        {
            get
            {
                return $"{DependencyService.Get<IAppVersionAndBuild>().GetVersionNumber()}";
            }
        }

        /// <summary>
        /// Build de la app
        /// </summary>
        public string Build
        {
            get
            {
                return $"{DependencyService.Get<IAppVersionAndBuild>().GetBuildNumber()}";
            }
        }
        /// <summary>
        /// Indica si se corre sobre un IOS
        /// </summary>
        public bool IsIOS
        {
            get
            {
                return Device.RuntimePlatform == Device.iOS;
            }
        }

        /// <summary>
        /// Parametros Mobile
        /// </summary>
        public List<ParamItemVM> Parameters { get; set; }

        /// <summary>
        /// Retorna el valor de un parametro
        /// </summary>
        public string GetParamValue(string Key)
        {
            return this.Parameters.Where(x => x.Key == Key).FirstOrDefault().Value;
        }

        public string DeviceID
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("DeviceID"))
                    return Application.Current.Properties["DeviceID"].ToString();
                else
                    return string.Empty;
            }
            set { Application.Current.Properties["DeviceID"] = value; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        protected GenericXamarinAppContext()
        {
            // -- Creo el dispatcher por reflection                
            this.HttpClient = Activator.CreateInstance(typeof(H), new object[1] { this.ApiURL }) as H;
            this.Database = Activator.CreateInstance(typeof(D), new object[1] { this.DbUri }) as D;
        }

        public H HttpClient
        {
            get; set;
        }

        public D Database
        {
            get; set;
        }

        private static A instance;
        public static A Instance
        {
            get
            {
                if (instance == null)
                    instance = new A();
                return instance;
            }
        }

        /// <summary>
        /// Limpia el Contexto al Cerrar session
        /// </summary>
        protected abstract Task<bool> ClearContext();

        /// <summary>
        /// Se setea el Token luego del Login
        /// Dado que el primer llamado al Api es sin seguridad de Token
        /// </summary>
        public void SetToken(string Token)
        {
            this.HttpClient.SetTokenValues(Token);
        }

        /// <summary>
        /// Limpia el Contexto al Cerrar session
        /// </summary>
        public abstract Task<bool> ClearContextError();


        /// <summary>
        /// Retorna el usuario actual. Se especifica en cada proyecto 
        /// </summary>
        public abstract U GetCurrentUser();

        /// <summary>
        /// Setea usuario actual
        /// </summary>
        public abstract void SetCurrentUser(U User);

        /// <summary>
        /// Redireccina al Home
        /// </summary>
        public virtual void GoToHome()
        {

        }

    }

    public interface IXamarinAppContext
    {
    }
}
