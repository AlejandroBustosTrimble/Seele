using RAK.Core.Service.Entity.Interfaces;
using System;

namespace RAK.Core.Service.Entity
{
    /// <summary>
    /// Entidad base
    /// </summary>
    public abstract class EntityBase : IEntityBase
    {
        /// <summary>
        /// FechaHora de Alta
        /// </summary>
        public virtual DateTime CreateDateTime { get; set; }

        /// <summary>
        /// FechaHora de Actualizacion
        /// </summary>
        public virtual DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Activo o no
        /// </summary>
        public virtual bool Active { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public virtual long ID { get; set; }
    }
}
