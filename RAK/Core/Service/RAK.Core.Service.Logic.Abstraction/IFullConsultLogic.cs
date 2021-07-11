using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;

namespace RAK.Core.Service.Logic.Abstraction
{
    /// <summary>
    /// Interfaz de Logica de Entidades de SOLO Consulta con vista basica
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="BIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="BCE"></typeparam>
    public interface IFullConsultLogic<E, LIE, BIE, LCE, BCE> : IConsultLogic<E, LIE, LCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where BIE : class, IBasicEntity
        where LCE : class, IListedCriteriaEntity
        where BCE : class, IBasicCriteriaEntity
    {
        /// <summary>
        /// Obtiene la vista basica de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ListEntity<BIE> GetBasic(BCE entity);
    }
}
