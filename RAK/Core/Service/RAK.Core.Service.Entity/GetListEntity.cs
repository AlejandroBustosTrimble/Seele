using RAK.Core.Service.Entity.Interfaces;

namespace RAK.Core.Service.Entity
{
    /// <summary>
    /// Entidad para obtener una lista de una entidad
    /// </summary>
    public class GetListEntity : EntityBase, IGetListEntity
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
