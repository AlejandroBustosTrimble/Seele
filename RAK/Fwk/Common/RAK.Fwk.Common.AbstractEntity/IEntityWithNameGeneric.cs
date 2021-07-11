using System;

namespace RAK.Fwk.Common.AbstractEntity
{
    public interface IEntityWithNameGeneric : IEntityGeneric
    {
        String Name { get; set; }
    }
}
