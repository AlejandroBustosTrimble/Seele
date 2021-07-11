using RAK.Fwk.Common.AbstractEntity.Attributes;
using System;

namespace RAK.Fwk.Common.AbstractEntity
{
    public interface IEntityGeneric
    {
        [DataKey()]
        Int64 ID { get; set; }
    }
}
