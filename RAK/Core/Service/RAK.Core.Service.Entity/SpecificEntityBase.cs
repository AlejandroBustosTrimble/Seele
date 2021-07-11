using System;

namespace RAK.Core.Service.Entity
{
    /// <summary>
    /// Representa una entidad que es concreta (que representan funcionalmente a algo de la realidad)
    /// </summary>
    public abstract class SpecificEntityBase
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
