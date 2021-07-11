using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Logic.Abstraction;
using RAK.Core.Service.Repository.Abstraction;

namespace RAK.Core.Service.Logic
{
    /// <summary>
    /// Logica de Entidades de SOLO Consulta con vista basica
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="BIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="BCE"></typeparam>
    /// <typeparam name="R"></typeparam>
    public abstract class FullConsultLogicBase<E, LIE, BIE, LCE, BCE, R> : ConsultLogicBase<E, LIE, LCE, R>, IFullConsultLogic<E, LIE, BIE, LCE, BCE>
        where E : class, IEntityBase, new()
        where LIE : class, IListedItemEntity
        where BIE : class, IBasicEntity
        where LCE : class, IListedCriteriaEntity
        where BCE : class, IBasicCriteriaEntity
        where R : IFullConsultRepository<E, LIE, BIE, LCE, BCE>
    {
        #region Methods

        #region Public

        /// <summary>
        /// Obtiene la vista basica de listado de una entidad
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ListEntity<BIE> GetBasic(BCE entity)
        {
            var result = this.ExecuteAction<BCE, ListEntity<BIE>>(req =>
            {
                return this.Repository.GetBasic(req);
            }, entity);

            return result;
        }

        #endregion

        #endregion
    }
}
