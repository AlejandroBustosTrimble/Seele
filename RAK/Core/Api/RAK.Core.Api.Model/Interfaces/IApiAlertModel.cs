using RAK.Core.Api.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model.Interfaces
{
    /// <summary>
    /// Representa una alerta de la respuesta del Api
    /// </summary>
    public interface IApiAlertModel
    {
        /// <summary>
        /// Tipo de Alerta
        /// </summary>
        ApiAlertTypeEnum Type { get; set; }

        /// <summary>
        /// Codigo (Podria ser un numero de una validacion)
        /// </summary>
        String Code { get; set; }

        /// <summary>
        /// Identificador unico de la alerta (Podria ser en el caso de un error un Guid para buscarlo en los logs mas rapido)
        /// </summary>
        String UniqueIdentifier { get; set; }

        /// <summary>
        /// Mensaje
        /// </summary>
        String Message { get; set; }
    }
}
