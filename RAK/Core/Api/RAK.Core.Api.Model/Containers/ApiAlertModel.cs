using RAK.Core.Api.Model.Enums;
using RAK.Core.Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model.Containers
{
    /// <summary>
    /// ViewModel que representa una alerta
    /// </summary>
    public class ApiAlertModel : IApiAlertModel
    {
        /// <summary>
        /// Tipo de Alerta
        /// </summary>
        public ApiAlertTypeEnum Type { get; set; }

        /// <summary>
        /// Codigo (Podria ser un numero de una validacion)
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// Identificador unico de la alerta (Podria ser en el caso de un error un Guid para buscarlo en los logs mas rapido)
        /// </summary>
        public String UniqueIdentifier { get; set; }

        /// <summary>
        /// Mensaje
        /// </summary>
        public String Message { get; set; }
    }
}
