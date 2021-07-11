using RAK.Core.Api.Model.Interfaces;
using System;

namespace RAK.Core.Api.Model
{
    /// <summary>
    /// Representa un modelo de una entidad concreta
    /// </summary>
    public class SpecificModelBase : ModelBase, ISpecificModel
    {
        /// <summary>
        /// FechaHora de creacion
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// FechaHora de actualizacion
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Activo
        /// </summary>
        public Boolean Active { get; set; }
    }
}
