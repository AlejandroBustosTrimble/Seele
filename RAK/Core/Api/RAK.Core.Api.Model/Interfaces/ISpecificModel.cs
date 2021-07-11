using System;

namespace RAK.Core.Api.Model.Interfaces
{
    /// <summary>
    /// Interfaz que representa un modelo de una entidad concreta
    /// </summary>
    public interface ISpecificModel : IModelBase, IRequestModel, IResponseModel
    {
        /// <summary>
        /// FechaHora de creacion
        /// </summary>
        DateTime CreateDateTime { get; set; }

        /// <summary>
        /// FechaHora de actualizacion
        /// </summary>
        DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// Activo
        /// </summary>
        Boolean Active { get; set; }
    }
}
