using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model.Interfaces
{
    /// <summary>
    /// ViewModel con una lista de entidades
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public interface IListModel<VM> : IModelBase, IResponseModel
        where VM : IModelBase
    {
        /// <summary>
        /// Lista
        /// </summary>
        List<VM> List { get; set; }
    }
}
