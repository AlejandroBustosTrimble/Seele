using RAK.Core.Service.Entity;
using RAK.Core.Service.Entity.Interfaces;
using RAK.Core.Service.Repository.Abstraction;

namespace RAK.Core.Service.Repository
{
    /// <summary>
    /// Repositorio para Entidades que son Editables
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <typeparam name="LIE"></typeparam>
    /// <typeparam name="LCE"></typeparam>
    public abstract class EditRepositoryBase<E, LIE, LCE> : ConsultRepositoryBase<E, LIE, LCE>, IEditRepository<E, LIE, LCE>
        where E : class, IEntityBase
        where LIE : class, IListedItemEntity
        where LCE : class, IListedCriteriaEntity
    {
        /// <summary>
        /// Inserta / Actualiza la entidad
        /// </summary>
        /// <param name="entity"></param>
        public virtual ResultEntity Add(E entity)
        {
            this.Context.Add(entity);

            var result = new ResultEntity();

            result.ID = entity.ID;

            return result;
        }

        /// <summary>
        /// Actualiza una entidad
        /// </summary>
        /// <param name="entity"></param>
        public virtual ResultEntity Update(E entity)
        {
            this.Context.Add(entity);

            var result = new ResultEntity();

            result.Count = 1;

            return result;
        }

        /// <summary>
        /// Borra la entidad
        /// </summary>
        /// <param name="ID"></param>
        public virtual ResultEntity Delete(IdentityEntity entity)
        {
            this.Context.Delete(entity);

            var result = new ResultEntity();

            return result;
        }
    }
}
