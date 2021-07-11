using RAK.Core.Service.Entity.Interfaces;
using System;
using System.Linq.Expressions;

namespace RAK.Core.Service.Entity
{
    /// <summary>
    /// Entidad Criteria
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class CriteriaEntity<E> : GetListEntity
        where E : class, IEntityBase
    {
        /// <summary>
        /// Criteria
        /// </summary>
        public Expression<Func<E, bool>>  Criteria { get; set; }
    }
}
