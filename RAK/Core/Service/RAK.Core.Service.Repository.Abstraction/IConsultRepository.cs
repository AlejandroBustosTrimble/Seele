using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Fwk.Service.Repository.Abstraction;

namespace RAK.Core.Service.Repository.Abstraction
{
    /// <summary>
    /// Interfaz de Repositorios de SOLO Consulta
    /// </summary>
    public interface IConsultRepository<E, LIE, LCE> : IRepositoryGeneric
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where LCE : class, IListedCriteriaEntity
    {
        /// <summary>
        /// Obtiene una entidad por ID
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        E GetByID(IdentityEntity entity);

        /// <summary>
        /// Obtiene la vista de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ListEntity<LIE> GetListed(LCE entity);

        /// <summary>
        /// Obtiene una lista de una entidad con todos sus registros
        /// </summary>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        ListEntity<E> GetAll(GetListEntity entity);

        /// <summary>
        /// Obtiene una lista de una entidad en base a un filtro
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        ListEntity<E> GetByCriteria(CriteriaEntity<E> entity);
    }
}
