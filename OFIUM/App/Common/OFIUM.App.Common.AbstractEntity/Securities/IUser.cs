using RAK.Fwk.Common.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFIUM.App.Common.AbstractEntity
{
    public interface IUser : IEntityGeneric
    {
        String Password { get; set; }
    }
}
