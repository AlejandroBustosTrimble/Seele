using System.Collections.Generic;

namespace RAK.Core.Service.Entity.Interfaces
{
    /// <summary>
    /// Entidad que contiene una lista de entidades
    /// </summary>
    /// <typeparam name="E"></typeparam>
    public interface IListEntity<E> : IEntityBase
        where E : IEntityBase
    {
        List<E> List { get; }
    }
}
