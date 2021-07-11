using RAK.Core.Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model
{
    /// <summary>
    /// ViewModel que contiene el resultado de una operacion
    /// </summary>
    public class ResultModel : ModelBase, IResultModel
    {
        /// <summary>
        /// Obtiene si la operacion termino con exito o no
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Cantidad de entidades afectadas
        /// </summary>
        public int Count { get; set; }
    }
}
