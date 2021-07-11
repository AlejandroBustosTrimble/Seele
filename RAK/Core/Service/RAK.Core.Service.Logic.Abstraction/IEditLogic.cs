using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;

namespace RAK.Core.Service.Logic.Abstraction
{
    /// <summary>
    /// Interfaz de logica de Entidades de Editables
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    public interface IEditLogic<E, LIE, LCE> : IConsultLogic<E, LIE, LCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where LCE : class, IListedCriteriaEntity
    {
        /// <summary>
        /// Inserta una entidad
        /// </summary>
        /// <param name="entity"></param>
        ResultEntity Add(E entity);

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        ResultEntity Update(E entity);

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="ID"></param>
        ResultEntity Delete(IdentityEntity entity);
    }
}
