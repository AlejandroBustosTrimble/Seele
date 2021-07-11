using RAK.Core.Api.Adapter.Abstraction;
using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Logic.Abstraction;

namespace RAK.Core.Api.Adapter
{
    /// <summary>
    /// Adapter de Entidades Editable
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    public abstract class EditAdapterBase<VM, E, LIVM, LIE, LCVM, LCE, L> : ConsultAdapterBase<VM, E, LIVM, LIE, LCVM, LCE, L>, IEditAdapter<VM, LIVM, LCVM>
        where VM : class, ISpecificModel, new()
        where E : class, IEntityBase
        where LIVM : class, IListedItemModel
        where LIE : class, IListedItemEntity
        where LCVM : class, IListedCriteriaModel
        where LCE : class, IListedCriteriaEntity
        where L : IEditLogic<E, LIE, LCE>
    {
        #region Methods

        #region Public

        /// <summary>
        /// Inserta una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultModel Add(VM entity)
        {
            var resultVM = this.ExecuteAction<VM, ResultModel, E, ResultEntity>(req =>
            {
                var resultE = this.Logic.Add(req);

                return resultE;
            }, entity);

            return resultVM;
        }

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultModel Update(VM entity)
        {
            var resultVM = this.ExecuteAction<VM, ResultModel, E, ResultEntity>(req =>
            {
                var resultE = this.Logic.Update(req);

                return resultE;
            }, entity);

            return resultVM;
        }

        /// <summary>
        /// Borra una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResultModel Delete(IdentityModel entity)
        {
            var resultVM = this.ExecuteAction<IdentityModel, ResultModel, IdentityEntity, ResultEntity>(req =>
            {
                var resultE = this.Logic.Delete(req);

                return resultE;
            }, entity);

            return resultVM;
        }

        #endregion

        #endregion
    }
}
