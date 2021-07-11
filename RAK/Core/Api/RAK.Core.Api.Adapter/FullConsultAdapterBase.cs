using RAK.Core.Api.Adapter.Abstraction;
using RAK.Core.Api.Model;
using RAK.Core.Api.Model.Interfaces;
using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Logic.Abstraction;

namespace RAK.Core.Api.Adapter
{
    /// <summary>
    /// Adapter de Entidades de SOLO Consulta con vista basica
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="VM"></typeparam>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIVM"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="BIVM"></typeparam>
    /// <typeparam name="BIE"></typeparam>
    /// <typeparam name="LCVM"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="BCVM"></typeparam>
    /// <typeparam name="BCE"></typeparam>
    public abstract class FullConsultAdapterBase<VM, E, LIVM, LIE, BIVM, BIE, LCVM, LCE, BCVM, BCE, L> : ConsultAdapterBase<VM, E, LIVM, LIE, LCVM, LCE, L>, IFullConsultAdapter<VM, LIVM, BIVM, LCVM, BCVM>
        where VM : class, ISpecificModel, new()
        where E : class, IEntityBase
        where LIVM : class, IListedItemModel
        where LIE : class, IListedItemEntity
        where BIVM : class, IBasicModel
        where BIE : class, IBasicEntity
        where LCVM : class, IListedCriteriaModel
        where LCE : class, IListedCriteriaEntity
        where BCVM : class, IBasicCriteriaModel
        where BCE : class, IBasicCriteriaEntity
        where L : IFullConsultLogic<E, LIE, BIE, LCE, BCE>
    {
        #region Methods

        #region Public

        /// <summary>
        /// Obtiene la vista basica de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ListModel<BIVM> GetBasic(BCVM entity)
        {
            var resultVM = this.ExecuteAction<BCVM, ListModel<BIVM>, BCE, ListEntity<BIE>>(req =>
            {
                var resultE = this.Logic.GetBasic(req);

                return resultE;
            }, entity);

            return resultVM;
        }

        #endregion

        #endregion
    }
}
