using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Repository.Abstraction;

namespace RAK.Core.Service.Repository
{
    /// <summary>
    /// Repositorio de SOLO Consulta con vista basica
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="BIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="BCE"></typeparam>
    public abstract class FullConsultRepositoryBase<E, LIE, BIE, LCE, BCE> : ConsultRepositoryBase<E, LIE, LCE>, IFullConsultRepository<E, LIE, BIE, LCE, BCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where BIE : class, IBasicEntity
        where LCE : class, IListedCriteriaEntity
        where BCE : class, IBasicCriteriaEntity
    {
        /// <summary>
        /// Obtiene la vista de listado basica de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract ListEntity<BIE> GetBasic(BCE entity);
    }
}
