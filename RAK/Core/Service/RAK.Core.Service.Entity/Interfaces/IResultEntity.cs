namespace RAK.Core.Service.Entity.Interfaces
{
    /// <summary>
    /// Interfaz de entidad que contiene resultados de una operacion
    /// </summary>
    public interface IResultEntity : IEntityBase
    {
        /// <summary>
        /// Obtiene si la operacion fue exitosa o no
        /// </summary>
        bool IsSuccess { get; set; }

        /// <summary>
        /// Cantidad de entidades afectadas
        /// </summary>
        int Count { get; set; }
    }
}
