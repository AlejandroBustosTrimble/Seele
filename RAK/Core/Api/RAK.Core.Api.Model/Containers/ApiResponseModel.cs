using RAK.Core.Api.Model.Interfaces;
using System.Collections.Generic;

namespace RAK.Core.Api.Model.Containers
{
    /// <summary>
    /// Interfaz de ViewModel de respuesta de la api que contiene un ViewModel de respuesta
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ApiResponseModel<TModel> : IApiResponseModel<TModel>
         where TModel : IResponseModel
    {
        /// <summary>
        /// Lista de alertas
        /// </summary>
        public List<IApiAlertModel> AlertList { get; set; }

        /// <summary>
        /// Datos concretos de la respuesta del API
        /// </summary>
        public TModel Data { get; set; }
    }
}
