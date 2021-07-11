using RAK.Core.Api.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAK.Core.Api.Model
{
    /// <summary>
    /// ViewModel con una lista de entidades
    /// </summary>
    /// <typeparam name="VM"></typeparam>
    public class ListModel<VM> : ModelBase, IListModel<VM>
        where VM : IModelBase
    {
        /// <summary>
        /// Lista
        /// </summary>
        public List<VM> List { get; set; }
    }
}
