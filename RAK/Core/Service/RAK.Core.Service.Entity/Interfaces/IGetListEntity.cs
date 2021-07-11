namespace RAK.Core.Service.Entity.Interfaces
{
    /// <summary>
    /// Entidad para obtener una lista de entidades
    /// </summary>
    public interface IGetListEntity : IEntityBase
    {
        /// <summary>
        /// Cantidad de elementos a traer
        /// </summary>
        int? Count { get; set; }

        /// <summary>
        /// Cantidad de elementos a descartar desde el pricipio
        /// </summary>
        int? OffSet { get; set; }
    }
}
