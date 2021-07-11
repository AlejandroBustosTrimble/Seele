using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model.Interfaces
{
    /// <summary>
    /// Interface de ViewModel criteria
    /// </summary>
    public interface ICriteriaModel<VM> : IModelBase
        where VM : IModelBase
    {
        /// <summary>
        /// Criteria
        /// </summary>
        Expression<Func<VM, bool>> Criteria { get; set; }
    }
}
