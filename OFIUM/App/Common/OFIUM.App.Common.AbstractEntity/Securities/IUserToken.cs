using RAK.Fwk.Common.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace OFIUM.App.Common.AbstractEntity.Securities
{
    public interface IUserToken : IEntityGeneric
    {
        String Token { get; set; }
    }
}
