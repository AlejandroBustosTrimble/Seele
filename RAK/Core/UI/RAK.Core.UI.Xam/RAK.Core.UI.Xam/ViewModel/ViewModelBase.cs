using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RAK.Core.UI.Xam.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public bool Inicializado { get; set; }

        public virtual async void Inicializar()
        {
        }

        /// <summary>
        /// Alertas
        /// </summary>
        public BusinessValidationCollection Alerts
        {
            get; set;
        } = new BusinessValidationCollection();

        /// <summary>
        /// Ejecuta post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> ExecuteServicePost<T>(string url, object param)
            where T : RequestViewModelBase
        {
            // -- Ejecutamos
            var res = await ServiceHelper.Post<T>(url, param);

            #region Procesamos mensaje

            // -- Seteamos alertas
            this.Alerts = res.Alerts;

            #endregion

            // -- Retornamos el response vm
            return res.ResponseVM;
        }

        /// <summary>
        /// Ejecuta get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<T> ExecuteServiceGet<T>(string url, object param)
           where T : RequestViewModelBase
        {
            // -- Ejecutamos
            var res = await ServiceHelper.Get<T>(url, new ParameterCollection(param));

            #region Procesamos mensaje

            // -- Seteamos alertas
            this.Alerts = res.Alerts;

            #endregion

            // -- Retornamos el response vm
            return res.ResponseVM;
        }

        /// <summary>
        /// Ejecuta post para listas
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> ExecuteServiceListPost<T>(string url, object param)
            where T : RequestViewModelBase
        {
            // -- Ejecutamos
            var res = await ServiceHelper.PostList<T>(url, param);

            #region Procesamos mensaje

            // -- Seteamos alertas
            this.Alerts = res.Alerts;

            #endregion

            // -- Retornamos el response vm
            return res.ResponseVM;
        }

        /// <summary>
        /// Post file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<T> ExecuteServiceFilePost<T>(string url, byte[] file, string extension)
           where T : RequestViewModelBase
        {
            // -- Ejecutamos
            var res = await ServiceHelper.PostFile<T>(url, file, extension);

            #region Procesamos mensaje

            // -- Seteamos alertas
            this.Alerts = res.Alerts;

            #endregion

            // -- Retornamos el response vm
            return res.ResponseVM;
        }


        /// <summary>
        /// Ejecuta get para lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<T>> ExecuteServiceListGet<T>(string url, object param)
           where T : class
        {
            // -- Ejecutamos
            var res = await ServiceHelper.GetList<T>(url, new ParameterCollection(param));

            #region Procesamos mensaje

            // -- Seteamos alertas
            this.Alerts = res.Alerts;

            #endregion

            // -- Retornamos el response vm
            return res.ResponseVM;
        }
    }
}
