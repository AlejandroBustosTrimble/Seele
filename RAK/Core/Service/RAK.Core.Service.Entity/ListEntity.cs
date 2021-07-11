using RAK.Core.Service.Entity.Interfaces;
using System.Collections.Generic;

namespace RAK.Core.Service.Entity
{
    /// <summary>
    /// Entidad que contiene una lista de entidades
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public class ListEntity<E> : EntityBase, IListEntity<E>
        where E : IEntityBase
    {
        public List<E> List { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        public ListEntity():base()
        {
            this.List = new List<E>();
        }

    }
}
