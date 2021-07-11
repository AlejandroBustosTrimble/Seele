using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;

namespace RAK.Core.Service.Repository.Abstraction
{
    /// <summary>
    /// Interfaz de Repositorio para Entidades que son Editables con vista basica
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="BIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="BCE"></typeparam>
    /// <remarks>
    /// Util para poder ver una vista minima de la entidad, 
    /// por ej en un control de busqueda de esa entidad
    /// </remarks>
    public interface IFullEditRepository<E, LIE, BIE, LCE, BCE> : IEditRepository<E, LIE, LCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where BIE : class, IBasicEntity
        where LCE : class, IListedCriteriaEntity
        where BCE : class, IBasicCriteriaEntity
    {
        /// <summary>
        /// Obtiene la vista de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ListEntity<BIE> GetBasic(BCE entity);
    }
}
