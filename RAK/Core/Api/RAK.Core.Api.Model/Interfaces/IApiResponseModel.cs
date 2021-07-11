using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model.Interfaces
{
    /// <summary>
    /// Interfaz de ViewModel de respuesta de la api que contiene un ViewModel de respuesta
    /// </summary>
    /// <typeparam name="TInnerViewModel"></typeparam>
    public interface IApiResponseModel<TInnerViewModel>
        where TInnerViewModel : IResponseModel
    {
        /// <summary>
        /// Lista de alertas
        /// </summary>
        List<IApiAlertModel> AlertList { get; set; }

        /// <summary>
        /// Datos concretos de la respuesta del APi
        /// </summary>
        TInnerViewModel Data { get; set; }
    }
}
