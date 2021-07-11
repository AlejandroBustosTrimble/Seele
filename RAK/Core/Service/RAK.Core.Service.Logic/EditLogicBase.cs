using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Logic.Abstraction;
using RAK.Core.Service.Repository.Abstraction;

namespace RAK.Core.Service.Logic
{
    /// <summary>
    /// Logica de Entidades de Editables
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    /// <typeparam name="R"></typeparam>
    public abstract class EditLogicBase<E, LIE, LCE, R> : ConsultLogicBase<E, LIE, LCE, R>, IEditLogic<E, LIE, LCE>
        where E : class, IEntityBase, new()
        where LIE : class, IListedItemEntity
        where LCE : class, IListedCriteriaEntity
        where R : IEditRepository<E, LIE, LCE>
    {
        #region Methods

        #region Public

        /// <summary>
        /// Inserta una entidad
        /// </summary>
        /// <param name="entity"></param>
        public ResultEntity Add(E entity)
        {
            var result = this.ExecuteAction<E, ResultEntity>(req =>
            {
                return this.Repository.Add(req);
            }, entity);

            return result;
        }

        /// <summary>
        /// Elimina una entidad
        /// </summary>
        /// <param name="ID"></param>
        public ResultEntity Delete(IdentityEntity entity)
        {
            var result = this.ExecuteAction<IdentityEntity, ResultEntity>(req =>
            {
                return this.Repository.Delete(req);
            }, entity);

            return result;
        }

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        public ResultEntity Update(E entity)
        {
            var result = this.ExecuteAction<E, ResultEntity>(req =>
            {
                return this.Repository.Update(req);
            }, entity);

            return result;
        }

        #endregion

        #endregion
    }
}
