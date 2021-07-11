using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Repository.Abstraction;
using RAK.Fwk.Service.Repository;

namespace RAK.Core.Service.Repository
{
    /// <summary>
    /// Repositorios base de SOLO Consulta
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    public abstract class ConsultRepositoryBase<E, LIE, LCE> : RepositoryGeneric, IConsultRepository<E, LIE, LCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where LCE : class, IListedCriteriaEntity
    {
        #region Methods

        #region Public

        /// <summary>
        /// Obtiene una lista de una entidad con todos sus registros
        /// </summary>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public virtual ListEntity<E> GetAll(GetListEntity entity)
        {
            var list = this.Context.GetAll<E>(entity.Count, entity.OffSet);

            var result = new ListEntity<E>();

            result.List.AddRange(list);

            return result;
        }

        /// <summary>
        /// Obtiene una lista de una entidad en base a un filtro
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public virtual ListEntity<E> GetByCriteria(CriteriaEntity<E> entity)
        {
            var list = this.Context.GetByCriteria(entity.Criteria, entity.Count, entity.OffSet);

            var result = new ListEntity<E>();

            result.List.AddRange(list);

            return result;
        }

        /// <summary>
        /// Obtiene una entidad por ID
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public virtual E GetByID(IdentityEntity entity)
        {
            var result = this.Context.GetById<E>(entity.ID);

            return result;
        }

        /// <summary>
        /// Obtiene la vista de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract ListEntity<LIE> GetListed(LCE entity);

        #endregion

        #endregion
    }
}
