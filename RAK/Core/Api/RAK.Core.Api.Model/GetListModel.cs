using RAK.Core.Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model
{
    /// <summary>
    /// ViewModel para obtener una lista de una entidad
    /// </summary>
    public class GetListModel : ModelBase, IGetListModel, IRequestModel
    {
        /// <summary>
        /// Cantidad de elementos a traer
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Cantidad de elementos a descartar desde el pricipio
        /// </summary>
        public int? OffSet { get; set; }
    }
}
