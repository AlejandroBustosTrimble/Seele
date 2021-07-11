using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Logic.Abstraction;
using RAK.Core.Service.Repository.Abstraction;

namespace RAK.Core.Service.Logic
{
    /// <summary>
    /// Logica base de Entidades de SOLO Consulta
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="R"></typeparam>
    public abstract class ConsultLogicBase<E, LIE, LCE, R> : LogicBase<R>, IConsultLogic<E, LIE, LCE>
        where E : class, IEntityBase, new()
        where LIE : class, IListedItemEntity
        where LCE : class, IListedCriteriaEntity
        where R : IConsultRepository<E, LIE, LCE>
    {
        #region Methods

        #region Public

        /// <summary>
        /// Obtiene una lista de una entidad con todos sus registros
        /// </summary>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ListEntity<E> GetAll(GetListEntity entity)
        {
            var result = this.ExecuteAction<GetListEntity, ListEntity<E>>(req =>
            {
                return this.Repository.GetAll(req);
            }, entity);

            return result;
        }

        /// <summary>
        /// Obtiene una lista de una entidad en base a un filtro
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="count"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public ListEntity<E> GetByCriteria(CriteriaEntity<E> entity)
        {
            var result = this.ExecuteAction<CriteriaEntity<E>, ListEntity<E>>(req =>
            {
                return this.Repository.GetByCriteria(req);
            }, entity);

            return result;
        }

        /// <summary>
        /// Obtiene una entidad por ID
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="ID"></param>
        /// <returns></returns>
        public E GetById(IdentityEntity entity)
        {
            var result = this.ExecuteAction<IdentityEntity, E>(req =>
            {
                return this.Repository.GetByID(req);
            }, entity);

            return result;
        }

        /// <summary>
        /// Obtiene la vista de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ListEntity<LIE> GetListed(LCE entity)
        {
            var result = this.ExecuteAction<LCE, ListEntity<LIE>>(req =>
            {
                return this.Repository.GetListed(req);
            }, entity);

            return result;
        }

        #endregion

        #endregion
    }
}
