using System;
using System.Linq.Expressions;

namespace RAK.Core.Service.Entity.Interfaces
{
    /// <summary>
    /// Interface de entidad criteria
    /// </summary>
    public interface ICriteriaEntity<E> : IGetListEntity
        where E : IEntityBase
    {
        /// <summary>
        /// Criteria
        /// </summary>
        Expression<Func<E, bool>> Criteria { get; set; }
    }
}
