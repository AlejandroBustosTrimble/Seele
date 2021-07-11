using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RAK.Core.UI.Xam.ViewModel;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Web;
using RAK.Core.UI.Xam.General;
using Xamarin.Forms;
using Newtonsoft.Json.Converters;

namespace RAK.Core.UI.Xam
{
    public class ServiceHelper
    {
        private const string TokenKEY = "Token";
        private const string PerfilKEY = "PerfilID";

		private static string token { get { return DependencyService.Get<IToken>().GetToken(); } }
        private static long perfilID { get { return DependencyService.Get<IPerfil>().GetPerfil(); } }
		private static string urlBase { get { return DependencyService.Get<IApiUrl>().GetUrl(); } }

		public ServiceHelper()
        {

        }

        private static HttpClient Instanciar()
        {
            var httpClient = new HttpClient();

            if (token != null)
                httpClient.DefaultRequestHeaders.Add(TokenKEY, token);

            httpClient.DefaultRequestHeaders.Add(PerfilKEY, perfilID.ToString());

            return httpClient;

        }

        /// <summary>
        /// Ejecuta un get y convierte a objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<GenericResponseVM<T>> Get<T>(string url, ParameterCollection param)
            where T : RequestViewModelBase
        {
            url = Statics.ApiUrl + url;

            if (param != null)
                url += "?" + param;

            var httpClient = Instanciar();

            var response = await httpClient.GetStringAsync(url);

            GenericResponseVM<T> obj = JsonConvert.DeserializeObject<GenericResponseVM<T>>(response, GetIsoDateTimeFormat());

            return obj;
        }

        /// <summary>
        /// Ejectura un post y convierte a objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<GenericResponseVM<T>> Post<T>(string url, object param)
            where T : RequestViewModelBase
        {
            url = Statics.ApiUrl + url;
            var httpClient = Instanciar();

            try
            {
                var json = JsonConvert.SerializeObject(param, GetIsoDateTimeFormat());
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(url, content).Result;
                var contents = await response.Content.ReadAsStringAsync();


                GenericResponseVM<T> obj = JsonConvert.DeserializeObject<GenericResponseVM<T>>(contents, GetIsoDateTimeFormat());

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Ejecuta un get y convierte a objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<GenericListResponseVM<T>> GetList<T>(string url, ParameterCollection param)
            where T : class
        {
            try
            {
                url = Statics.ApiUrl + url;

                if (param != null)
                    url += "?" + param;

                var httpClient = Instanciar();

                var response = await httpClient.GetStringAsync(url);

                GenericListResponseVM<T> obj = JsonConvert.DeserializeObject<GenericListResponseVM<T>>(response, GetIsoDateTimeFormat());

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Ejectura un post y convierte a objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<GenericListResponseVM<T>> PostList<T>(string url, object param)
            where T : RequestViewModelBase
        {
            url = Statics.ApiUrl + url;
            var httpClient = Instanciar();

            try
            {
                var json = JsonConvert.SerializeObject(param, GetIsoDateTimeFormat());
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(url, content).Result;
                var contents = await response.Content.ReadAsStringAsync();


                GenericListResponseVM<T> obj = JsonConvert.DeserializeObject<GenericListResponseVM<T>>(contents, GetIsoDateTimeFormat());

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Post file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<GenericResponseVM<T>> PostFile<T>(string url, byte[] file, string extension)
          where T : RequestViewModelBase
        {
            url = Statics.ApiUrl + url;
            var httpClient = Instanciar();

            try
            {
                MultipartFormDataContent content = new MultipartFormDataContent();
                ByteArrayContent baContent = new ByteArrayContent(file);
                content.Add(baContent, "File", $"filename.{extension}");
                var response = httpClient.PostAsync(url, content).Result;
                var contents = await response.Content.ReadAsStringAsync();

                GenericResponseVM<T> obj = JsonConvert.DeserializeObject<GenericResponseVM<T>>(contents, GetIsoDateTimeFormat());

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Obtiene el tipo de formate de fecha
        /// </summary>
        /// <returns></returns>
        private static IsoDateTimeConverter GetIsoDateTimeFormat()
        {
            return new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy HH:mm:ss" };
        }
    }

    public class ParameterCollection
    {
        private Dictionary<string, string> _parms = new Dictionary<string, string>();

        #region Contructores

        public ParameterCollection()
        {

        }

        public ParameterCollection(object obj)
        {
            this.LoadFromObject(obj);
        }

        #endregion

        public void Add(string key, string val)
        {
            if (_parms.ContainsKey(key))
            {
                throw new InvalidOperationException(string.Format("The key {0} already exists.", key));
            }
            _parms.Add(key, val);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var kvp in _parms)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.AppendFormat("{0}={1}",
                    kvp.Key,
                    kvp.Value);
            }
            return sb.ToString();
        }

        public void LoadFromObject(object obj)
        {
            var dictionary = obj.ToDictionary();
            foreach (var item in dictionary)
            {
                if (item.Value != null)
                    this._parms.Add(item.Key, item.Value.ToString());
                else
                    this._parms.Add(item.Key, "");
            }
        }

        public object ToObject()
        {
            object obj = new object();
            foreach (var item in _parms)
            {
                //obj[item.Key] = item.Value;
                obj.AddProperty(item.Key, item.Value);
            }
            return obj;
        }
    }
}
