using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model.Enums
{
    /// <summary>
    /// Tipos de alertas que devuelve la api
    /// </summary>
    public enum ApiAlertTypeEnum
    {
        // TODO_RAK: Esto deberia a moverlo a un lugar en comun entre APi y Logic, porque puede ser que quiera
        // generar una alerta o informacion desde la logica, el mejor cantidades parece ser en AbstractEntities
        // Porque va a ser un lugar Cross, a su vez me parece que lo mejor va a ser cambiar la carpeta AbstractEntities
        // Por Cross o algo asi y ahi adentro voy a tener por ej RAK.Core.Cross.AbstractEntities y esta enumeracion tambien en algun proyecto
        // Cambiar por AlertTypeEnum

        Success = 1,
        Alert = 2,
        Info = 3,
        Error = 4
    }
}
