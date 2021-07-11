using System;

namespace RAK.Core.Service.Entity.Interfaces
{
    /// <summary>
    /// Interfaz para las entidades que son concretas (que representan funcionalmente a algo de la realidad)
    /// </summary>
    public interface ISpecificEntity : IEntityBase
    {
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>
        /// Fecha de actualizacion
        /// </summary>
        DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        Boolean Active { get; set; }
    }
}
