using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Fwk.Api.Adapter
{
    /// <summary>
    /// Interfaz de adapter
    /// </summary>
    /// <remarks>
    /// Se encarga de mapeo de VM a EN y llamada a Logic
    /// Queda aca y no en RAK.Fwk.Api.Adapter.Abstract porque donde llamo 
    /// esto es donde va a llenar el DIContainer, entonces no hace falta que 
    /// exista Abstract en otra dll
    /// </remarks>
    public interface IAdapterGeneric
    {
        
    }
}
