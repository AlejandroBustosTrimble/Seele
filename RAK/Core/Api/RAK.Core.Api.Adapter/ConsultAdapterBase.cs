using RAK.Core.Api.Adapter.Abstraction;
using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Logic.Abstraction;

namespace RAK.Core.Api.Adapter
{
    /// <summary>
    /// Adapter de Entidades de SOLO Consulta
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    public abstract class ConsultAdapterBase<VM, E, LIVM, LIE, LCVM, LCE, L> : AdapterBase<L>, IConsultAdapter<VM, LIVM, LCVM>
        where VM : class, ISpecificModel, new()
        where E : class, IEntityBase
        where LIVM : class, IListedItemModel
        where LIE : class, IListedItemEntity
        where LCVM : class, IListedCriteriaModel
        where LCE : class, IListedCriteriaEntity
        where L : IConsultLogic<E, LIE, LCE>
    {
        #region Methods

        #region Public

        /// <summary>
        /// Obtiene una lista de una entidad con todos sus registros
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ListModel<VM> GetAll(GetListModel entity)
        {
            var resultVM = this.ExecuteAction<GetListModel, ListModel<VM>, GetListEntity, ListEntity<E>>(req =>
            {
                var resultE = this.Logic.GetAll(req);

                return resultE;
            }, entity);

            return resultVM;
        }

        /// <summary>
        /// Obtiene una entidad por ID
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public VM GetById(IdentityModel entity)
        {
            var resultVM = this.ExecuteAction<IdentityModel, VM, IdentityEntity, E>(req =>
            {
                var resultE = this.Logic.GetById(req);

                return resultE;
            }, entity);

            return resultVM;
        }

        /// <summary>
        /// Obtiene la vista de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual ListModel<LIVM> GetListed(LCVM entity)
        {
            var resultVM = this.ExecuteAction<LCVM, ListModel<LIVM>, LCE, ListEntity<LIE>>(req =>
            {
                var resultE = this.Logic.GetListed(req);

                return resultE;
            }, entity);

            return resultVM;
        }

        #endregion

        #endregion
    }
}
