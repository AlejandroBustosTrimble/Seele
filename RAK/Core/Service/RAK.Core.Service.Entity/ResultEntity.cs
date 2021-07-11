using RAK.Core.Service.Entity.Interfaces;

namespace RAK.Core.Service.Entity
{
    /// <summary>
    /// Interfaz de entidad que contiene resultados de una operacion
    /// </summary>
    public class ResultEntity : EntityBase, IResultEntity
    {
        /// <summary>
        /// Obtiene si la operacion fue exitosa o no
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Cantidad de entidades afectadas
        /// </summary>
        public int Count { get; set; }
    }
}
