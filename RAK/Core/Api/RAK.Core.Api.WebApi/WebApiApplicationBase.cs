using RAK.Fwk.Api.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RAK.Core.Api.WebApi
{
    /// <summary>
    /// Manager de aplicacion WebApi base
    /// </summary>
    public abstract class WebApiApplicationBase : HttpApplication, IWebApiApplicationGeneric
    {
    }
}
