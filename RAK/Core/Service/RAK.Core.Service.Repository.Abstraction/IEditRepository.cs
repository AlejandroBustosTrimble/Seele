using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;

namespace RAK.Core.Service.Repository.Abstraction
{
    /// <summary>
    /// Interfaz de Repositorio para Entidades que son Editables
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="BIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="BCE"></typeparam>
    public interface IEditRepository<E, LIE, LCE> : IConsultRepository<E, LIE, LCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where LCE : class, IListedCriteriaEntity
    {
        /// <summary>
        /// Inserta / Actualiza la entidad
        /// </summary>
        /// <param name="entity"></param>
        ResultEntity Add(E entity);

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        ResultEntity Update(E entity);

        /// <summary>
        /// Borra la entidad
        /// </summary>
        /// <param name="ID"></param>
        ResultEntity Delete(IdentityEntity entity);
    }
}
